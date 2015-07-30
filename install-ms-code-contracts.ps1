$msiPath = "$($env:USERPROFILE)\Contracts.devlab9ts.msi"
(New-Object Net.WebClient).DownloadFile('https://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970/file/93972/17/Contracts.devlab9ts.msi', $msiPath)
cmd /c start /wait msiexec /i $msiPath /quiet
del $msiPath