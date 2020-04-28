_Please help us update this guide!_

# Development Requirements

You must have the .NET Core 3.1 SDK installed. In general, AuroraLoader is based around the x86 architecture for compatibility.

Our developers typically use the latest preview release of Visual Studio since it includes Windows Forms Designer functionality. However, using Visual Studio is not a requirement and changes have been released using VS Code.

# Submitting pull requests

All are encouraged to submit pull requests to this repository! We have basic CI and branch protections set up, so there's no risk of you breaking our codebase.

Requesting that we merge in a fork is perfectly acceptable as well.

# Help

Reach out [on Discord](https://discordapp.com/channels/314031775892373504/701885084646506628).

# Mod registry

https://github.com/Aurora-Modders/AuroraRegistry is the primary Aurora mod registry and will soon contain instructions for setting up additional registries alongside examples. AuroraLoader is designed to work with multiple registries as defined in https://github.com/Aurora-Modders/AuroraLoader/blob/master/AuroraLoader/mirrors.ini. Our goal is to allow members of the Aurora community to autonomously release and update mods that will show up in Aurora Loader while according with the developer's wishes and having a hell of a lot of fun. 

## Testing new mods

Create a new branch against the Aurora Registry with the new mod's information. In your local AuroraLoader solution, update `mirror.ini` to point to the branch you just created and pushed.

## Creating releases

AuroraLoader is currently published using Visual Studio 2019's built-in Publish functionality (although we'd love to set up true CD) with the following options:
![](https://media.discordapp.net/attachments/701885084646506628/704427823342944387/unknown.png?width=709&height=618)
Note that since AuroraLoader is still under active development, we tend to publish releases using the Debug configuration for convenience.

After releases are published, we manually zip up the result and upload them as GitHub releases. New AuroraLoader releases become available for automatic updates when added to https://github.com/Aurora-Modders/AuroraLoader/blob/master/updates.txt.

