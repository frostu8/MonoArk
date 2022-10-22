using BepInEx;
using BepInEx.Logging;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using HarmonyLib;

namespace MonoArk
{
    [BepInPlugin(Plugin.GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("ChronoArk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "net.frostu8.chronoark.monoark";

        internal static ManualLogSource Log;

        private void Awake()
        {
            Plugin.Log = this.Logger;

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
