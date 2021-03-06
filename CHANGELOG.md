# Changelog

## 2.11 (21.11.2019)

### Added

- Added "Shuffle and Play" custom shortcut! A shortcut that first shuffles all song folders, and then starts the game
  - You can create it in the settings
- Added "Translator Mode" into settings
  - When enabled, uppon pressing F5, the current localisation will be reloaded and the current window's translation refreshed
- Added "Report an Issue" button
- Added info about the translation creator
- Locale error logs now contain system info
- You can now disable locale error logs in the settings

### Changes

- Starting the program with "wipe" will require the user's confirmation
- Code optimization

### Bug Fixes

- Fixed an error in which the program would crash if variable "args" was empty in Localisation.DumpError
- Fixed possible UI scaling issues

## 2.10.1 (14.11.2019)

### Added

- You can now click the links in changelog text box in the Settings
- You can now open current MSCMM directory by Shift + click on "Open in Explorer" button
- "downloads" folder will be now removed after playlist download ends
- "downloads" will be removed on start, if it exists

### Changes

- Changed how tabs in Settings are displayed to prevent the text overflow for some locales
- Small changes in the code

### Bug Fixes

- Fixed crash which would sometimes occure when switching to Edit tab
- Fixed about notice not translating after language change
- Fixed locale error in Error Message window

## 2.10 (11.11.2019)

### Added

- Added Finnish localisation (thanks to Daniel Pommell)
- You can now download entire playlists from YouTube!
- Added cover art file info
- You can now convert song to mono channel while converting to .OGG

### Changes

- Cover art preview loading optimization
- Replaced the default context menu for text boxes
- All downloader messages will now appear in downloader log window, instead of the global log
- Minor UI changes and touches
  - "Download" button in Downloader tab will be disabled if the text box is empty
  - "Save" button in Edit tab will be disabled if the text box is empty, or if the text in text box is the same as the current song title
  - Buttons in Recycle Bin are now context aware
  - Youtube-dl related settings will be disabled, if youtube-dl hasn't been found
  - "Edit with Audacity" button is disabled, if no song in Song List is selected

### Bug Fixes

- Fixed crash when opening Edit tab if the song list was empty
- Fixed potential crash causing bug, if the song didn't exist and the user clicked "Edit with Audacity" button
- Fixed downloaded changelog formatting

### Removed

- Removed "Open History" and "Open Log Folder" from "Tools" menu bar in main form, because it was doubling the functionality of the same function in Settings

## 2.9.1 (05.11.2019)

### Bug Fixes

- Fixed download audio quality setting not working properly

## 2.9 (04.11.2019)

### Added

- Added cover art generator! The program will automatically generate a new cover art with the list of first 11 songs on the back of the cover, and your text on CD! You can change the font type too!
- Added donate button
- Added changelog view in the Settings
- Added "view all changelogs" button
  - You can now load full changelog directly from the settings
- You can now change download quality of a song - best, average, or compressed
- Added "Edit with Audacity" button
- The program can now set the music frequency to 22050 Hz while converting

### Changes

- Small UI changes

## 2.8.1 (25.10.2019)

### Bug Fixes

- Fixed crash on minimizing

## 2.8 (22.10.2019)

### Added

- Added recycle bin!
  - From now on, all deleted files will land there first
  - You can access the recycle bin in the new Recycle Bin tab
  - Recycle bin is inside My Summer Car folder in the new "Trash" folder
- If you have the newer version downloaded than the currenty available in server, the program won't try to downgrade itself
- Added ability to disable the message which appears when user is converting song over the limit of the current folder
- Added some more bugs to fix later ;)

### Changes

- Small UI changes
- Recycle bin buttons will now be disabled, if the bin is empty
- Move To window will now appear next to the cursor
- Language can now be changed without program having to restart
- Small changes in updating of song list
- Code optimization and improvements

### Bug Fixes

- Fixed not working "Changing Order: moved '{0}' to '{1}', and '{1}' to '{0}'" translation

### Removed

- Removed "remove source file" function. The program will now delete files that have been pasted into Radio or CD folder, and not when user converted song by drag & dropping file, preventing the infinite conversion loop

## 2.7 (15.10.2019)

### Added

- Multiple changes and additions to Download!
  - Added live output of youtube-dl
  - MSCMM will try to find the song name
  - Added song download progress bar
  - Added ETA and download speed info
- New player keyboard shorcuts:
  - You can now quickly change working folder by clicking Ctrl+Up/Down arrow
  - You can use Alt+Up/Down arrows to move songs up or down
- Songs that are converted by MSCMM finding files in Radio or CDs folders, will now use file name, if there isn't one in metadata of these songs
- Added ffmpeg output. In order to enable it, go into Settings -> General -> Show ffmpeg output
- Added useful keyboard shortcuts list in Help window

### Changes

- Player will now load all .ogg files found in folder, instead of first 99 files
  - Same goes for sorting files
- Optimization of Coverter.cs
- Code optimization and improvements

### Bug Fixes

- Fixed Error message window not initializing localization script
- Fixed link validity checking for downloader
- Fixed bugs regarding stopping song playback when the song name was longer than 51 characters
- Fixed bug in which the program would not start, if the directory of the game didn't exist, or the MSC was moved. Now the program will try to find the game fodler again
- Fixed "Converted {0} file(s) in total" translation not working

## 2.6 (30.09.2019)

### Added

- Added localisation support! If you want to help with translating MSCMM to Your language, visit Steam Community discussion, or GitLab repository!
- Completly redesigned Settings
- You can now select how frequent youtube-dl checks for updates
- You can now open last log file directly in MSCMM
- Restored "start game without Steam" setting
- You can now delete multiple files at the same time
- Added new keyboard controls for song list:
  - Select all files by pressing CTRL+A
  - Delete single or all files by clicking "Delete"
  - You can now play the song by pressing Enter when focused on song list
- Added context menu to song list
- All errors related to localisations will be saved in LOG/locale_errors

### Changes

- "Create Desktop Shortcut" has been moved into Settings
- Moved "Check for Update" and "Check for youtube-dl update" into Settings
- Minor UI changes
- Code optimization and improvements

### Bug Fixes

- Fixed a bug in which MSCMM would crash on start if the user used older MSC version without CD1/2/3 support
- Fixed a bug in which MSCMM would crash if the MSCMM was converting the songs if they're dropped inside of folder
- Download button now won't work if the URL text box is empty
- Player buttons will react if the text is too big for a button
- Fixed bugged Error Message appearing in few conditions
- Fixed time of crash saving as 00:00:00
- When using quick converter, if the song name is longer than 40 characters, the text will not overflow
- Fixed quick converter throwing error while saving metafile and not saving song name
- Quick convert now saves the song name as the file name if the FFmpeg hadn't found the name in meta

### Removed

- Removed Herobrine

## 2.5.3 (22.09.2019)

### Bug Fixes

- Fixed a bug in which MSCMM would read the My Summer Car path incorrectly, if the game wasn't installed in main Steam folder

## 2.5.2 (22.09.2019)

### Bug Fixes

- Fixed a bug in which the program wouldn't start on computers using DD/MM/YYYY or MM/DD/YYYY date format
- Fixed a bug that would cause MSCMM to try and save the null value into registry causing a crash (thanks to @Patrick-van-Halm at GitLab)
- Fixed an error which occured if the GamePath value was empty

## 2.5.1 (21.09.2019)

### Added

- Added support for high DPI screens
- You can now cancel the YouTube download, partially adressing the "stuck while downloading song" bug
- youtube-dl output is now being saved into history.txt

### Changes

- Safe Mode is now refered as Restricted Mode in the code
- When no file name was found in meta, the file name's going to be used
- Now when you start the MSCMM after update, the full changelog is displayed instead of the short version
- WARNING: 'Action After Conversion' feature is now considered as obsolete and will be removed in the future updates

### Bug Fixes

- Fixed bug in which MSCMM would rename already existing track files to new name (ex. track1 to track16)
- Fixed freezing window after drag & dropping the file
- Restored disabled in earlier update error loggers

## 2.5 (09.09.2019)

### Added

- Song names are now stored in single XML file per folder. No more tons of .mscmm files!
- Your saved songs will be converted to new format on start

### Changes

- Delete button has a text instead of left arrow with X
- Small UI changes

### Bug Fixed

- Fixed few potential crash causing bugs
- Small bug fixes and improvements

## 2.4.1 (30.07.2019)

### Added

- Added crash and error tracking
- Added new error message
- When you Shift + Click Play button, the song will be played in default music player

### Bug Fixes

- Potentially fixed bug which involved songs disappearing from list while moving them up or down
- Fixed a bug in which the program would crash if you used the game version without CD1/2/3 support
- Removed minimize and maximize boxes from Move window

## 2.4 (27.07.2019)

### Added

- Added new Move To dialog which let's you choose any music folder in MSC
- You can now select multiple items in song list
- You can now move multiple files at once! Simply select few of them and click Move button
- Replaced the Radio and CD radio buttons with new combo box with all new CD folders
- Added song imported from CD to CD1 folder (please turn on the game first in order for the tool to detect the new game version)
- Now with the songs number, the songs limit per folder is also displayed
- Added tooltips
- Added "wipe" argument, which will remove all your settings. To use it, start the MSCMM like this: "./MSC Music Manager.exe wipe"

### Changes

- Changed history message for songs with undetected name
- Downloading updates and FFmpeg should be a bit faster
- Improved song list updating - now it won't flash white after each refresh
- Updates and the actual MSCMM you download are now a separate thing to avoid FFmpeg downloading problems

## 2.3 (07.05.2019)

### Added

- FFmpeg, FFplay and youtube-dl are not included anymore. Instead, they will be downloaded automatically if they're missing
- While downloading an update, the progress bar will be displayed
- When the trackTemp is detected, the program will try to also find the meta file of that trackTemp

### Changes

- Download system overhaul - now the tool won't freeze while downloading the update and it will display the progress bar
- Greatly improved loading times by better update checking algorithm

### Bug fixes

- Fixed bug with which the new update message would appear twice when using preview update
- Fixed bug in which you couldn't change order of first song in the list
- Fixed incorrect message uppon updating the preview release
- Fixed broken clone button
- Fixed a bug in which the update script wouldn't work if the directory had a space in path
- You can now switch between radio and cd while youtube-dl is updating

## 2.2.2 (03.05.2019)

### Bug Fixes

- Fixed a bug in which the MSCMM would think that the youtube-dl didn't download the song
- Fixed a bug in which the tool will freeze if the file wasn't downloaded

## 2.2.1 (02.05.2019)

### Added

- When using Quick Convert, the tool will now check for file validity
- The tool will automatically close after using Quick Convert
- Updated youtube-dl to version 2019.04.30

### Changes

- Code optimization and improvements

### Bug Fixes

- Fixed a bug in which downloading the song from Youtube wouldn't work at all

## 2.2 (29.04.2019)

### Added

- Give a chance a chance! You can now shuffle your music to always have random songs order
- Added history - all operations on the files will be saved to history.txt
- The tool will try to automatically find My Summer Car installation path - no more manual selecting it!
- Added counter which tells you how many songs you got in selected folder. If you're over the in-game limit, the counter will turn red
- You can edit song names directly in Music Manager (note: removing name will reset name back to default)
- Added "Clone" button which let's you quickly clone songs
- Last selected song list item will still be selected even after list updates
- youtube-dl will now automatically check for updates on start
- Added Help button with some truly helpful notes

### Changes

- Tiny-itty-bitty UI changes
- Lots of code optimizations and improvements

### Bug Fixes

- When moving the song from CD to Radio and vice-versa, the tool will sort the folder as expected
- If you're using preview, and the new stable release appears, the correct message appears
- Small UI fixes
- The program should load much faster when your computer is offline
- Crash Log button in Settings should check and uncheck correctly

### Removed

- LastConversion - replaced with history

## 2.1.1 (26.04.2019)

### Added

- Updated youtube-dl to version 2019.04.24

### Bug fixes

- Fixed bug in which the program would throw "Object reference not set to an instance of an object." error while trying to set game path

## 2.1 (17.04.2019)

### Added

- You can now download the files directly from YouTube - either with search term, or URL
- Added preview auto updates. In order to enable them, press Shift + Updates
- "Create Desktop Shortcut" button
- Added legal notice uppon first start
- Player can get song names from .OGG metadata
- Play, Pause and Delete buttons now have a neat icons
- The tool now automatically converts songs if it detects them in CD or Radio folder

### Changes

- Massive UI changes
- The tool now disables some functions if conversion is in progress to prevent errors
- Code optimization and improvements
- Player doesn't stop if you switch between Radio or CD

### Bug Fixes

- Fixed bug with Quick Converter which would use wrong working directory and cause false "missing ffmpeg" error
- Fixed bug in which files wouldn't be deleted after conversion

## 2.0.1 (10.04.2019)

### Bug Fixes

- Fixed bug in which the program would convert already converted files uppon pressing Convert

## 2.0 (08.04.2019)

### Added

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

### Changes

- Renamed "MSC OGG" to "MSC Music Manager"
- Huge UI changes
- New icon
- Log will no longer be created, if no files were found
- Code has been refactored
- Improvements in settings loading and saving
- Updated ffmpeg to version 4.1.1
- Lots and lots of small changes under the hood

## 1.2.1 (12.12.2017)

### Changes

- Changed "How to use" note. It now shows all supported music formats
- Renamed "Remove MP3 files after conversion" to "Remove files after conversion"
- Code optimization
- Made some code easier to read
- Added missing comments in code

### Bug fixes

- Version file is now deleted after detected update too
- Small UI touches (the program isn't focused on path text box at start)

### Removed

- Obsolete code

## 1.2 (08.12.2017)

### Added

- You can now disable checking for updates
- The tool now shows if it's updated or not in the log
- If there are over 15 files in CDs folder, the tool will ask if you wish to continue conversion
- When the program is initialized for the first time there's welcome message box appearing. Most of the controls are disabled

### Changes

- Small changes and touches in the UI
- Optimized Log class

### Removed

- Removed "Check for update" button
- Unused part of code

### Fixes

- Fully fixed "NullReferenceException" bug

## 1.1 (04.12.2017)

### Added

- Manifest file
- Added EXE only download in repository for updates
- Support for WAV, AAC, M4A and WMA file types
- The game now starts using Steam (you can change that in settings)
- Added ability to perform action by program after finishing the conversion (you can change that in settings). Default is none
- Added more bugs to fix later

### Changes

- Changed links in repository
- Added Settings class which loads and saves settings from registry

### Fixes

- Fixed typos
- Fixed incorrect author in EXE info
- Dialog box when update is available

## 1.0.1 (03.12.2017)

### Added

- You can now remove MP3 files after conversion
- Icon

### Changes

- Small changes in ConversionLog
- Changes in menu strip

## 1.0 (02.12.2017)

### Added

- LOG handler (they're now saved into LOG folder in program's directory)
- Conversion output as TXT file as for request of Crazysteve190
- "Open LOG folder" button into Tool menu strip
- "Open last conversion log" button into Tool menu strip
- After finished conversion, Windows sound now plays
- "Launch the game" button

### Changes

- Moved conversion void to async void
- You cannot close program if you've started one conversion

### Bug fixes

- Fixed bug that would allow start second conversion if one is running already
- You can now move window freely when conversion is in progress
- Fixed typos
- Temporary fix for NullReferenceException error while loading. It'll be fixed later. For now the error window will not pop up
