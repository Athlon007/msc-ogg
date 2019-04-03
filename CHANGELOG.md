# 2019.04.03.1715 BETA

### Added:
- You can now move songs between CD and Radio
- You can now switch off the crash logs

### Changes:
- Small UI changes
- Many changes under the hood
- Convertion log won't be saved if no files were converted

### Bug fixes:
- Fixed bug in which the tool would crash if you used player buttons
- Temporarily disabled question while removing file
- Fixed bug in which song wouldn't pause if the plyare fodler has changed

# 2019.04.02.1817 BETA

### Added:
- Added ffplay
- Added converter songs player
- You can now remove, sort and change order of songs in the tool
- You can now drag and drop files to convert them quickly. To select the destination folder, set it "Radio" or "CD" radio buttons

### Changes:
- Updated ffmpeg to version 4.1.1

### Known issues
- Deleting song causes NullReferenceException

# 2019.04.02.1130 BETA

### Added:
- FFmpeg's license info

### Changes:
- Code refactoring
- Improvements in settings loading
- Moved conversion method to separate class
- MSCOGG doesn't use NReco Video Converter anymore

# 2017.12.12.1224

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

# 2017.12.8.1120

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

# 2017.12.4.2228

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

# 2017.12.3.1522

### Added:
- You can now remove MP3 files after conversion
- Icon

### Changes:
- Small changes in ConversionLog
- Changes in menu strip

# 2017.12.2.1342

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