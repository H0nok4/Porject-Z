@echo off
cd /d 
echo Compiling C# solution...
"MSBuild.exe" "DataManager\DataManager.sln" /t:Rebuild /p:Configuration=Release
echo Compilation complete.
echo Running the console application...
"DataManager\DataManager\bin\Release\net6.0\DataManager.exe" || (
    echo Your program has encountered an error. >> error.log
    pause
)
echo Execution complete.