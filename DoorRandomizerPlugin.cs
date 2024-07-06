using SPT.Reflection.Patching;
using BepInEx;
using DrakiaXYZ.DoorRandomizer.VersionChecker;
using EFT;
using System;
using System.Reflection;

namespace DrakiaXYZ.DoorRandomizer
{
    [BepInPlugin("xyz.drakia.doorrandomizer", "DrakiaXYZ-DoorRandomizer", "1.4.0")]
    [BepInDependency("com.SPT.core", "3.9.0")]
    public class DoorRandomizerPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            if (!TarkovVersion.CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception($"Invalid EFT Version");
            }

            new DoorRandomizerPatch().Enable();
        }
    }

    internal class DoorRandomizerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
        }

        [PatchPrefix]
        public static void PatchPrefix()
        {
            DoorRandomizerComponent.Enable();
        }
    }
}
