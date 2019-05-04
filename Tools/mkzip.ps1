<#
    MSC Music Manager
    This script let's you quickly compress new release to .zip file.
#>

Write-Output "Starting the compression...";
Compress-Archive -LiteralPath `
"..\OggConverter\bin\Release\MSC Music Manager.exe"`
-DestinationPath ..\mscmm.zip -Force;
#"..\OggConverter\bin\Release\ffmpeg.exe", `
#"..\OggConverter\bin\Release\ffplay.exe", `
#"..\OggConverter\bin\Release\youtube-dl.exe" `

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