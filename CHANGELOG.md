# 2017.12.3.

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