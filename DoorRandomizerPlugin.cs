using Aki.Reflection.Patching;
using BepInEx;
using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrakiaXYZ_DoorRandomizer
{
    [BepInPlugin("xyz.drakia.doorrandomizer", "DrakiaXYZ-DoorRandomizer", "0.0.1")]
    public class DoorRandomizerPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new DoorRandomizerPatch().Enable();
        }
    }

    // I'm lazy, so we have patch class here
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
