# 2019.04.05.1806 BETA

### Bug fixes:
- When Log folder doesn't exist, it won't be created if you pressed "Open LOG folder"
- Tool now checks if game folder, or My Summer Car executable still exist on launch

# 2019.04.04.2349 BETA

### Bug fixes:
- "Get Update Now!" button now works
- "Couldn't find My Summer Car path" message when using Quick Convert now appears correctly

# 2019.04.04.2235 BETA

### Added:
- Updates are now downloaded and installed automatically
- Added "check for update" button
- Added panel which tells how many files are drag & dropped
- You can now drag & drop files on MSC OGG.exe, even if the program is not running
- You can now press Shift + Check for update to force download latest version
- You can also use Shift + Open LOG folder to remove all old log files
- Same goes for Shift + Launch game - it lets you start the game with no Steam quicker

### Changes:
- Renamed "MSC OGG" to "MSC Music Manager"
- Log text is now scrolling down automatically
- If there wasn't any song in one directory, and there was in another, the empty one will not be mentioned in last conversion log
- On drag & drop conversion, the file path will no longer be present

### Bug fixes:
- Fixed bug with song moving button
- Non music files are now converted and ignored
- Fixed potential bug, in which program could crash if the song was deleted while playing the song
- Message box when deleting the file now works
- Music now stops when you try to move or sort songs

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