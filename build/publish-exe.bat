echo off
set CONFIGURATION=%1

if "%CONFIGURATION%" == "" SET CONFIGURATION = Release

cd ..\src\WarGame
dotnet publish -c "%CONFIGURATION%" -r win10-x64 -o ..\..\distributable

cd ..\..\build