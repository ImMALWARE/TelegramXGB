powershell.exe -command "Start-Process powershell -ArgumentList '$wc = New-Object net.webclient; $path = $env:temp + ''\TXGBInstaller'';  New-Item -ItemType Directory -Path $path; $path = $env:temp + ''\TXGBInstaller\Microsoft.VCLibs.x64.14.00.Desktop.appx''; $wc.DownloadFile(''https://github.com/ImMALWARE/TelegramXGB/releases/download/1.0/Microsoft.VCLibs.x64.14.00.Desktop.appx'', $path); Add-AppxPackage -Path $path; $path = $env:temp + ''\TXGBInstaller\Microsoft.VCLibs.x64.14.00.appx''; $wc.DownloadFile(''https://github.com/ImMALWARE/TelegramXGB/releases/download/1.0/Microsoft.VCLibs.x64.14.00.appx'', $path); Add-AppxPackage -Path $path; $path = $env:temp + ''\TXGBInstaller\Microsoft.UI.Xaml.2.8.appx''; $wc.DownloadFile(''https://github.com/ImMALWARE/TelegramXGB/releases/download/1.0/Microsoft.UI.Xaml.2.8.appx'', $path); Add-AppxPackage -Path $path; $path = $env:temp + ''\TXGBInstaller\Microsoft.NET.Native.Runtime.2.2.appx''; $wc.DownloadFile(''https://github.com/ImMALWARE/TelegramXGB/releases/download/1.0/Microsoft.NET.Native.Runtime.2.2.appx'', $path); Add-AppxPackage -Path $path; $path = $env:temp + ''\TXGBInstaller\Microsoft.NET.Native.Framework.2.2.appx''; $wc.DownloadFile(''https://github.com/ImMALWARE/TelegramXGB/releases/download/1.0/Microsoft.NET.Native.Framework.2.2.appx'', $path); Add-AppxPackage -Path $path; $path = $env:temp + ''\TXGBInstaller\TelegramXGB.cer''; $wc.DownloadFile(''https://github.com/ImMALWARE/TelegramXGB/releases/download/1.0/TelegramXGB.cer'', $path); Import-Certificate -FilePath $path -CertStoreLocation Cert:\LocalMachine\TrustedPeople; $path = $env:temp + ''\TXGBInstaller\TelegramXGB.msixbundle''; $wc.DownloadFile(''https://github.com/ImMALWARE/TelegramXGB/releases/download/1.0/TelegramXGB.msixbundle'', $path); Add-AppxPackage -Path $path; echo Installed.; pause;' -Verb RunAs"