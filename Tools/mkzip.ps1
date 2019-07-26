<#
    MSC Music Manager
    This script let's you quickly compress new release to .zip file.

    Script version: 1.1 (26.07.2019)
#>

Write-Output "Starting the compression...";
Compress-Archive -LiteralPath `
"..\OggConverter\bin\Release\MSC Music Manager.exe",`
"..\OggConverter\bin\Release\ffmpeg.exe", `
"..\OggConverter\bin\Release\ffplay.exe" `
-DestinationPath ..\mscmm.zip -Force;

Compress-Archive -LiteralPath `
"..\OggConverter\bin\Release\MSC Music Manager.exe"`
-DestinationPath ..\mscmm_update.zip -Force;

Write-Output "Removing history and logs...";
if (Test-Path "..\OggConverter\bin\Release\history.txt")
{
    Remove-Item "..\OggConverter\bin\Release\history.txt"
}
if (Test-Path "..\OggConverter\bin\Debug\history.txt")
{
    Remove-Item "..\OggConverter\bin\Debug\history.txt"
}
if (Test-Path "..\OggConverter\Bin\Release\LOG")
{
    Remove-Item -Recurse "..\OggConverter\Bin\Release\LOG"
}
if (Test-Path "..\OggConverter\Bin\Debug\LOG")
{
    Remove-Item -Recurse "..\OggConverter\Bin\Debug\LOG"
}
Write-Output "Done!";
Start-Sleep -Seconds 1;