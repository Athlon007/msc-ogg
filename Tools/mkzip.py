# MSC Music Manager Packing Zip
# This script let's you quickly compress new release to .zip file.
# It also removes logs and history from folder.
# Script version: 2.1.1 (14.10.2019)
#
# This file is distributed under the same license as the MSCMM is.

import os
import sys
import zipfile
from zipfile import ZipFile
from array import array
import shutil

print("=== MSCMM Packing Script 2.1 ===\n")
print("Collecting locale files...")

BASE_DIR = os.path.dirname(os.getcwd())
os.chdir(BASE_DIR + "\\OggConverter\\bin\\Release")


def make_zip(files, zipName):
    print('Creating new zip: {0}'.format(zipName))
    NEW_ZIP = ZipFile(BASE_DIR + "\\" + zipName, 'w', zipfile.ZIP_DEFLATED)

    for file in files:
        NEW_ZIP.write(file)

    NEW_ZIP.close()


# Getting all locales
LOCALES = []
LOCALES = os.listdir("locales")

print("Found " + str(len(LOCALES)) + " files:\n")

FILES = []

for locale in LOCALES:
    print(" - " + locale)
    FILES.extend(["locales\\" + locale])

FILES.extend(["MSC Music Manager.exe"])

make_zip(FILES, 'mscmm_update.zip')

# Adding ffmpeg and ffplay for full install
FILES.extend(["ffmpeg.exe"])
FILES.extend(["ffplay.exe"])

make_zip(FILES, 'mscmm.zip')


def junk_cleaner(folder):
    print('Now cleaning: {0}'.format(folder))
    os.chdir(BASE_DIR + "\\OggConverter\\bin\\" + folder)

    if os.path.isfile("history.txt"):
        os.remove("history.txt")

    if os.path.isdir("LOG"):
        shutil.rmtree("LOG", True)

    print('Junk cleaning of {0} done!'.format(folder))


junk_cleaner('Release')
junk_cleaner('Debug')

print("Quitting...")
quit()
