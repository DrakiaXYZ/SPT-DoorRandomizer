using BepInEx;
using BepInEx.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace DrakiaXYZ.Waypoints.VersionChecker
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class TarkovVersion : Attribute
    {
        private int version;
        public TarkovVersion() : this(0) { }
        public TarkovVersion(int version)
        {
            this.version = version;
        }

        public static int BuildVersion { 
            get {
                return Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(TarkovVersion), false)
                    ?.Cast<TarkovVersion>()?.FirstOrDefault()?.version ?? 0;
            }
        }

        // Make sure the version of EFT being run is the correct version, throw an exception and output log message if it isn't
        public static bool CheckEftVersion(ManualLogSource Logger, PluginInfo Info)
        {
            int currentVersion = FileVersionInfo.GetVersionInfo(BepInEx.Paths.ExecutablePath).FilePrivatePart;
            int buildVersion = BuildVersion;
            if (currentVersion != buildVersion)
            {
                Logger.LogError($"ERROR: This version of {Info.Metadata.Name} v{Info.Metadata.Version} was built for Tarkov {buildVersion}, but you are running {currentVersion}. Please download the correct plugin version.");
                return false;
            }

            return true;
        }
    }
}
