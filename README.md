# Gassy Girl

Herein lies the code for the Gassy Girl gas mileage tracker. It is implemented as a Blazor progressive web application.

## Links

* Visual Studio Code [user manual](https://code.visualstudio.com/docs).
* Open VSX [Registry](https://open-vsx.org).
* VS Code C# [support](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).
* VS Code Firefox debugging [extension](https://marketplace.visualstudio.com/items?itemName=firefox-devtools.vscode-firefox-debug).
* VS Code Blazor debugging [extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.blazorwasm-companion).
* Blazor [documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0).
* IndexedDB [documentation](https://javascript.info/indexeddb).
* JavaScript interop [documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-6.0#synchronous-js-interop-in-blazor-webassembly-apps).
* Radzen Chart [API](https://www.radzen.com/documentation/blazor/chart)
* PWA in Google Play [documentation](https://developers.google.com/codelabs/pwa-in-play).
* HAVIT Blazor component [Library](https://havit.blazor.eu).
* Open Iconic [list](https://useiconic.com/open#icons).

## Initial Setup

* Install Visual Studio Code: sudo pacman -S code
* Install Visual Studio Code Extension: Markdown Editor
* Download the two VSIX files above, install manually into Visual Studio Code.
* Install .Net Framework:
  * sudo pacman -S dotnet-runtime dotnet-sdk aspnet-runtime
  * dotnet --list-sdks
  * sudo dotnet workload install wasm-tools
* Create .Net Solution, plus Blazor PWA and test projects:
  * dotnet new sln --output GassyGirl
  * cd GassyGirl
  * dotnet new blazorwasm -o Client --pwa
  * dotnet new mstest -o Client.Test
  * dotnet sln add Client
  * dotnet sln add Client.Test
  * dotnet add Client.Test/Client.Test.csproj reference Client/Client.csproj
* Run the tests:
  * dotnet test GassyGirl/Client.Test/Client.Test.csproj

## Running and Debugging

* To run the application with hot reloading, run this command from the "GassyGirl/Client" directory; it will launch your default browser: dotnet watch
* To debug the app, run the "Debug Blazor WebAssembly" launch profile by pressing F5; this will launch the applicaton in Edge.
  * To stop, close the browser first, then click the "Detach" icon in Visual Studio Code.
  * Only use this when you absolutely must debug C# code: you do not get the hot reloading for some reason, so UI changes are cumbersome.
  * This also leaves rogue processes, which you need to manually clean up with the "ps ax | grep dotnet" and "kill NNNNNN" bash commands. They will appear as the one or two newest processes in the grep output.

## Publishing

* Modify the "base" tag within the wwwroot/index.html file; make sure its path matches the hosted URL-- for example, "/GassyGirl/".
* From the "GassyGirl/Client" directory, issue this command: dotnet publish -c Release
* Issue a copy command to deploy this newly-minted version: cp -R bin/Release/net6.0/publish/wwwroot/* /some/destination
* Make sure that the wwwroot/.well-known directory gets copied over too.
* Undo your change to wwwroot/index.html; if you leave it with a path other than "/", "dotnet watch" will no longer work.
* Access the application at the hosted URL in a browser. You should have the option to install it, as well as use it as a normal web site.

## Updating on Play Store

* Follow [these](https://developers.google.com/codelabs/pwa-in-play) instructions.
* Specifically, install JDK 11 from [here](https://www.oracle.com/java/technologies/downloads/#java11-mac)
  * On MacOS, the JDK path will be: /Library/Java/JavaVirtualMachines/jdk-11.0.16.1.jdk
* Install Android Studio, and let it install the Android SDK for you. Install the command line tools using the SDK Manager accessible within Android Studio's startup screen. Then, set up the SDK directory tree as described [here](https://stackoverflow.com/questions/63812717/bubblewrap-cli-shows-error-the-given-android-sdk-isnt-correct-when-bubblewrap-b)
  * On MacOS, the SDK path will be: /Users/[userId]/Library/Android/sdk
* Fill in all of the parameters. Some of the trickier ones:
  * Application id: com.exigentduality.gassygirl
  * Status bar color: #3769a5
  * Splash screen color: #444444
  * Key store location: /path/to/presigning.keystore
  * Key name and password: Check local notes
* Once build is done locally, issue this command: bubblewrap fingerprint <key from .well-known file>
* Complete the steps in the Google Play console to get the app into the various, cumbersome testing and publishing states.
* The APK and App Bundle will be signed automatically. But for posterity's sake, if ever the need arises to create a manually-signed API, this is it::
  * jarsigner -verbose -keystore C:\Users\[UserId]\.android\production.keystore -signedjar GassyGirl_signed.apk app-release-unsigned-aligned.apk exigentduality.com
  * C:\Users\[UserId]\AppData\Local\Android\Sdk\build-tools\30.0.3\zipalign -v 4 GassyGirl_signed.apk GassyGirl.apk

## Launch JSON File

Put these contents into your "gassy-girl/.vscode/launch.json" file. The Firefox attach one doesn't work for debugging, but I'm including it since I think it's very close.

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "type": "blazorwasm",
      "name": "Debug Blazor WebAssembly",
      "request": "launch",
      "cwd": "${workspaceFolder}/GassyGirl/Client",
      "url": "http://localhost:5114",
      "browser": "edge"
    },
    {
      "type": "firefox",
      "request": "attach",
      "name": "Attach to Firefox",
      "url": "http://localhost:5114",
      "webRoot": "${workspaceFolder}/GassyGirl/Client/wwwroot",
    }
  ]
}
```
