echo off
REM 
REM  Written by Patrice Dion (CEL Aerospace)  feb 2002
REM  Revised by Nadia Sangiovanni (CEL Aerospace) aug 2002 for Project 00937
REM	Add workgroup security on msaccess database
REM  Revised by Nadia Sangiovanni (CEL Aerospace) jul 2003 for Project 01081
REM	Start access application minimize so the user will not see msaccess window
REM  Revised by Sylvain Boyte (CEL Aerospace) october 2012 for Project 10726 Indonesia
REM 
REM  This file should be execute from the directory PrjExt
REM  PS : 1) The default database path sould be set to C:\My Projects\ISaGRAF Enhanced\Workbench\Prj\PrjMaster
REM          SEE in msaccess Tools->Options->General->Default Database Folder
REM       2) Make sure that MSACCESS.exe is include in the path
REM          ex : PATH = C:\WINNT;...;D:\Microsoft Office\Office
REM
REM


REM we need a delay to make sure that access is closed befor continuing the sequence
REM to do it we use the ping command with -w 3000 => wait 3 seconds for replies and -n 10 for retry 5 times

echo on


cd "C:\My Projects\CELDAS 200\Workbench"


copy DACN*.csv Csv

cd PrjExt
ftp -s:script.ftp

pause