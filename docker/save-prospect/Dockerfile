FROM microsoft/windowsservercore:10.0.14393.693
SHELL ["powershell"]

RUN Set-ItemProperty -path 'HKLM:\SYSTEM\CurrentControlSet\Services\Dnscache\Parameters' \
    -Name ServerPriorityTimeLimit -Value 0 -Type DWord

WORKDIR /save-prospect-handler
COPY SaveProspectHandler .

CMD .\kCura.Hack.Client.exe