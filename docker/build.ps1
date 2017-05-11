$nuGetPath = ".\NuGet.exe"
$msBuildPath = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

& $nuGetPath restore ..\CustodianService\CustodianService.sln

# publish website:
rm -r -force .\web\ProductLaunchWeb
& $msBuildPath ..\CustodianService\ProductLaunch.Web\ProductLaunch.Web.csproj
cp -r ..\CustodianService\ProductLaunch.Web .\web\ProductLaunchWeb

# publish message handlers:
& $msBuildPath ..\CustodianService\kCura.Hack.Client\kCura.Hack.Client.csproj /p:OutputPath=..\..\docker\save-prospect\SaveProspectHandler

# build images:
docker build -t sixeyed/msdn-web-app:v2 .\web
docker build -t sixeyed/msdn-save-handler:v2 .\save-prospect