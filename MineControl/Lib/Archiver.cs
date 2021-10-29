using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl.Lib
{
    /// <summary>
    /// Manages archiving and data retention
    /// </summary>
    public class Archiver
    {
        private Properties.Settings Settings { get; set; } = Properties.Settings.Default;
        private ILog Log { get; }
        private ISettingsFile SettingsFile { get; }
        private DateTime LastArchiveEval { get; set; } = DateTime.Now;
        public bool IsConfigArchiveNeeded { get; set; } = false;
        public List<Chart> Charts { get; set; }
        public DataTable LogData { get; set; }
        
        public Archiver(DataTable logData, List<Chart> charts, ISettingsFile settingsFile, ILog log)
        {
            LogData = logData;
            SettingsFile = settingsFile;
            Charts = charts;
            Log = log;
        }

        public void DeleteOldArchiveFiles(DateTime evalStartTime)
        {
            if (Settings.archivesDeleteOldFiles)
            {
                DateTime archiveAgeCutoff = evalStartTime.AddDays(-Settings.archivesDeleteOldFilesDays);
                int deletedLogCount = 0;
                int deletedConfigCount = 0;

                // delete old log archives
                DirectoryInfo directory = new(GetArchiveFolder());
                var files = directory.GetFiles("*.*").Where(f => f.LastWriteTime < archiveAgeCutoff && Path.GetExtension(f.Name) == ".txt");
                foreach (var file in files)
                {
                    file.Delete();
                    ++deletedLogCount;
                }

                // delete old config archives
                directory = new DirectoryInfo(GetConfigArchiveFolder());
                files = directory.GetFiles("*.*").Where(f => f.LastWriteTime < archiveAgeCutoff && Path.GetExtension(f.Name) == ".config");
                foreach (var file in files)
                {
                    file.Delete();
                    ++deletedConfigCount;
                }

                if (deletedLogCount + deletedConfigCount > 0)
                {
                    Log.Append($"Deleted {deletedLogCount} log archive files and {deletedConfigCount} config archive files older than {Settings.archivesDeleteOldFilesDays} days");
                }
            }
        }

        public bool StartArchiveAndClear(out DateTime evalStartTime)
        {
            // first make sure an archive or clear option is enabled
            if (!(Settings.archivesArchiveConfig || Settings.archivesDeleteOldFiles || Settings.archivesLogManagement || Settings.archivesClearOldCharts))
            {
                evalStartTime = DateTime.MaxValue;
                return false;
            }

            // find next archive time
            DateTime now = DateTime.Now;
            var nextArchiveEval = Settings.archivesArchiveIntervalUnit switch
            {
                "Days" => LastArchiveEval.AddDays(Settings.archivesArchiveInterval),
                "Hours" => LastArchiveEval.AddHours(Settings.archivesArchiveInterval),
                "Minutes" => LastArchiveEval.AddMinutes(Settings.archivesArchiveInterval),
                _ => throw new Exception("Unknown archive interval unit"),
            };
            if (now >= nextArchiveEval)
            {
                evalStartTime = now;
                LastArchiveEval = now;
                return true;
            }
            else
            {
                evalStartTime = nextArchiveEval;
                return false;
            }
        }

        public void ClearOldChartData(DateTime evalStartTime)
        {
            DateTime timeCutoff = GetTimeCutoff(evalStartTime, Settings.archivesClearOldChartsUnit, Settings.archivesClearOldChartsValue);
            bool logged = false;
            List<DataPoint> points;
            foreach (Chart chart in Charts)
            {
                foreach (Series series in chart.Series)
                {
                    points = series.Points.Where(p => DateTime.FromOADate(p.XValue) <= timeCutoff).ToList();
                    for (int i = points.Count - 1; i >= 0; i--)
                    {
                        if (!logged)
                        {
                            // only log the clearing if we actually found something to clear
                            Log.Append($"Clearing chart data older than {timeCutoff}");
                            logged = true;
                        }
                        series.Points.Remove(points[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Archives the config if it's been marked as changed or it's never been archived before
        /// </summary>
        public void ArchiveConfigIfNeeded()
        {
            try
            {
                if (Settings.archivesArchiveConfig && (IsConfigArchiveNeeded || !Directory.GetFiles(GetConfigArchiveFolder()).Any()))
                {
                    IsConfigArchiveNeeded = false;
                    SettingsFile.Export(GetNextConfigArchiveFilePath());
                }
            }
            catch (Exception ex)
            {
                Log.Append($"Exception archiving config: {ex.GetType()} - {ex.Message}");
            }
        }

        public string GetNextConfigArchiveFilePath()
        {
            string configArchivePath;
            int i = 1;
            do
            {
                configArchivePath = Path.Combine(GetConfigArchiveFolder(), $"userArchive{i:000}.config");
                ++i;
            }
            while (File.Exists(configArchivePath));
            return configArchivePath;
        }

        public void ArchiveAndClearOldLogs(DateTime evalStartTime, bool archiveAll = false)
        {
            if (Settings.archivesLogManagement && LogData.Rows.Count > 0)
            {
                // find date/time cutoff          
                DateTime timeCutoff = archiveAll ? DateTime.MaxValue : GetTimeCutoff(evalStartTime, Settings.archivesLogManagementUnit, Settings.archivesLogManagementValue);

                // find any log entries in scope                
                DataRow[] rowsToActOn = LogData.Select($"Time <= #{timeCutoff}#");

                if (rowsToActOn.Any())
                {
                    StreamWriter writer = null;
                    bool archiveEnabled = Settings.archivesLogManagementType.Contains("Archive");
                    bool clearEnabled = Settings.archivesLogManagementType.Contains("Clear");
                    if (archiveEnabled)
                    {
                        // one archive file per day
                        string archiveFile = Path.Combine(GetArchiveFolder(), $"LogArchive{evalStartTime:yyyy-MM-dd}.txt");

                        if (File.Exists(archiveFile))
                        {
                            writer = File.AppendText(archiveFile);
                        }
                        else
                        {
                            writer = File.CreateText(archiveFile);
                        }
                        Log.Append($"Archiving and clearing logs older than {timeCutoff}");
                    }
                    else if (clearEnabled)
                    {
                        Log.Append($"Clearing logs older than {timeCutoff}");
                    }

                    try
                    {
                        foreach (DataRow row in rowsToActOn)
                        {
                            if (archiveEnabled)
                            {
                                writer.WriteLine($"\"{row[0]}\",\"{row[1]}\",\"{row[2]}\",\"{row[3]}\"");
                            }
                            if (clearEnabled)
                            {
                                row.Delete();
                            }
                        }
                    }
                    finally
                    {
                        writer?.Close();
                        writer?.Dispose();
                    }
                }
            }
        }

        public static DateTime GetTimeCutoff(DateTime evalStartTime, string unit, int value)
        {
            return unit switch
            {
                "Days" => evalStartTime.AddDays(-value),
                "Hours" => evalStartTime.AddHours(-value),
                "Minutes" => evalStartTime.AddMinutes(-value),
                _ => throw new Exception("Unknown unit"),
            };
        }

        public string GetArchiveFolder()
        {
            string folder = Settings.archivesArchiveFolder;
            if (!Directory.Exists(folder))
            {
                try
                {
                    if (folder.Length > 0)
                    {
                        Directory.CreateDirectory(folder);
                    }
                    else
                    {
                        folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), typeof(Program).Assembly.GetName().Name);
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                    }
                    if (!Directory.Exists(folder))
                    {
                        throw new Exception("Couldn't create a valid archive folder");
                    }
                }
                catch (Exception ex)
                {
                    Log.Append($"Exception getting archive folder. Archives may be negatively impacted. Exception: {ex.GetType()} - {ex.Message}", LogType.Error);
                }
            }
            return folder;
        }

        public string GetConfigArchiveFolder()
        {
            return Path.Combine(GetArchiveFolder(), "ConfigArchives");
        }

        /// <summary>
        /// Runs an archive and clear operation if it's time to do so based on user settings.
        /// </summary>
        internal void RunArchiveAndClearIfNeeded()
        {
            try 
            { 
                if (StartArchiveAndClear(out DateTime evalStartTime))
                {
                    ArchiveAndClearOldLogs(evalStartTime);
                    ArchiveConfigIfNeeded();
                    ClearOldChartData(evalStartTime);
                    DeleteOldArchiveFiles(evalStartTime);
                }
            }
            catch (Exception ex)
            {
                Log?.Append($"Exception archiving/clearing data: {ex.GetType()} - {ex.Message}", LogType.Error);
            }
        }
    }
}
