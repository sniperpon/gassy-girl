# Gassy Girl

Herein lies the code for the Gassy Girl gas mileage tracker. It is implemented as a Blazor progressive web application.

## Links

- Visual Studio Code [user manual](https://code.visualstudio.com/docs).
- Open VSX [Registry](https://open-vsx.org).
- C# [support](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).
- Firefox debugging [extension](https://marketplace.visualstudio.com/items?itemName=firefox-devtools.vscode-firefox-debug)
- Blazor debugging [extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.blazorwasm-companion).
- Blazor docs [docs](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0).
- PWA Google Play [docs](https://developers.google.com/codelabs/pwa-in-play).
- Example [application](https://github.com/dotnet/blazor-samples/tree/main/6.0/BlazorSample_WebAssembly)

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
* From the "GassyGirl/Client" directory, issue this command: dotnet publish -c Release
* Issue a copy command to deploy this newly-minted version: cp -R bin/Release/net6.0/publish/wwwroot/* /some/destination
* Modify the "base" tag within the index.html file in the destination; make sure its path matches the hosted URL-- for example, "/GassyGirl/".

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
