# Shell script that packages Monoark to upload to Thunderstore.
OLD_PWD=$(pwd)
BUILD_CONFIG="${BUILD_CONFIG:=Release}"
OUT_DIR="./.thunderstore"

# Compile projects
dotnet build -c $BUILD_CONFIG MonoArk
dotnet build -c $BUILD_CONFIG MonoArk.Preload

# Package into zip file
cd $OUT_DIR/build
mkdir -p BepInEx/plugins BepInEx/patchers
cp $OLD_PWD/MonoArk/bin/$BUILD_CONFIG/net35/MonoArk.dll \
    BepInEx/plugins/MonoArk.dll
cp $OLD_PWD/MonoArk.Preload/bin/$BUILD_CONFIG/net35/MonoArk.Preload.dll \
    BepInEx/patchers/MonoArk.Preload.dll
cp $OLD_PWD/icon.png $OLD_PWD/manifest.json $OLD_PWD/README.md .
zip -r ../package *

