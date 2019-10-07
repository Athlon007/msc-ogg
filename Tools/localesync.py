# MSC Music Manager Locale Sync
# Quickly sync locale files from /OggConverter/locales/ with /OggConverter/bin/Debug/locales/ and /OggConverter/bin/Release/locales/
# Script version: 1.0 (06.10.2019)
#
# This file is distributed under the same license as the MSCMM is.

import os
import sys
from distutils.dir_util import copy_tree
import shutil
from shutil import copyfile
from array import array

BASE_DIR = os.path.dirname(os.getcwd())
os.chdir('OggConverter')

# Removes locale folders with it's contents and creates new, empty ones


def wipe_locale_dirs(dir):
    shutil.rmtree(BASE_DIR + 'OggConverter\\bin\\' + dir + '\\locales', True)
    os.mkdir(BASE_DIR + 'OggConverter\\bin\\"+ dir + "\\locales')


wipe_locale_dirs('Debug')
wipe_locale_dirs('Release')

LOCALES = []
LOCALES = os.listdir('locales')

os.remove('TemplateTranslation.po')
os.chdir('OggConverter')

for locale in LOCALES:
    print(' - ' + locale)
    copyfile('OggConverter\\locales\\' + locale,
             'OggConverter\\bin\\Debug\\locales\\' + locale)
    copyfile('OggConverter\\locales\\' + locale,
             'OggConverter\\bin\\Release\\locales\\' + locale)

print('\n\nFINISHED')
