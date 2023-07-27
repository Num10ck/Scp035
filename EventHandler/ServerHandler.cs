using Exiled.Events.EventArgs.Scp096;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.API.Features;
using System;
using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Enums;
using Mirror;
using System.Linq;
using Exiled.API.Features.Pickups;
using Utf8Json.Internal.DoubleConversion;

namespace PluginUtils.Plugins.SCP035.EventHandler
{
    internal class ServerHandler
    {
        private Config _config;
        public ServerHandler(Plugin plugin) => _config = plugin.Config;
        public SchematicObject _room;
        public static Pickup _SCP035;
        public void OnWaitingPlayers()
        {
            foreach (Room room in Room.List)
            {
                if (room.Type == RoomType.Hcz079)
                {
                    if (_config.IsOpenDoors) 
                    {
                        foreach (Door door in room.Doors)
                        {
                            if (door.IsGate)
                            {
                                door.Unlock();
                                door.IsOpen = true;
                            }
                        }
                    }

                    if (_config.IsNeededDelete)
                    {
                        foreach (Pickup pickup in room.Pickups)
                        {
                            pickup.Destroy();
                        }
                    }
                }
            }

            if (_room != null)
            {
                _room.Destroy();
            }

            try
            {
                _room = ObjectSpawner.SpawnSchematic(_config.Schematic, Vector3.zero, new Quaternion(0, 0, 0, 0), new Vector3(1, 1, 1));
                var hcz079 = Room.Get(RoomType.Hcz079);
                hcz079.Color = Color.black;
                _room.transform.parent = hcz079.transform;
                _room.transform.localPosition = new Vector3(0, 0, 0);
                _room.transform.localEulerAngles = new Vector3(0, 0, 0);
                _room.transform.parent = null;

                SpawnDoor(hcz079, new Vector3(-5.47f, -3.48f, -8.5244f), new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1)); // дверь от маски
                SpawnDoor(hcz079, new Vector3(4.35f, -3.35f, -9.9067f), new Vector3(0f, 90f, 0f), new Vector3(1f, 1f, 1)); // дверь от комнаты маски
            }
            catch (Exception ex)
            {
                Log.Error("Карта не была создана. Проблема в MapEditorReborn или нет самой карты:");
                Log.Error(ex);
            }

            Room SpawnRoom = Room.Get(RoomType.Hcz079);
            _SCP035 = Pickup.Create(ItemType.SCP268);
            _SCP035 = Pickup.Spawn(_SCP035, SpawnRoom.Position, new Quaternion());
            _SCP035.Transform.localPosition = Config.Position;
            _SCP035.Transform.parent = SpawnRoom.Transform;
        }
        public void SpawnDoor(Room room, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            MapGeneration.DoorSpawnpoint prefab = null;
            prefab = UnityEngine.Object.FindObjectsOfType<MapGeneration.DoorSpawnpoint>().First(x => x.TargetPrefab.name.Contains("HCZ"));
            var door = UnityEngine.Object.Instantiate(prefab.TargetPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            door.transform.parent = room.transform;
            door.transform.localPosition = position;
            door.transform.localRotation = Quaternion.Euler(rotation);
            door.transform.localScale = scale;
            door.transform.parent = null;
            NetworkServer.Spawn(door.gameObject);
        }
        public void OnScpEnrage(EnragingEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                ev.IsAllowed = false;
            }
        }
    }
}
