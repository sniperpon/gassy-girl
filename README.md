# Gassy Girl

Herein lies the code for the Gassy Girl gas mileage tracker. It is implemented as a Blazor progressive web application.

## Links

* Visual Studio Code [user manual](https://code.visualstudio.com/docs).
* Open VSX [Registry](https://open-vsx.org).
* C# [support](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).
* Firefox debugging [extension](https://marketplace.visualstudio.com/items?itemName=firefox-devtools.vscode-firefox-debug).
* Blazor debugging [extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.blazorwasm-companion).
* Blazor docs [docs](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0).
* PWA Google Play [docs](https://developers.google.com/codelabs/pwa-in-play).
* Example [application](https://github.com/dotnet/blazor-samples/tree/main/6.0/BlazorSample_WebAssembly).
* HAVIT Blazor [Library](https://havit.blazor.eu).
* SQLLite with C# [instructions](https://zetcode.com/csharp/sqlite/).
* Open Iconic [list](https://useiconic.com/open#icons).
* Icons requiring attribution:
  * Fuel [pumps](https://www.iconfinder.com/icons/4059754/and_architecture_fuel_gas_gasoline_pump_station_icon).
  * Smiling [girl](https://www.iconfinder.com/icons/2903220/girl_smiley_icon).
  * Database [record](https://www.iconfinder.com/search?q=data&price=free).
  * Bar [graph](https://www.iconfinder.com/search?q=graph&price=free).
  * Import and Export [ship](https://www.iconfinder.com/search?q=export&price=free).

## Initial Setup

* Install Visual Studio Code: sudo pacman -S code
* Install Visual Studio Code Extension: Markdown Editor
* Download the two VSIX files above, install manually into Visual Studio Code.
* Install .Net Framework:
  * sudo pacman -S dotnet-runtime dotnet-sdk aspnet-runtime
  * dotnet --list-sdks
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
* Undo your change to wwwroot/index.html; if you leave it with a path other than "/", "dotnet watch" will no longer work.
* Access the application at the hosted URL in a browser. You should have the option to install it, as well as use it as a normal web site.

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
