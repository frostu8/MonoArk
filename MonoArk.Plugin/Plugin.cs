using BepInEx;

namespace MonoArk.Plugin
{
    [BepInPlugin(Plugin.GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("ChronoArk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "net.frostu8.chronoark.monoark";

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}

