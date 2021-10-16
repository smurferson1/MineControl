# MineControl 

A power tool for temperature management, scheduling, and data aggregation during casual cryptocurrency mining. For us plebs with a single GPU and CPU running **Windows**. It can automatically keep your miners and hardware running within your parameters and give you data about what's going on (e.g. charts, averages, kWh). This is accomplished by:
- Reading temperatures and other sensor data from an external hardware monitor app.
- Setting GPU power profile via an external GPU controller app.
- Optionally managing execution of an external GPU and/or CPU mining app and charting/logging their output centrally.
- Finding relevant data in raw text with customizable regular expressions.

Specific hardware monitor and GPU controller applications are not mentioned in order to avoid legal issues, but the default configuration happens to be compatible with the most popular hardware monitor and GPU controller applications for NVIDIA GPUs. Hint: they rhyme with **RidesharePimpo** and **MakeThemCry ButterChurner**.

## Donations
MineControl is free and open source. Crypto donations are however appreciated and will keep the developer(s) motivated to fix and improve things. Current wallets for donation:
- TBD

## DISCLAIMERS

- MineControl is NOT guaranteed to work correctly or protect your computer in all circumstances. It is in development and subject to being shit, making mistakes, or missing *YOUR* mistakes. **USE AT YOUR OWN RISK**. It is *essential* to configure MineControl carefully and watch it for a while to be sure it's doing what you intend. Your temps and power usage *CAN* go too high if you misconfigure MineControl or it makes a booboo.
- MineControl won't take your mining profits or send anything to anyone, **IF DOWNLOADED FROM THIS GITHUB REPO**. Feel free to inspect the code or compile yourself, but DO NOT get MineControl from anywhere or anyone else, since software can easily be compromised by attackers and redistributed in a nearly identical form.
- As with most other software developed in free time for fun, there is no guarantee from the developer to provide support or updates of any kind.

## Key Features

- Customizable input from external apps via system tray tooltips or console using regular expressions.
- Charting and statistics for many metrics like GPU memory junction temperature, hash rate, power usage (W and kWh), averages, etc. Hash rate data can be split by algorithm when using a miner that supports algo-switching.
- Low resource consumption. Designed to use 0% GPU and <1% CPU most of the time.
- System tray display of GPU power level, GPU miner status, and CPU miner status.
- Customizable GPU temperature (or memory junction temperature) range, controlled with up to 5 power profiles.
- GPU failsafe protections to shut off the GPU miner if max temp is exceeded at lowest power, or while the temp is unknown.
- Advanced scheduling for execution of GPU and CPU miner, for example to avoid high power consumption during peak hours of 5-8PM on weekdays during summer.
- Optional miner auto-shutoff during user activity.
- Log aggregation from MineControl and miners.
- Archiving of logs and configuration.
- More customization than most plebs could ask for.

## Instructions

1) Install the hardware monitor, GPU controller application, GPU and/or CPU miner you want to use. MineControl was tested using **PhoenixMiner** for GPU and **XMRig** (MoneroOcean version) for CPU, so these are supported by default.
2) Configure the miner(s) and test them to make sure they run correctly and send crypto to your wallet address.
3) Configure the hardware monitor output, which is done inside of the hardware monitor application. For RidesharePimpo, you MUST enable system tray output for the metrics you want to track, like GPU memory junction temp, as this is what MineControl uses to read data. This can be enabled individually through the right-click menu for each sensor row.
3) Configure the power profiles inside of the GPU controller application. This is ***EXTREMELY IMPORTANT***. You have full control over your power profiles, so you can set voltage curves or whatever you like. This should go from lowest power (lowest temp) in profile 1 to highest power in profile 5. To prevent MineControl from using a profile, change the parameter for the profile (under "Command Line Params" in GPU Temperature Management) to blank or something invalid. **Keep in mind that your temperature control will only be as good as your range of power profiles.**
4) Configure miners and applications by pointing to their executable. Batch files are supported but you *must* add the exact application (EXE) name *without* extension in the "App" cell, case-insensitive.
5) Configure Data Tracking metrics as needed for your miners and applications, and disable metrics you don't use. This is the tough part, but may not be necessary if you're using all of RidesharePimpo, MakeThemCry ButterChurner, PhoenixMiner, and xmrig. Use the "RegEx" method for any metric you need to customize. An easy way to do this is paste example output from the application into https://regexr.com/ for easyish learning and testing. There is a "View SysTray" button in MineControl that can be used to get a current example of that output (use Ctrl+C to copy the text out of the popup).
6) Configure your target GPU temps and anything else you care about.
7) Press Start!
8) Monitor periodically for a while to make sure your config is working to your satisfaction and not doing anything bad.

## Tips

- DO NOT run MineControl elevated (i.e. with Windows administrator privileges) if controlling GPU and CPU miner. In testing, there appeared to be a bug with PhoenixMiner that caused **masssively** increased GPU temps after several minutes when MineControl was elevated, for no apparent reason.
- DO run MineControl using a Windows administrator account if any of the controlled applications require admin privileges, and make sure Windows is configured so that additional admin popups are not displayed when MineControl launches miners and applications. Popular hardware monitor and GPU controller software *does* require admin rights.
- There are detailed tooltips for most settings to explain what they do.
- When MineControl runs a miner, the normal miner window will *not* show, because its output is being entirely redirected to MineControl and cannot be shown in its normal window. To verify the miner is running, check it in Task Manager (Windows). To see miner log entries in MineControl, enable Keep Logs for the miner, otherwise they're just gone, yo. Note: any text coloring from the miner is not kept.
- When MineControl recognizes a log entry from a miner as input for a metric (like when a RegEx match is found), it categorizes the log entry as "Input" and appends what it found. This can be used to debug your data tracking customizations if needed. This only shows up if Keep Logs for the miner is enabled.
- Understanding the MineControl system tray icon: 
  - GPU miner is the left bar, GPU power step is the number in the middle, and CPU miner is the right bar. 
  - Miner bar colors: black = disabled by user, green = running, yellow = stopped due to user activity, gray = stopped due to schedule, red = stopped due to error.
- Maintaining performance and low resource consumption:
  - Run minimized when not attended.
  - Enable clearing logs and charts at least once per day. Data built up much longer than that *will* raise CPU use and slow down the UI.
  - Avoid changing polling interval by much. Too low will use more resources, and too high will allow temps and other things to get out of hand for longer. 5000ms is a pretty good spot.
  - Disable GPU and CPU miner Keep Logs option. 
  - Data tracking: disable metrics you don't need. 
  - Data Tracking: use the UserValue method for units and algos that don't change. Reduces processing a tiny bit.

## Key Un-features
- Doesn't support more than 1 GPU or CPU.
- Doesn't support more than 1 GPU or CPU miner.
- Not internationalized, translated, or respectful of Windows regional settings.
- Kind of ugly.
- Not very user friendly.
- Chart data and stats can't be archived or restored.
- Probably some allowed configurations that would make no sense or cause errors or crashes.
- Engineering: badly engineered with a bunch of logic attached to the form and stuff. Sorry.
- Most temperature and power management features are GPU-only.
