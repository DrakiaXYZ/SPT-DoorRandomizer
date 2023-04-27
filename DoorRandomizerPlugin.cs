using Aki.Reflection.Patching;
using BepInEx;
using DrakiaXYZ.Waypoints.VersionChecker;
using EFT;
using System;
using System.Reflection;
using UnityEngine;

namespace DrakiaXYZ_DoorRandomizer
{
    [BepInPlugin("xyz.drakia.doorrandomizer", "DrakiaXYZ-DoorRandomizer", "1.0.2")]
    public class DoorRandomizerPlugin : BaseUnityPlugin
    {
        public static int interactiveLayer;

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
            // I don't know if the layer names can change between scenes, so to be safe, get the layer number on raid start
            DoorRandomizerPlugin.interactiveLayer = LayerMask.NameToLayer("Interactive");

            DoorRandomizerComponent.Enable();
        }
    }
}
