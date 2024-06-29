# Changelog

All notable changes to this package will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) and this package adheres to [Semantic Versioning](https://semver.org/)

## [1.1.1] 2022-11-25
### Fixed
- Fixed a bug where adjusting the value through the editor would be restored automatically.

## [1.1.0] 2022-07-20
### Updated
- Dependencies.

### Added
- Tagging player prefs as favorites for easier tracking of pref values.
- Deleting only prefs created by Player Prefs Enhanced (Currently only works for the editor).
- Renaming keys from the editor.

## [1.0.5] 2022-06-15
### Updated
- Updated GUIDs due to conflicting GUIDs with other packages.

## [1.0.4] 2022-06-07
### Updated
- Updated assembly GUIDs for missing references.

## [1.0.3] 2022-05-19
### Updated
- Updated dependencies.

## [1.0.2] 2022-04-15
### Updated
 - EnhancedPrefs fixed an issue where Unity player prefs werent filtered from the pref overview list.

## [1.0.1] 2022-04-07
### Updated
 - PlayerPrefsWindowTextures extend to use assets and package location paths.

## [1.0.0] 2022-03-16
### Added
 - PlayerPrefsEnhanced, to replace Unity's PlayPrefs.
 - PlayerPrefDebugger, an editor window to manage your player prefs in.
 - Encryptor, used to encrypt player prefs.