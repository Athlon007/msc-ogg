# MSC Music Manager Template Sync
# Quickly sync locale files with Template Translation
# Script version: 1.1 (14.10.2019)
#
# This file is distributed under the same license as the MSCMM is.

import os
import sys
from distutils.dir_util import copy_tree
import shutil
from shutil import copyfile
from array import array

BASE_DIR = os.path.dirname(os.getcwd())
os.chdir(BASE_DIR)

# First we want to find new content of Template compared to English (UK)


def get_file_content(file_name):
    f = open(file_name, 'r')
    return f.read()


def remove_poedit_tags(localisation_content):
    c = localisation_content.split('msgid "Game Folder: {0}"')
    c[1] = 'msgid "Game Folder: {0}"' + c[1]
    return c[1]


TEMPLATE_FILE = get_file_content('TemplateTranslation.po')
TEMPLATE_FILE = remove_poedit_tags(TEMPLATE_FILE)

ENGLISH_LOCALE = get_file_content('OggConverter\\locales\\English (UK).po')
ENGLISH_LOCALE = remove_poedit_tags(ENGLISH_LOCALE)

NEW_CONTENT = TEMPLATE_FILE.replace(ENGLISH_LOCALE, '')

if not ENGLISH_LOCALE in TEMPLATE_FILE:
    print('Template file does not contain some strings as English (UK) does. Fix that first.\n')
    print("\n\n========================================")
    DIFFERENCE = ""
    TEMPLATE_ARRAY = TEMPLATE_FILE.splitlines()
    ENGLISH_ARRAY = ENGLISH_LOCALE.splitlines()
    for i in ENGLISH_ARRAY:
        if i not in TEMPLATE_ARRAY:
            DIFFERENCE += i + "\n"

    print("Difference in English.py:\n\n" + DIFFERENCE)
    print("\n\n========================================")
    DIFFERENCE = ""
    for i in TEMPLATE_ARRAY:
        if i not in ENGLISH_ARRAY:
            DIFFERENCE += i + "\n"

    print("\n\nDifference in TemplateTranslation.py:\n\n" + DIFFERENCE)
    quit()

print('New translations: ' + NEW_CONTENT + '\n\nLocales:\n')

os.chdir('OggConverter')
LOCALES = []
LOCALES = os.listdir('locales')

for locale in LOCALES:
    if locale.endswith(".po"):
        print(' - ' + locale)
        locale_file = open("locales\\" + locale, 'a')
        locale_file.write(NEW_CONTENT)
    else:
        os.remove("locales\\" + locale)

print('\n\nSUCCESSFULLY FINISHED')
