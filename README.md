# MineControl

A **power tool** for automating temperature management, scheduling, and data aggregation while mining cryptocurrency. For us plebs with a single GPU and CPU running **Windows 10 x64**. It can automatically keep your miners and hardware running within your parameters and give you nice data (e.g. charts, averages, kWh), with more customization than you probably need. This is accomplished by:
- Reading temperatures and other sensor data from an external hardware monitor app.
- Setting GPU power profile through an external GPU controller app.
- Optionally managing execution of an external GPU and/or CPU mining app and capturing their output for charting/logging.
- Finding relevant data in the raw output using customizable regular expressions.

Names of hardware monitor and GPU controller apps are omitted in order to avoid legal issues, but the default config is compatible with the most popular hardware monitor and GPU controller app. These support both NVIDIA and AMD GPUs. Hint: they rhyme with **RidesharePimpo** and **WhoStoleMy ButterChurner**.

![image](https://user-images.githubusercontent.com/91922614/138532772-a657dc16-2947-4f0b-8315-203717929fcc.png)

## Donations

MineControl is free and open source. Crypto donations are however appreciated and will keep the developer(s) motivated to fix and improve things. Current wallets for donation:
- TBD

If you want to see additional donation options added here, please contact the developer.

## License

MineControl is licensed under the Creative Commons Attribution-ShareAlike 4.0 International Public License, a.k.a. **CC BY-SA 4.0** (see License.txt).

Note: CC BY-SA is specifically required due to adaptation of certain code from StackOverflow. For attribution, see MineControl's About tab, or the source code.

## DISCLAIMERS

- MineControl is NOT guaranteed to work correctly or protect your computer in all circumstances. It is in development and subject to being shit, making mistakes, or missing *YOUR* mistakes. **USE AT YOUR OWN RISK**. Please configure MineControl carefully and watch it for a while to be sure it's doing what you intend. Your temps and power usage *CAN* go too high if you misconfigure MineControl or it makes a booboo.
- MineControl won't do nefarious things like take your mining profits or send anything to anyone, **AS LONG AS YOU DOWNLOADED IT FROM THIS GITHUB REPO**. Feel free to inspect the code or compile yourself, but DO NOT download MineControl from anywhere or anyone else, since software can easily be compromised by attackers and redistributed in a nearly identical form.
- As with most other software developed in free time for fun, there is no guarantee from the developer to provide support or updates of any kind.

## Key Features

- Drive a GPU miner and/or a CPU miner and aggregate their data.
- Set GPU temperature (or memory junction temperature) min and max targets, and control with up to 5 power profiles.
- Schedule execution of GPU and CPU miner with advanced options, for example to avoid high power consumption during peak hours of 5-8PM on weekdays during summer.
- Turn off pretty much anything you don't want. If you just want to control temps without driving miners, or the other way around, that's fine.
- Charts and/or statistics for many metrics like GPU temperature or memory junction temperature, hash rate, power usage (W and kWh), averages, etc. Hash rate data can be split by algorithm when using a miner that supports algo-switching.
- Low resource consumption. Designed to use 0% GPU and <1% CPU most of the time (run minimized for best results).
- Capture output from external apps using regular expressions, via either system tray tooltips or console (stdout redirection).
- System tray display of GPU power level and GPU/CPU miner statuses.
- GPU failsafe protections to shut off the GPU miner if max temp is exceeded at lowest power, and while temperature is unknown.
- Crash safety: if MineControl crashes, miners under its control will also close.
- Optional miner auto-shutoff during user activity.
- Log aggregation including miner output.
- Archiving of logs and configuration.

## Key Un-features

- Only supports up to one GPU miner and one CPU miner at a time.
- Most temperature/power management features are GPU-only, and only one GPU can be controlled.
- Not internationalized, translated, or respectful of most Windows regional settings.
- Not very user friendly.
- Chart data and stats aren't remembered or archived, i.e. you lose them when MineControl closes.
- Can't automatically click on popups or do other interactive tasks required when launching other apps.
- Probably some allowed configurations that would make no sense or cause errors or crashes.
- Engineered badly with a bunch of logic embedded in the form code, no automated tests, etc. Sorry.

See Issues area for comprehensive list, and add to it if you find a bug.

## Requirements

- Windows 10 x64 (at least)
- .NET 5 installed
- A hardware monitor app, *if* MineControl should monitor hardware sensors and automate actions based on sensor readings.
- A GPU controller app, *if* MineControl should automate GPU power stepping for temperature control.
- A GPU and/or CPU miner app that outputs to a console window, *if* MineControl should aggregate GPU/CPU miner data and control their execution.
- MineControl must be **both** 1) started using an administrator account, and 2) **not** running elevated (i.e. admin privileges).
- UAC (User Account Control) in Windows **must** be configured low enough that the logged in user is not prompted to confirm execution of applications as admin. Otherwise automated execution of miners and apps, including power stepping, will cause a bunch of UAC prompts and not work.
- You'll probably need to exclude MineControl from virus scanning. As with most crypto mining apps, it's flagged as a false positive.

## Instructions

Note: Steps assume you have nothing already on your PC. For experienced miners, much of the setup will already be completed.
0) Please review the disclaimer and requirements.
1) Install prerequisites such as .NET 5, the hardware monitor, GPU controller, GPU and/or CPU miner you want to use. MineControl was tested using **PhoenixMiner** for GPU and **XMRig** (MoneroOcean version) for CPU, so these should work by default. TeamRedMiner was also verified as working with some RegEx customization.
2) Configure and test the miner(s) independently to make sure they run correctly and send crypto to your wallet address.
3) Configure the hardware monitor output from *inside* of the hardware monitor application. For RidesharePimpo, this means starting up with the **sensors only** option, then enabling system tray output for the metrics you want to track (like GPU memory junction temp), since MineControl relies on systray tooltips to read data. SysTray output can be enabled from the right-click menu for the sensor row. In addition, the **systray icons must be visible on the taskbar to be read by MineControl**, not hidden in the overflow area (screensaver/screen lock doesn't break anything). To be really sure, you can enable the "Always show all icons in the notification area" option in Windows taskbar settings for notification icons.
4) Configure the power profiles *inside* of the GPU controller application. You have full control over your power profiles, so you can set voltage curves or whatever you like. These should progress from lowest power (lowest temp) in profile 1 to highest power in profile 5. **Your temperature control and hash rates will only be as good as your power profiles.**
5) Extract MineControl to a folder somewhere.
6) Configure miners and applications by pointing to their executables at the top of the Config tab. Batch files are supported but you **must** set the exact application (EXE) name of the miner *without* extension in the "Process Name" cell.
7) Configure Data Tracking metrics as needed for your miners and applications, and disable metrics you don't use. This can be tedious, but may not be necessary if you're using RidesharePimpo, WhoStoleMy ButterChurner, PhoenixMiner, and xmrig. Use the "RegEx" method for any metric you need to customize. An easy way to do this is to paste example output from the application into https://regexr.com/ for easyish learning and testing. There is a "View SysTray" button in MineControl that can be used to get a current example of that output (use Ctrl+C to copy the text out of the popup).
8) Configure your target GPU temps and anything else you care about.
9) Press Start!
10) Monitor periodically for a while to make sure your config is working to your satisfaction and not doing anything bad.

## Tips

- If MineControl stops running or disappears altogether, make sure it wasn't flagged as a virus.
- Sensor data and GPU power step only update on charts when the value changes, not periodically.
- MineControl can't get data from sensors that don't exist. Some power supplies provide wattage sensor readings and some don't, for example. Your GPU may also not report a memory junction temperature.
- DO NOT run MineControl elevated (i.e. with Windows administrator privileges) if controlling GPU and CPU miner. In testing, there appeared to be a bug with PhoenixMiner that caused **massively** increased GPU temps after several minutes when MineControl was elevated, for no apparent reason. You might have better luck, but don't count on it.
- DO run MineControl while logged in with a Windows administrator account if any of the controlled applications require admin privileges, and make sure Windows is configured so that additional admin popups are not displayed when MineControl launches miners and applications. Popular hardware monitor and GPU controller software *does* require admin rights.
- There are detailed tooltips for most settings to explain what they do.
- When MineControl runs a miner, the normal miner window will *not* show, because its output is being entirely redirected to MineControl and wouldn't show in its normal window. To verify the miner is running, check it in Task Manager (Windows). To see miner log entries in MineControl, enable Keep Logs for the miner, otherwise they're just gone, yo. Note: any text coloring from the miner is not kept.
- When MineControl recognizes a log entry from a miner as input for a metric (like when a RegEx match is found), it categorizes the log entry as "Input" and appends what it found. This can be used to debug your data tracking customizations if needed. This only shows up if Keep Logs for the miner is enabled.
- Understanding the MineControl system tray icon (![SysTray](https://user-images.githubusercontent.com/91922614/138613630-875d578a-4d48-4b32-8d4f-acfb1e9fed93.png)): 
  - GPU miner is the left bar, GPU power step is the number in the middle, and CPU miner is the right bar. 
  - Miner bar colors: black = disabled by user, green = running, yellow = stopped due to user activity, gray = stopped due to schedule, orange = stopped due to unknown temp or overheating, red = stopped due to other error.
- Maintaining performance and low resource consumption:
  - Run minimized when not attended.
  - Enable clearing logs and charts at least once per day. Data built up much longer than that *will* raise CPU use and slow down the UI.
  - Avoid changing polling interval by much. Too low will use more resources, and too high will allow temps and other things to get out of hand for longer. 5000ms is a pretty good spot.
  - Disable GPU and CPU miner Keep Logs option. 
  - Data tracking: disable metrics you don't need. 
  - Data Tracking: use the UserValue method for units and algos that don't change. Reduces processing a tiny bit.
  - If resource consumption goes up after several days, restart MineControl (it may have a resource leak or accumulation issue).
- To prevent MineControl from using a GPU power profile, change the parameter for the profile (under "Command Line Params" in GPU Temperature Management) to blank or something invalid.
