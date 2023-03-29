using Comfort.Common;
using EFT;
using EFT.Interactive;
using System;
using UnityEngine;

namespace DrakiaXYZ_DoorRandomizer
{
    internal class DoorRandomizerComponent : MonoBehaviour
    {
        public void Awake()
        {
            FindObjectsOfType<Door>().ExecuteForEach(door =>
            {
                if (door.DoorState == EDoorState.Open || door.DoorState == EDoorState.Shut)
                {
                    // Check if any doors have an open/closed state, but also a lock
                    if (!string.IsNullOrEmpty(door.KeyId))
                    {
                        Console.WriteLine($"Door has open/close state, but has a key! {door.name}");
                    }

                    // Randomly open/close doors
                    if (UnityEngine.Random.Range(0, 100) < 50)
                    {
                        door.DoorState = EDoorState.Open;
                    }
                    else
                    {
                        door.DoorState = EDoorState.Shut;
                    }

                    // Trigger "OnEnable" to make sure the properties are set correctly for interaction
                    door.OnEnable();
                }
            });
        }

        public static void Enable()
        {
            if (Singleton<IBotGame>.Instantiated)
            {
                var gameWorld = Singleton<GameWorld>.Instance;
                gameWorld.GetOrAddComponent<DoorRandomizerComponent>();
            }
        }
    }
}
