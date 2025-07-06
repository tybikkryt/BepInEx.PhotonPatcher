using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace PhotonPatcher
{
	public class PhotonPatcherLib
	{
		public const string pluginGuid = "tybikkryt.photonpatcher";
		public const string pluginName = "PhotonPatcher";
		public const string pluginVersion = "1.0.0.0";

		// Uncomment the fields you need
		public static string cfgAppIdRealtime;
		//public static string cfgAppIdFusion;
		//public static string cfgAppIdChat;
		//public static string cfgAppIdVoice;
		//public static string cfgAppVersion;
		//public static bool cfgUseNameServer;
		//public static string cfgFixedRegion;
		public static string cfgForceRegion;
		public static string cfgNickName;

		public static Action<string> Log;

		public static void Patch()
		{
			Harmony harmony = new(pluginGuid);
			harmony.PatchAll();
		}
	}

	[HarmonyPatch(typeof(LoadBalancingClient), "ConnectToRegionMaster")]
	public class RegionPatch
	{
		static void Prefix(ref string region)
		{
			string newRegion = PhotonPatcherLib.cfgForceRegion;
			if (!string.IsNullOrWhiteSpace(newRegion))
			{
				PhotonPatcherLib.Log($"Force change region from '{region}' to '{newRegion}'");
				region = newRegion;
			}
		}
	}

	[HarmonyPatch(typeof(PhotonNetwork), "ConnectUsingSettings", [typeof(AppSettings), typeof(bool)])]
	public class PhotonPatch
	{
		static void Prefix(AppSettings appSettings)
		{
			PhotonPatcherLib.Log("--------- Game default values ---------");

			var properties = appSettings.GetType().GetProperties();
			foreach (var property in properties)
			{
				var value = property.GetValue(appSettings);
				PhotonPatcherLib.Log($"{property.Name}: {value}");
			}

			PhotonPatcherLib.Log("--------- Game default values ---------\n");

			foreach (var property in properties)
			{
				string fieldName = $"cfg{property.Name}";

				var configField = typeof(PhotonPatcherLib).GetField(fieldName);
				if (configField == null)
				{
					PhotonPatcherLib.Log($"Field '{fieldName}' not found in PhotonPatcherLib");
					continue;
				}

				var configValue = configField.GetValue(null);
				if (configValue == null)
				{
					PhotonPatcherLib.Log($"Failed to retrieve value of '{fieldName}'");
					continue;
				}

				if (string.IsNullOrWhiteSpace(configValue.ToString()))
				{
					PhotonPatcherLib.Log($"Skipping unset value for {property.Name}");
					continue;
				}

				PhotonPatcherLib.Log($"Changing {property.Name} from '{property.GetValue(appSettings)}' to '{configValue}'");

				try
				{
					property.SetValue(appSettings, configValue);
				}
				catch (Exception ex)
				{
					PhotonPatcherLib.Log($"Error setting property '{property.Name}': {ex.Message}");
				}
			}

			PhotonPatcherLib.Log($"Changing NickName from {PhotonNetwork.NetworkingClient.NickName} to {PhotonPatcherLib.cfgNickName}");
			PhotonNetwork.NetworkingClient.NickName = PhotonPatcherLib.cfgNickName;
		}
	}
}
