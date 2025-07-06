# About BepInEx.PhotonPatcher

This plugin allows you to change Photon settings to your own such as:
- AppIdRealtime - AppId for Realtime or PUN
- AppIdFusion - AppId for Photon Fusion
- AppIdChat - AppId for Photon Chat
- AppIdVoice - AppId for Photon Voice
- AppVersion - Can be used to identify builds and will split the AppId distinct "Virtual AppIds"
- UseNameServer - If false, the app will attempt to connect to a Master Server
- FixedRegion - Can be set to any of the Photon Cloud's region names to directly connect to that region

[AppSettings Docs](https://doc-api.photonengine.com/en/pun/current/class_photon_1_1_realtime_1_1_app_settings.html)

And not for AppSettings class
- ForceRegion - Will be substituted when changing the region
- NickName - Set another player name

## Compatibility

| BepInEx ver | Mono | IL2CPP |
| ----------- | ---- | ------ |
| 5.X | ✅️ | ✖ |
| 6.X | ✅ | ✅ |
| 6.X (CoreCLR) | N/A | N/A |

Only compatible with games that use the `ConnectUsingSettings` method

## How to use

*In these steps I assume using BepInEx 6 and IL2CPP. Dont forget to change BepInEx ver and Unity scripting backend type to yours*

1. Git clone project `git clone https://github.com/tybikkryt/BepInEx.PhotonPatcher.git`
2. Uncomment the fields you need in `PhotonPatcherLib.cs` and `PhotonPatcher.BepInEx6.IL2CPP`
3. Build 2 DLLs
```
cd BepInEx.PhotonPatcher/PhotonPatcherLib
dotnet build -c release
cd ../PhotonPatcher.BepInEx6.IL2CPP
dotnet build -c release
```
4. Copy `PhotonPatcherLib.dll` and `PhotonPatcher.BepInEx6.IL2CPP.dll` to `BepInEx/plugins`
5. Run the game to create tybikkryt.photonpatcher.cfg
6. Open BepInEx/config/tybikkryt.photonpatcher.cfg and set your values.

## Issues
If the plugin does not work try this:

1. Found the `PhotonRealtime.dll` in game folder:
	- For Mono: `GameFolder/GameName_Data/Managed`
	- For IL2CPP: `GameFolder/BepInEx/interop` (Run the game with BepInEx at least once)

2. Copy and replace it into `BepInEx.PhotonPatcher/lib`
3. Build

If the plugin still does not work then try to do the same with other DLLs like:
- `PhotonUnityNetworking.dll`
- `Il2Cppmscorlib.dll`
- `Il2CppInterop.Runtime.dll`

## Thanks
Thanks to [BepInEx-PhotonRedir](https://github.com/awc21/BepInEx-PhotonRedir) for the code base
