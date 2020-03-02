
$WshShell = New-Object -comObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut("$Home\Desktop\InSys Initial Setup.lnk")

$CurrentDir = Convert-Path('.')

$Shortcut.TargetPath = "$CurrentDir\PsScrptInstaller.exe"
$Shortcut.Save()