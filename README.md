# Cloaked Tetris
### A demo game using the [Cloaked game development library](https://github.com/WriterZephos/Cloaked])

### This project is in its infancy. Stay tuned for updates.

## Introduction
Cloaked Tetris is a demo game for the Cloaked game development library. Its purpose is to show how to use Cloaked as well as for testing the library with a real world (albeit simple) use case. This project will continue to evolve and grow as the Cloaked library does.

## Project Setup (for contributing)

### Prerequisites
- Visual Studio Code (Visual Studio 2019 will work, but requires a different setup process)
- Git
- [.NET Core SDK](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=netcore31)

### Setup
- Make sure you have the Cloaked project setup by following instructions [here](https://github.com/WriterZephos/Cloaked).
- Clone this repo into your desired home for the project on your machine.
- Open the project folder in VS Code.
- Open a new terminal and run `dotnet restore` to install dependencies.
- In the terminal, run `dotnet build` to make sure the project builds successfully.
- Install the MonoGame Content Builder by running `dotnet tool install -g dotnet-mgcb`
- [optional] Install the MGCB Editor by running `dotnet tool install --global dotnet-mgcb-editor`
- [optional] Register the MGCB Editor by running `mgcb-editor --register`
- [optional] Install the MonoGame project templates by running `dotnet new --install MonoGame.Templates.CSharp`
- Reference the Cloaked project by running `dotnet add reference "<relative path of Cloaked csproj file>"`.
- In the terminal, run `dotnet run`.
- The game should start. Press any key to see shapes start to drop.