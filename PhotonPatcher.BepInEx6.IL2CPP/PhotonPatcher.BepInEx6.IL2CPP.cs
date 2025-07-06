using BepInEx;
using BepInEx.Unity.IL2CPP;

namespace PhotonPatcher
{
	[BepInPlugin(PhotonPatcherLib.pluginGuid, PhotonPatcherLib.pluginName, PhotonPatcherLib.pluginVersion)]
	public class PhotonPatcherPlugin : BasePlugin
	{
		public override void Load()
		{
			PhotonPatcherLib.Log = Log.LogMessage;
			PhotonPatcherLib.Log("Patching...");

			// Uncomment the fields you need
			PhotonPatcherLib.cfgAppIdRealtime = Config.Bind("General", "AppIdRealtime", "").Value;
			//PhotonPatcherLib.cfgAppIdFusion = Config.Bind("General", "AppIdFusion", "").Value;
			//PhotonPatcherLib.cfgAppIdChat = Config.Bind("General", "AppIdChat", "").Value;
			//PhotonPatcherLib.cfgAppIdVoice = Config.Bind("General", "AppIdVoice", "").Value;
			//PhotonPatcherLib.cfgAppVersion = Config.Bind("General", "AppVersion", "").Value;
			//PhotonPatcherLib.cfgUseNameServer = Config.Bind("General", "UseNameServer", true).Value;
			//PhotonPatcherLib.cfgFixedRegion = Config.Bind("General", "FixedRegion", "").Value;
			PhotonPatcherLib.cfgForceRegion = Config.Bind("General", "ForceRegion", "").Value;
			PhotonPatcherLib.cfgNickName = Config.Bind("General", "Nickname", "").Value;

			PhotonPatcherLib.Patch();
			PhotonPatcherLib.Log("Patching complete");
		}
	}
}
