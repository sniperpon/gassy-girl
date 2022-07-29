# Gassy Girl

Herein lies the code for the Gassy Girl gas mileage tracker. It is implemented as a Blazor progressive web application.

## Links

- Visual Studio Code [user manual](https://code.visualstudio.com/docs).
- Open VSX [Registry](https://open-vsx.org).
- C# [support](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).
- Firefox debugging [extension](https://marketplace.visualstudio.com/items?itemName=firefox-devtools.vscode-firefox-debug)
- Blazor debugging [extention](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.blazorwasm-companion).
- Blazor docs [docs](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0).
- PWA Google Play [docs](https://developers.google.com/codelabs/pwa-in-play).

## Commands

* Install Visual Studio Code: sudo pacman -S code
* Install Visual Studio Code Extension: Markdown Editor
* Download the two VSIX files above, install manually into Visual Studio Code.
* Install .Net Framework:
  * sudo pacman -S dotnet-runtime dotnet-sdk aspnet-runtime
  * dotnet --list-sdks
* Create .Net Solution, plus Blazor PWA and test projects:
  * dotnet new sln --output GassyGirl
  * cd GassyGirl
  * dotnet new blazorwasm -o Application --pwa
  * dotnet new mstest -o ApplicationTest
  * dotnet sln add Application
  * dotnet sln add ApplicationTest
  * dotnet add ApplicationTest/ApplicationTest.csproj reference Application/Application.csproj
* Run the tests:
  * dotnet test GassyGirl/ApplicationTest/ApplicationTest.csproj

## Launch JSON File

Put these contents into a "gassy-girl/.vscode/launch.json" file:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "type": "blazorwasm",
      "name": "Launch and Debug Blazor WebAssembly Application",
      "request": "launch",
      "cwd": "${workspaceFolder}/GassyGirl/Application",
      "url": "http://localhost:5114",
      "browser": "edge"
    }
  ]
}
```
