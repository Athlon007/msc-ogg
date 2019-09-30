# C:/Users/aathl/AppData/Local/Microsoft/WindowsApps/python.exe

# MSC Music Manager Packing Zip
# This script let's you quickly compress new release to .zip file.
# It also removes logs and history from folder.
# Script version: 2.0 (27.09.2019)
#
# This file is distributed under the same license as the MSCMM is.

import os
import sys
import zipfile
from zipfile import ZipFile
from array import array
import shutil

print("=== MSCMM Packing Script 2.0 ===\n")
print("Collecting locale files...")

BASE_DIR = os.path.dirname(os.getcwd())
os.chdir(BASE_DIR + "\\OggConverter\\bin\\Release")

LOCALES = []
LOCALES = os.listdir("locales")

print("Found " + str(len(LOCALES)) + " files:\n")

FILES = []

for locale in LOCALES:
    print(" - " + locale)
    FILES.extend(["locales\\" + locale])

FILES.extend(["MSC Music Manager.exe"])
print("\nCreating mscmm_update.zip...")
UPDATER_ZIP = ZipFile(BASE_DIR + "\\mscmm_update.zip",
                      'w', zipfile.ZIP_DEFLATED)
for file in FILES:
    UPDATER_ZIP.write(file)

UPDATER_ZIP.close()

print("mscmm_update.zip created succesfully!")
print("Creating mscmm.zip...")

FILES.extend(["ffmpeg.exe"])
FILES.extend(["ffplay.exe"])
FULL_ZIP = ZipFile(BASE_DIR + "\\mscmm.zip", "w", zipfile.ZIP_DEFLATED)
for file in FILES:
    FULL_ZIP.write(file)

FULL_ZIP.close()

print("mscmm.zip created succesfully!")
print("Removing logs and history files from Release...")

if os.path.isfile("history.txt"):
    os.remove("history.txt")

if os.path.isdir("LOG"):
    shutil.rmtree('LOG')

os.chdir(os.path.dirname(os.getcwd()))
os.chdir("Debug")
print("Removing logs and history files from Debug...")

if os.path.isfile("history.txt"):
    os.remove("history.txt")

if os.path.isdir("LOG"):
    shutil.rmtree('LOG')

print("Done!")
print("Quitting...")
quit()
