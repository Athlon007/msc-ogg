<#
    MSC Music Manager
    This script let's you quickly compress new release to .zip file.
#>

Write-Output "Starting the compression...";
Compress-Archive -LiteralPath `
"..\OggConverter\bin\Release\MSC Music Manager.exe", `
"..\OggConverter\bin\Release\ffmpeg.exe", `
"..\OggConverter\bin\Release\ffplay.exe", `
"..\OggConverter\bin\Release\youtube-dl.exe" `
-DestinationPath ..\mscmm.zip -Force;
Write-Output "Done!";
Start-Sleep -Seconds 1;