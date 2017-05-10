FROM microsoft/aspnet:windowsservercore-10.0.14393.693
SHELL ["powershell"]

RUN Set-ItemProperty -Path 'HKLM:\SYSTEM\CurrentControlSet\Services\Dnscache\Parameters' \
    -Name ServerPriorityTimeLimit -Value 0 -Type DWord

RUN Remove-Website -Name 'Default Web Site'; \
    New-Item -Path 'C:\web-app' -Type Directory; \
    New-Website -Name 'web-app' -PhysicalPath 'C:\web-app' -Port 80 -Force

EXPOSE 80

COPY ProductLaunch.Web /web-app