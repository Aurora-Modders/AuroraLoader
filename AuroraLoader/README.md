![.NET Core](https://github.com/Aurora-Modders/AuroraLoader/workflows/.NET%20Core/badge.svg?branch=master)

# Requirements

None, not even Aurora. If you get error messages related to missing a .NET Core runtime, try [downloading and installing this](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.3-windows-x86-installer). If that doesn't solve the problem for you, try using the self-contained version of the release.

# Usage

Download and extract to a directory. Run the .exe. If the directory doesn't contain a copy of Aurora you'll be prompted to download one automatically.

Mods in the online registry can be viewed, installed, upgraded, and configured on the 'Manage Mods' tab. They'll be stored in a `Mods` directory.

If the 'Enable Mods' box is checked, a modified .exe can be used to run Aurora if one is selected. Any utility mods that have been checked will be launched alongside Aurora when you click "Single Player".

# Mod Registry

https://github.com/Aurora-Modders/AuroraRegistry is the primary Aurora mod registry and will soon contain instructions for setting up additional registries alongside examples. AuroraLoader is designed to work with multiple registries as defined in https://github.com/Aurora-Modders/AuroraLoader/blob/master/AuroraLoader/mirrors.ini.
