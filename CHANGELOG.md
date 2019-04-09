# Preview 2.1.0.0 (09.04.2019)

NOTE: On first YouTube download, the tool will ask you to download youtube-dl. Uppon pressing Yes it will download the latest version from developer's website.

### Added:
- You can now download the files directly from YouTube
- Added preview auto updates. In order to enable them, press Shift + Updates
- "Create Desktop Shortcut" button

### Changes:
- The tool now disables some functions if conversion is in progress to prevent errors

# 2.0 (08.04.2019)

### Added:
- You can now drag and drop files to convert them quickly - either by dropping them on program's window, or it's executable
- Added music player
- You can now remove, sort and change order of songs in the tool
- Updates are now downloaded and installed automatically
- Added "check for update" button
- Added ffplay
- You can now press Shift + Check for update to force download latest version
- You can also use Shift + Open LOG folder to remove all old log files
- Same goes for Shift + Launch game - it lets you start the game with no Steam quicker
- You can now put OGG files into the conversion - instead of converting them, the program will simply rename it accordingly
- If the tool detects that it crashed before while changing order of file, it will try to fix it on next start

### Changes:
- Renamed "MSC OGG" to "MSC Music Manager"
- Huge UI changes
- New icon
- Log will no longer be created, if no files were found
- Code has been refactored
- Improvements in settings loading and saving
- Updated ffmpeg to version 4.1.1
- Lots and lots of small changes under the hood

# 1.2.1 (12.12.2017)

### Changes:
- Changed "How to use" note. It now shows all supported music formats
- Renamed "Remove MP3 files after conversion" to "Remove files after conversion"
- Code optimization
- Made some code easier to read
- Added missing comments in code

### Bug fixes:
- Version file is now deleted after detected update too
- Small UI touches (the program isn't focused on path text box at start)

### Removed:
- Obsolete code

# 1.2 (08.12.2017)

### Added:
- You can now disable checking for updates
- The tool now shows if it's updated or not in the log
- If there are over 15 files in CDs folder, the tool will ask if you wish to continue conversion
- When the program is initialized for the first time there's welcome message box appearing. Most of the controls are disabled

### Changes:
- Small changes and touches in the UI
- Optimized Log class

### Removed:
- Removed "Check for update" button
- Unused part of code

### Fixes:
- Fully fixed "NullReferenceException" bug

# 1.1 (04.12.2017)

### Added:
- Manifest file
- Added EXE only download in repository for updates
- Support for WAV, AAC, M4A and WMA file types
- The game now starts using Steam (you can change that in settings)
- Added ability to perform action by program after finishing the conversion (you can change that in settings). Default is none 
- Added more bugs to fix later

### Changes:
- Changed links in repository
- Added Settings class which loads and saves settings from registry

### Fixes:
- Fixed typos
- Fixed incorrect author in EXE info
- Dialog box when update is available

# 1.0.1 (03.12.2017)

### Added:
- You can now remove MP3 files after conversion
- Icon

### Changes:
- Small changes in ConversionLog
- Changes in menu strip

# 1.0 (02.12.2017)

### Added:
- LOG handler (they're now saved into LOG folder in program's directory)
- Conversion output as TXT file as for request of Crazysteve190
- "Open LOG folder" button into Tool menu strip
- "Open last conversion log" button into Tool menu strip
- After finished conversion, Windows sound now plays
- "Launch the game" button

### Changes:
- Moved conversion void to async void
- You cannot close program if you've started one conversion

### Bug fixes:
- Fixed bug that would allow start second conversion if one is running already
- You can now move window freely when conversion is in progress
- Fixed typos
- Temporary fix for NullReferenceException error while loading. It'll be fixed later. For now the error window will not pop up