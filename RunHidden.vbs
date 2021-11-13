Set objShell = WScript.CreateObject("WScript.Shell")
objShell.Run("taskkill /im EdgeMurdererRunner.exe"), 0, True
objShell.Run("""" & CreateObject("Scripting.FileSystemObject").GetParentFolderName(WScript.ScriptFullName) & "\EdgeMurdererRunner.exe"""), 0, True
