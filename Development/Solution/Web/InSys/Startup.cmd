@ECHO off
 
IF "%ComputeEmulatorRunning%" == "true" (
    ECHO "Dev"
) ELSE (
    ECHO "Executing Starter" >> log.txt

	START PsScrptInstaller.exe "%DevStrge%"

	ECHO "Finished" >> log.txt
)


 