$nuGetPath = ".\NuGet.exe"
$msBuildPath = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

& $nuGetPath restore ..\ProductLaunch\ProductLaunch.sln

# publish website:
& $msBuildPath ..\ProductLaunch\ProductLaunch.Web\ProductLaunch.Web.csproj /p:OutputPath=c:\git\hack\DockerHack\docker\web\ProductLaunchWeb 
cp -r .\web\ProductLaunchWeb\_PublishedWebsites\ProductLaunch.Web .\web
rm -r -force .\web\ProductLaunchWeb

# publish message handlers:
& $msBuildPath ..\ProductLaunch\ProductLaunch.MessageHandlers.SaveProspect\ProductLaunch.MessageHandlers.SaveProspect.csproj /p:OutputPath=..\..\docker\save-prospect\SaveProspectHandler

# build images:
docker build -t sixeyed/msdn-web-app:v2 .\web
docker build -t sixeyed/msdn-save-handler:v2 .\save-prospect