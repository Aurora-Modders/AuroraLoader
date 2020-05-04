![.NET Core](https://github.com/Aurora-Modders/AuroraLoader/workflows/.NET%20Core/badge.svg?branch=master)

![](https://i.ibb.co/LJHr0rN/Capture.png)

# Installation Requirements

You must have the .NET Core 3.1 x86 runtime installed - download it [here](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.3-windows-x86-installer). This is a system-level prerequisite similar to a Java JRE or the dependency that Aurora itself has on the .NET Framework 4.0. We decided to publish a small executable that relies on this library rather than releasing a large (>60mb) executable without that dependency for convenience, but the latter can easily be created (let us know!).

Otherwise, there are no requirements to use or develop AuroraLoader - not even Aurora itself. In fact, extracting the AuroraLoader download zip into an empty folder is one of the most reliable ways to use it. Just download the latest release from the [Releases page](https://github.com/Aurora-Modders/AuroraLoader/releases) and run the executable. If you extract AuroraLoader to a directory that contains an existing Aurora installation, it will automatically be detected and backed up.

# Usage

Download the latest release and extract to a directory. Run the .exe. If the directory doesn't contain a copy of Aurora the latest version will be downloaded automatically. Mods in the online registry can be viewed, installed, upgraded, and configured on the 'Manage Mods' tab.

# Features

- Automatically installs, backs up, and updates both Aurora and itself
- Browse mods on the Aurora Registry by type, version, and whether or not they've been approved by the developer
- Validates mod structure and compatibility with your version of Aurora
- Supports running Aurora with custom exe launchers, database modifications, and any launching number of utilities*
- Supports configuring many mods
- Adds optional in-game music

*Using mods carries with it the risk of unintended behavior - we ask that you not submit bug reports to the developer when using mods outside of the 'Approved' category and make this clear in the UI.

# Support

Contact the developers [on Discord](https://discordapp.com/channels/314031775892373504/701885084646506628) or [Reddit](https://www.reddit.com/r/aurora4x_mods/comments/g53o3l/auroraloader/), or drop an issue or pull request directly into the repository! Note that the latest available version of AuroraLoader can always be obtained from the [Releases page](https://github.com/Aurora-Modders/AuroraLoader/releases) or by clicking the Update button within the loader itself.

# For developers

## Mod registry

https://github.com/Aurora-Modders/AuroraRegistry is the primary Aurora mod registry and will soon contain instructions for setting up additional registries alongside examples. AuroraLoader is designed to work with multiple registries as defined in https://github.com/Aurora-Modders/AuroraLoader/blob/master/AuroraLoader/mirrors.ini. Our goal is to allow members of the Aurora community to autonomously release and update mods that will show up in Aurora Loader while according with the developer's wishes and having a hell of a lot of fun.

## Creating releases

After pushing to `master` or a branch, navigate to the [Actions page](https://github.com/Aurora-Modders/AuroraLoader/actions?query=workflow%3A%22.NET+Core%22) and find the build corresponding to the change you just pushed. Download the published artifact (`AuroraLoader.zip.zip`, i.e. it contains the `Aurora.zip` file you'll be uploading).

Create a new release. If you're releasing from master, choose a valid SemVer version such as `0.24.1` and use it as your tag. If you're releasing from a branch, choose a SemVer version such as `0.24.1-rc1` and make sure you select your branch when configuring the tab. Name the release `AuroraLoader <tag>` and mark it as a prerelease if not on master.

Finally, update `AuroraLoader/mod.json` with a new version and link to the raw .zip you just uploaded.
