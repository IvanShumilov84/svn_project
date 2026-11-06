echo off
cd "C:\My Projects\CELDAS 200\HMI"
echo CleanUpPostBuild.bat in %cd% at %time%
rmdir /s /q de
rmdir /s /q es
rmdir /s /q fr
rmdir /s /q hu
rmdir /s /q it
rmdir /s /q ja
rmdir /s /q pt
rmdir /s /q ru
rmdir /s /q sk
rmdir /s /q sv
rmdir /s /q tr
rmdir /s /q zh-cn
rmdir /s /q zh-tw
echo This fonction has been hard coded in the Build Events
echo Post-build has cleaned files After
echo on