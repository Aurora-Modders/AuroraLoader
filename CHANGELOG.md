# Changelog

## [0.25.3]

### Changes
- `mod.json` and AuroraLoader now support changelog files (there's a Changelog button next to the Config File button in the mod manager now)
- AuroraLoader now has a changelog as well, and there's a button for accessing it
- Aurora version targeted by the currently loaded game is shown in parenthesis (and only mods compatible with the loaded game's version are shown - but that was already the case)
- Additional message after updating Aurora telling user to create a new game in order to use the new version
- Got rid of modal dialogs prompting user to update Aurora/the loader on launch (there are a lot of cases where people want to hang back at 1.9.5 and I don't want to annoy them - IMHO the snazzy new update icons are obvious enough). This also means that the dialogs aren't re-shown every time `RefreshInstallData()` is called.
- Behavior when creating new save games is a bit more intuitive (the 'new game' button is disabled when it can't be clicked, and the user is presented with a confirmation dialog before creating a fresh DB that tells them the version of Aurora that the new game will target - in later Loader versions, we may allow them to select this manually)
- Selected executable mod is no longer reset after checking or unchecking 'Enable Poweruser Mods'
- Lists of available mods are now updated after making changes in the Mod Manager window
- (Devs) GitHub Action now builds the loader on the LTS version of .NET Core 3.1, produces a self-contained executable, and appends the built commit to the artifact's name

### Bugfixes
- Buttons for opening a mod's config & changelog files are disabled if the mod hasn't actually been installed yet
- Fixed typo in Aurora & loader update confirmation dialogs
- Confirmation dialogs are now shown when the user decides to manually update either Aurora or the loader (previously behavior was inconsistent)
- Removed an unused click handler & fixed some typos in the codebase
- Mod manager is now modal

## [0.25.2]

### Bugfixes
- Dependency and packaging tweaks

## [0.25.1]

### Bugfixes

- uninstall & close utilities on game end
- fix theme mod installation
- fix update aurora

## [0.25.0]

### Features

- UI overhaul by agm-114. Mod and game management extracted to separate windows.
- Support single-folder-per-game

### Bugfixes

- Throw exception when mod.json in root doesn't belong to AuroraLoader
- Fix 'airplane mode'

## [0.24.0]

### Features
- `mod.json` format (see example in this PR) - this should make things much more intuitive for mod authors. Individual mod versions are now first-class citizens in the backend.
- Aurora backed up to a versioned directory immediately on load & before updates
- Mod approval status and description now displayed in manage listview
- Some UI features should be slightly more responsive (i.e. more intelligent updates)
- Improve postgame player motivation
- Mod compatibility status made clearer / wildcards added

### Bugfixes
- AuroraLoader update version now visible in button / fixed in some message windows
- Window resize handle more visible
- Removed an unneeded error when a given mirror didn't contain Aurora version info
- Fixed an issue where duplicate mod.jsons could be loaded
- Tweaked cache update behavior
- Hardened cached Aurora version handling
- Stop peddisplaying incompatible mod versions that may be in the registry
- Fixed crash on launch when registry cannot be contacted
- Fixed issue allowing mods to be updated to incompatible versions
- Fixed ModVersion CanBeUpdated property

## [0.23.5]

### Bugfixes

- Fix loader update

## [0.23.4]

### Features
- Reorganized UI (almost entirely cosmetic)
- Icon added to builds and re-enabled
- App startup window is now shown on top
- Added Discord link
- Aurora is backed up before updates
- Previous 4 checkboxes ('Enable mods'/'Enable approved mods'/'Enable public mods'/'Enable poweruser mods') have been replaced with two: 'Enable mods' which enables the use of approved mods, and 'Enable poweruser mods' which enables the use of poweruser mods. Utilities like A4xCalc can be used regardless.

### Bugfixes
- Fixed old bug in the Reddit links
- Fixed a case where the 'Update' button could be clicked when the selected mod was already up to date
- Fixed several crashes related to unknown checksums / set to 1.0.0 as last resort

## [0.23.1]

### Features
- Complete UI overhaul from AGM-114. No more tabs!
- AuroraLoader now actually allows you to manage mods when you don't have an internet connection

### Bugfixes
- A litany of visible and invisible UI issues and poor practices
- Loader no longer crashes when a `mod.ini` points to a bad or nonexistent config file
- Loader no longer crashes if a mirror doesn't work / you're not connected to the internet
- Modals for both of the above with actionable information
- A 'mod download failed' modal to give the user some feedback when that happens (previously, it looked like something was happening)
