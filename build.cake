var target = Argument("target", "Default");
var targetProject = "./CreditCardScan.sln";
var nuspecFile = "./nuspec/CreditCardScan.nuspec";
var apiKey = EnvironmentVariable("NUGET_API_KEY");

var packSettings = new NuGetPackSettings();
var buildSettings = new MSBuildSettings 
{
    Verbosity = Verbosity.Verbose,
    Configuration = "Release"
};

var publishSettings = new NuGetPushSettings 
{
    ApiKey = apiKey,
    Source = "https://www.nuget.org/api/v2/package", 
};

Task("Clean")
    .Does(() => DeleteDirectory("build", recursive: true));

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => NuGetRestore(targetProject));

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => MSBuild(targetProject, buildSettings));

Task("Copy")
    .IsDependentOn("Build")
    .Does(() =>
    {
        //Setup
        EnsureDirectoryExists("build/lib/portable-net45+win+wpa81+wp80");
        EnsureDirectoryExists("build/lib/MonoAndroid10/Bootstrap");
        EnsureDirectoryExists("build/lib/Xamarin.iOS10/Bootstrap");
        EnsureDirectoryExists("build/content/MonoAndroid10/Bootstrap");
        EnsureDirectoryExists("build/content/Xamarin.iOS10/Bootstrap");

        //PCL
        CopyFiles(@"CreditCardScan\bin\Release\*.dll", @"build\lib\portable-net45+win+wpa81+wp80");

        //Android
        CopyFile(@"nuspec\CreditCardScanBootstrap.cs.pp", @"build\content\MonoAndroid10\Bootstrap\CreditCardScanBootstrap.cs.pp");
        CopyFiles(@"CreditCardScan.Droid\bin\Release\*.dll", @"build\lib\MonoAndroid10");

        //iOS
        CopyFile(@"nuspec\CreditCardScanBootstrapAction.cs.pp", @"build\content\Xamarin.iOS10\Bootstrap\CreditCardScanBootstrapAction.cs.pp");
        CopyFiles(@"CreditCardScan.iOS\bin\Release\*.dll", @"build\lib\Xamarin.iOS10");
    });

Task("Pack")
    .IsDependentOn("Copy")
    .Does(() => NuGetPack(nuspecFile, packSettings));

Task("Publish")
    .IsDependentOn("Pack")
     .Does(() => NuGetPush(GetFiles("*.nupkg").First().FullPath, publishSettings));

//Default Operation
Task("Default")
    .IsDependentOn("Publish");

RunTarget(target);