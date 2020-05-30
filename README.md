![.NET Core](https://github.com/Aurora-Modders/AuroraLoader/workflows/.NET%20Core/badge.svg?branch=master)

![](https://i.ibb.co/vq2T3ZL/gc.png)

# Features

- Discover and install mods from the [Aurora Registry](https://github.com/Aurora-Modders/AuroraRegistry)
- Safely update Aurora
- Manage multiple games that use different versions of Aurora
- Access community resources and file bug reports
- Play background music!

## Details

- Supports running Aurora with custom exe launchers, database modifications, and any launching number of utilities*
- Automatically detects and installs updates to both Aurora and itself
- Displays mods by type, version, Aurora version compatibility, and whether or not they've been approved by the developer
- Supports externally-hosted mirrors other than the [Aurora Registry](https://github.com/Aurora-Modders/AuroraRegistry)
- Validates mod structure and compatibility with your version of Aurora
- Allows easy access to mod config and changelog files

# Requirements

You must have the [.NET Core 3.1 x86 runtime](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.4-windows-x86-installer) installed.

# Usage

Download the [latest release](https://github.com/Aurora-Modders/AuroraLoader/releases) and extract AuroraLoader.zip to a new folder; it will automatically download a fresh copy of Aurora the first time it is run.

If you'd like AuroraLoader to manage any of your existing games of Aurora, move the folders containing them (the entire Aurora installation!) into `<AuroraLoader install dir>/Games`. You'll be able to select those games from the interface, and AuroraLoader will make sure to load your games using the version of Aurora they're designed for even after you've updated to newer versions.

# Support

Contact the developers [on Discord](https://discordapp.com/channels/314031775892373504/701885084646506628) or [Reddit](https://www.reddit.com/r/aurora4x_mods/comments/g53o3l/auroraloader/), or drop an issue or pull request directly into the repository! Note that the latest available version of AuroraLoader can always be obtained from the [Releases page](https://github.com/Aurora-Modders/AuroraLoader/releases) or by clicking the Update button within the loader itself.

# For developers

## Mod registry

https://github.com/Aurora-Modders/AuroraRegistry is the primary Aurora mod registry and will soon contain instructions for setting up additional registries alongside examples. AuroraLoader is designed to work with multiple registries as defined in https://github.com/Aurora-Modders/AuroraLoader/blob/master/AuroraLoader/mirrors.ini. Our goal is to allow members of the Aurora community to autonomously release and update mods that will show up in Aurora Loader while according with the developer's wishes and having a hell of a lot of fun.

## Creating releases

After pushing to `master` or a branch, navigate to the [Actions page](https://github.com/Aurora-Modders/AuroraLoader/actions?query=workflow%3A%22.NET+Core%22) and find the build corresponding to the change you just pushed. Download the published artifact (`AuroraLoader.zip.zip`, i.e. it contains the `Aurora.zip` file you'll be uploading).

Create a new release. If you're releasing from master, choose a valid SemVer version such as `0.24.1` and use it as your tag. If you're releasing from a branch, choose a SemVer version such as `0.24.1-rc1` and make sure you select your branch when configuring the tab. Name the release `AuroraLoader <tag>` and mark it as a prerelease if not on master.

Finally, update `AuroraLoader/mod.json` with a new version and link to the raw .zip you just uploaded.
