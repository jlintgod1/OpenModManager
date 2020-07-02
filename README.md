## OPEN MOD MANAGER
For A Hat in Time

![Screenshot](https://hat.ovh/omm.png)

What is this? Flying boat?
---
That's the Mod Manager but rewritten from scratch with new features:

 - Resizable window
 - Embedded console
 - "Mafia Punch:tm:" button (for killing editor when it not respond)
 - Batch-building more than one mods at once
 - Dark themed
 - Custom workshop uploader (works after int32 overflow issue!)
 - Search bar
 - Script watcher (it auto compile scripts when you modify something in it) - disabled by default
 - Mod list context menu
 ... and more!

You can find a ready to run version on the [Releases](https://github.com/mcu8/OpenModManager/releases/latest) tab
---
Now supports mod uploading via Steamworks API (no more int32 issue)!
Works only with the Steam release of the game and 64-bit operating systems.
Requires .NET Framework v4.0 (you probably have it already installed if you are used official Modding Tools before)

Installation
---
Download latest release from the [Releases](https://github.com/mcu8/OpenModManager/releases/latest) tab, extract it to some folder and run the "ModdingTools.exe" executable.

Building
---
Clone the repository and open it in the Visual Studio 2019 (or newer). You need to add original ModManager.exe as a dependency (it should be automatically detected if you’ve game installed on the drive C:).

Disclaimer
---
This tool is an unofficial tool and is not associated with Gears for Breakfast
