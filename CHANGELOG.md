# 2017.12.x.xxxx

### Added:
- You can now disable checking for updates
- The tool now shows if it's updated or not in the log
- If there's over 15 files in CDs folder, the tool will ask if you wish to continue conversion
- Tiny touches

### Changes:
- Small changes in the UI

### Removed:
- Removed "Check for update" button

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