# MineControl

A **power tool** for automated temperature management, scheduling, and/or data aggregation when mining cryptocurrency. For us plebs with a single GPU and CPU running **Windows 10 x64**. It automatically keeps your chosen miner(s) and hardware running within your parameters and gives you nice data (e.g. charts, averages, kWh). This is accomplished by (optionally):
- Reading sensor data from your chosen **external** hardware monitor app.
- Setting GPU power profile through your chosen **external** GPU controller app.
- Managing execution of your chosen **external** GPU and/or CPU mining app and capturing their output for charting/logging.
- Finding relevant data in the sea of output using customizable regular expressions (some presets are provided).

MineControl is open source, mainly to foster trust.

Names of specific hardware monitor and GPU controller apps are omitted to avoid legal issues, but the default config happens to be compatible with the most popular hardware monitor app and GPU controller app. Hint: the apps support both NVIDIA and AMD GPUs, and possibly rhyme with **RidesharePimpo** and **WhoStoleMy ButterChurner**.

## License

MineControl is licensed under the Creative Commons Attribution-ShareAlike 4.0 International Public License, a.k.a. **CC BY-SA 4.0** (see License.txt).

Note: CC BY-SA is specifically required due to adaptation of certain code from StackOverflow. For attribution, see MineControl's About tab, or the source code.

## Donations

MineControl is entirely free. Crypto donations are however appreciated, and will motivate the [developer](https://github.com/smurferson1) to fix and improve things (see [Issues](https://github.com/smurferson1/MineControl/issues)). Current wallets for donation:
- Ethereum or any ERC20 token (mainnet or Polygon please): 0x709Fc86a8Ce362602038b47fB3530De167573967
- Bitcoin: 12v8VLcanjoetQbmzsBP4vw6WGL5dxmVKW
- Cardano: addr1qx2he7qs7606r3l6chrwr2calx4h5maxp66nycl6ytd78szjr620fnyg2yv0jmgvhgkvmv4e8qame086zm5f4x20cujqp34fj8
- Dogecoin: DNRzZekh73wTqU4uyEwRBjUPSAnrahxExS
- Nano: nano_13uzii9s9pxqb8kd8166xhbwdpqrbbu5kawzo11isgzq1ebtue63zktgcyog

If you want to see additional donation options added here, please contact the [developer](https://github.com/smurferson1).

## Example Screenshots

Setup in this example: 
- i7-9700k mining with XMRig (MoneroOcean)
- RTX 3090 mining with PhoenixMiner (Ethereum)
- Target GPU memory junction temps between 88C and 90C.

**Config**

![](https://user-images.githubusercontent.com/91922614/139613521-6e221017-d977-4bf8-9317-cf85be40f455.png)

**Analytics - GPU**

Note: An overnight run where max power and hash rate was achieved within target temps as the ambient (room) temperature lowered. Note the changes in power step as memory junction temp moved outside of the target range.
![](https://user-images.githubusercontent.com/91922614/139613574-a9b29ae7-a233-49c6-84be-c7d12b404ef2.png)

**Analytics - CPU**

Note: This shows chart data being split between two algorithms as XMRig auto-switched.
![](https://user-images.githubusercontent.com/91922614/139613599-fa00c8ef-2d14-4da7-972a-861d7c1fb259.png)

**Analytics - Resources**

![](https://user-images.githubusercontent.com/91922614/139613633-88ee8be8-2793-4d9a-af6b-4ab546ad816c.png)

## DISCLAIMERS

- While it is designed with care to improve your mining experience, MineControl isn't guaranteed to work correctly in all circumstances. It's in development and subject to being shite, making mistakes, or missing *YOUR* mistakes. **Use it at your own risk**.
- MineControl is *complex* and **requires thoughtful configuration**. Please configure MineControl per requirements and instructions below, and monitor until you're confident that it's doing what you want it to.
- MineControl won't do nefarious things like take your mining profits or send *anything* outside of your PC, **as long as you download it from this GitHub repo**. Feel free to inspect or compile the code yourself, but **do not** download MineControl from anywhere or anyone else, since software can easily be compromised by attackers and redistributed in a nearly identical form.
- As with most other software developed in free time for fun, there is no guarantee from the developer to provide support or updates of any kind.

## Key Features

- Drive a GPU and/or CPU miner and capture useful data from them.
- Set GPU temperature (or memory junction temperature) min and max targets, and control with up to 5 power profiles.
- Turn off pretty much anything you don't want. If you just want to control temps without driving miners, or the other way around, that'll work.
- Schedule execution of GPU and CPU miner with advanced options, for example to shut down a miner when electricity is most expensive on weekdays during summer.
- Get charts and statistics for many metrics like GPU temperature (or memory junction temperature), hash rate, power usage (W and kWh), averages, etc. Hash rate chart data can be split by algorithm when using a miner that supports algo-switching.
- Low resource consumption. Designed to use 0% GPU and <1% CPU *when minimized*.
- Capture output from external apps using regular expressions, via either system tray tooltips or console (stdout redirection).
- System tray display of GPU power level and GPU/CPU miner statuses. Example: (![SysTray](https://user-images.githubusercontent.com/91922614/138613630-875d578a-4d48-4b32-8d4f-acfb1e9fed93.png)).
- Enable GPU failsafe protections to shut off GPU miner when max temp is exceeded at lowest power step, or temperature is unknown.
- Crash safety: if MineControl crashes, miners under its control will also close to prevent controlled temps.
- Shut off either miner automatically during user activity if desired.
- Archive logs and configuration.

## Key Un-features

- _Only_ supports up to one GPU miner and one CPU miner.
- Most temperature/power management features are GPU-only, and only one GPU can be controlled.
- Not internationalized, translated, or respectful of most Windows regional settings.
- Not very user friendly. Hopefully will improve in time.
- Chart data and stats aren't remembered or archived yet, i.e. you lose them when MineControl closes.
- Can't automatically get rid of popups or do other user tasks when launching other apps.
- Probably some allowed configurations that would make no sense or cause errors or crashes.
- "Classic" Windows styling, maybe ugly. Partly for performance reasons (no GPU usage).
- Developer: Engineered badly in some ways, with a bunch of logic embedded in the form code, no automated tests, etc. Features and performance over form, thus far. It may be better rewritten, but who knows if that will happen?
- Developer: There are lots of included DLLs, with 80%+ from Microsoft.Windows.Compatibility (an obnoxious kitchen sink package), most of which aren't actually called (and _none_ of the web-related controls are called). In fact, most of the DLLs could possibly be deleted without impact, this just hasn't been tested. This extra bulk was added _solely_ for the legacy chart controls, which should probably be replaced.

See Issues area for comprehensive list, and add to it if you find a bug.

## Requirements

- Windows 10 x64 (at least).
- [.NET 5](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-desktop-5.0.11-windows-x64-installer) with desktop runtime.
- A hardware monitor app, *if* MineControl should monitor hardware sensors and automate actions based on sensor readings.
- A GPU controller app, *if* MineControl should automate GPU power stepping for temperature control.
- A GPU and/or CPU miner app that outputs to a console window, *if* MineControl should aggregate GPU/CPU miner data and control their execution.
- MineControl should be **both** 1) started while using an administrator account, and 2) **not** running elevated (i.e. admin privileges). Running elevated may cause big performance problems (see Tips section).
- UAC (User Account Control) in Windows **must** be configured low enough that Windows doesn't prompt user to confirm launching apps as admin. Otherwise automated execution of miners and apps, including power stepping, will cause a bunch of UAC prompts and **not work** while you're away.
- You'll probably need to **exclude MineControl from virus scanning**. As with most crypto mining apps, it's flagged as a false positive.

## Instructions

Note: Steps assume you have nothing already on your PC. For experienced miners, much of the setup will already be completed.
0) Please review the disclaimer and requirements.
1) Install prerequisite software. MineControl was tested using **PhoenixMiner** for GPU and **XMRig** (MoneroOcean version) for CPU, so these should work by default. TeamRedMiner was also verified as working.
2) Configure and test the miner(s) independently to make sure they run correctly and send crypto to your wallet address.
3) Configure the hardware monitor output from *inside* of the hardware monitor application. For RidesharePimpo, this means starting up with the **"sensors only"** option, then **enabling system tray output** for the metrics you want to track (like GPU memory junction temp), since MineControl relies on systray tooltips to read data. SysTray output can be enabled from the right-click menu for the sensor row. In addition, the **systray icons must be visible on the taskbar to be read by MineControl**, not hidden in the overflow area (screensaver/screen lock isn't an issue). To be really sure, you can enable the "Always show all icons in the notification area" option in Windows taskbar settings for notification icons.
4) Configure the power profiles *inside* of the GPU controller application. You have full control over your power profiles, so you can set voltage curves or whatever you like. These should progress from lowest power (lowest temp) in profile 1 to highest power in profile 5. **Your temperature control and hash rates will only be as good as your power profiles.**
5) Extract MineControl to a folder somewhere and exclude from virus scanning if needed.
6) Configure miners and applications by pointing to their executables at the top of the Config tab. Simple batch files are supported but you **must** set the exact application (EXE) name of the miner *without* extension in the "Process Name" grid cell.
7) Configure Data Tracking metrics as needed for your miners and applications, and disable metrics you don't need. There are presets for some common miners/apps (use "Load Preset" button). Manual customization may not be necessary if there are presets for your apps, or you use the default config of PhoenixMiner/XMRig + RidesharePimpo + WhoStoleMy ButterChurner. Select the "RegEx" method in the grid. See Advanced Tips section for more help.
8) Configure your target GPU temps and anything else you care about. Defaults for most other things are fine. Note: default target temps are tailored for an RTX 3090, so may be too hot for your GPU!
9) Press Start!
10) Monitor periodically for a while to make sure your config is working to your satisfaction and not doing anything bad. Check logs for errors and warnings!

## Basic Tips

- Most settings have tooltips to explain what they do, and are saved automatically. Exception: most Analytics tab (visual) settings aren't saved.
- **Don't** run MineControl elevated (like, with "Run as Administrator"), especially if controlling a GPU or CPU miner. In testing, there was a problem with PhoenixMiner that caused **massively** increased GPU temps when MineControl was elevated, for no apparent reason. You might have better luck, but don't count on it.
  - The log will tell you whether or not MineControl is running elevated at startup.
- **Do** run MineControl while logged in with a Windows administrator account so that **external** apps can be launched as admin. Popular hardware monitor and GPU controller software *does* require running as admin.
  - You can launch external apps manually, though MineControl can't capture miner data using this method.
- **Do** configure Windows so there are no additional confirmation popups from Windows when MineControl launches admin-only apps. This is usually accomplished by turning UAC down (search for "UAC" in Windows).
- If MineControl stops running or disappears, it was probably flagged as a virus. You can fix this by adding MineControl as an exclusion in your antivirus software.
- When MineControl runs a miner, the normal miner window will *not* show, because its output is being redirected to MineControl. To verify that the miner is running, check it in Task Manager (Windows).
- To see miner log output in MineControl, enable Keep Logs checkbox for the miner (Config tab). Otherwise they're just gone, yo.
  - Text coloring that would normally show up in the miner app is lost, and replaced with MineControl colors if you enable "Color Code" (Analytics tab).
- MineControl system tray icon legend (![SysTray](https://user-images.githubusercontent.com/91922614/138613630-875d578a-4d48-4b32-8d4f-acfb1e9fed93.png)): 
  - GPU miner is the left bar, GPU power step is the number in the middle, and CPU miner is the right bar. 
  - Miner bar colors: black = disabled by user, green = running, yellow = stopped due to user activity, gray = stopped due to schedule, orange = stopped due to unknown temp or overheating, red = stopped due to other error.
  - GPU power step colors: white = GPU temp control active, gray = GPU temp control inactive.

## Advanced Tips

- Data Tracking customization:
  - Custom RegEx queries: one way to do this is to paste example output from the app into https://regexr.com/ for learning and testing. To get miner output, you can select log grid rows and Ctrl+C to copy! There's a "View SysTray" button in MineControl that can be used to get an example of that output (use Ctrl+C to copy the text out of the popup).
  - When MineControl recognizes a log entry from a miner as input for a metric (like when a RegEx match is found), it categorizes the log entry as "Input" and appends what it found. This can be used to verify/debug your data tracking customizations if needed. Note: This only shows up if Keep Logs for the miner is enabled.
  - Also consider saving/sharing presets with the dev for possible inclusion in the next MineControl version! You'll get attribution, and other plebs may thank you.
  - The "UserValue" method will just output whatever you put in the Query/UserValue cell for the metric all the time.
  - Some Source/Method combinations don't make sense and aren't allowed. For example, UserValues always come from MineControl (the Query / User Value cell).
- Hardware sensor data and GPU power step only update on charts when their value changes, so there may be large gaps in the chart data if a value doesn't change.
- MineControl can't get data from sensors that don't exist. Some power supplies provide wattage sensor readings and some don't, for example. Your GPU may also not report a memory junction temperature. Sensor data grabbing was tested using a Corsair power supply and RTX 3090.
- Maintaining high performance/low resource consumption:
  - Run minimized when not attended.
  - Enable clearing logs and charts at least once per day. Data built up much longer than that *will* raise CPU use and slow down the UI.
  - Avoid changing polling interval by much. Too low will use more resources, and too high will allow temps and other things to get out of hand for longer. 5000ms is a pretty good spot.
  - Disable GPU and CPU miner Keep Logs option. 
  - Data tracking: disable metrics you don't need. 
  - Data Tracking: use the UserValue method for units and algos that don't change. Reduces processing a tiny bit.
  - If resource consumption goes up after several days, restart MineControl (it may have a resource leak or accumulation issue to be fixed).
- To prevent MineControl from using a GPU power profile, change the parameter for the profile (under "Command Line Params" in GPU Temperature Management) to blank or something invalid.
