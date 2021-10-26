﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib.Utils
{
    public static class ConfigUtils
    {
        /// <summary>
        /// Ensures current settings file is loaded if one exists, upgrading (migrating) from previous versions as needed.
        /// </summary>
        public static void LoadSettingsFile(ApplicationSettingsBase settings, ILog log)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            if (!config.HasFile)
            {
                try
                {
                    settings.Upgrade();
                    log.Append($"MineControl settings for this version were either created from defaults or migrated from a previous version, then loaded from \"{config.FilePath}\"");
                }
                catch
                {
                    // note: doesn't seem to trigger, even when Upgrade finds nothing to migrate
                    log.Append($"No existing MineControl settings found, so defaults were set and loaded from \"{config.FilePath}\"");
                }
            }
            else
            {
                // note: we don't need to actually load anything here, as .NET has done this already
                log.Append($"Existing MineControl settings were loaded from \"{config.FilePath}\"");
            }
        }
    }
}
