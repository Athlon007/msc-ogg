# MSC Music Manager Locale Sync
# Quickly sync locale files from /OggConverter/locales/ with /OggConverter/bin/Debug/locales/ and /OggConverter/bin/Release/locales/
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
os.chdir('OggConverter')

# Removes locale folders with it's contents and creates new, empty ones


def wipe_locale_dirs(dir):
    shutil.rmtree('bin\\{0}\\locales'.format(dir), True)
    os.mkdir('bin\\{0}\\locales'.format(dir))


wipe_locale_dirs('Debug')
wipe_locale_dirs('Release')

LOCALES = []
LOCALES = os.listdir('locales')

for locale in LOCALES:
    if locale.endswith(".po"):
        print(' - ' + locale)
        copyfile('locales\\' + locale,
                 'bin\\Debug\\locales\\' + locale)
        copyfile('locales\\' + locale,
                 'bin\\Release\\locales\\' + locale)
    else:
        os.remove('locales\\' + locale)

print('\n\nFINISHED')
