using MapEditorReborn.API.Features.Objects;
using PlayerRoles;
using PluginUtils.Loader.Features;
using System.Collections.Generic;
using Exiled.API.Features;
using UnityEngine;
using MapEditorReborn.API.Features;
using PluginUtils.Plugins.SCP035.EventHandler;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features.Roles;

namespace PluginUtils.Plugins.SCP035
{
    internal class Plugin : Utils<Config>
    {
        public static Config _config;
        public override string Name => "SCP035";
        public override byte Priority => 10;

        private PlayerHandler _playerHandler;
        private ServerHandler _serverHandler;
        public static Dictionary<Player, SchematicObject> _schematicScp035;
        public override void Enable()
        {
            _playerHandler = new PlayerHandler();
            _serverHandler = new ServerHandler(this);
            _schematicScp035 = new Dictionary<Player, SchematicObject>();

            Exiled.Events.Handlers.Player.ChangingRole += _playerHandler.OnChangeRole;
            Exiled.Events.Handlers.Player.Spawning += _playerHandler.OnSpawning;
            Exiled.Events.Handlers.Player.Died += _playerHandler.OnDied;
            Exiled.Events.Handlers.Player.Left += _playerHandler.OnLeave;
            Exiled.Events.Handlers.Player.EnteringPocketDimension += _playerHandler.OnEntirePocketDimension;
            Exiled.Events.Handlers.Player.Handcuffing += _playerHandler.OnHandcuff;
            Exiled.Events.Handlers.Player.InteractingDoor += _playerHandler.OnDoorInteract;
            Exiled.Events.Handlers.Scp096.Enraging += _serverHandler.OnScpEnrage;
            Exiled.Events.Handlers.Scp096.AddingTarget += _serverHandler.OnAddingTarget;
            Exiled.Events.Handlers.Server.WaitingForPlayers += _serverHandler.OnWaitingPlayers;
            Exiled.Events.Handlers.Player.PickingUpItem += _playerHandler.OnPickUpItem;
            base.Enable();
        }
        public override void Disable()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= _playerHandler.OnChangeRole;
            Exiled.Events.Handlers.Player.Spawning -= _playerHandler.OnSpawning;
            Exiled.Events.Handlers.Player.Died -= _playerHandler.OnDied;
            Exiled.Events.Handlers.Player.Left -= _playerHandler.OnLeave;
            Exiled.Events.Handlers.Player.EnteringPocketDimension -= _playerHandler.OnEntirePocketDimension;
            Exiled.Events.Handlers.Player.Handcuffing -= _playerHandler.OnHandcuff;
            Exiled.Events.Handlers.Player.InteractingDoor -= _playerHandler.OnDoorInteract;
            Exiled.Events.Handlers.Scp096.Enraging -= _serverHandler.OnScpEnrage;
            Exiled.Events.Handlers.Scp096.AddingTarget -= _serverHandler.OnAddingTarget;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= _serverHandler.OnWaitingPlayers;
            Exiled.Events.Handlers.Player.PickingUpItem -= _playerHandler.OnPickUpItem;
            _schematicScp035 = null;
            _playerHandler = null;
            _serverHandler = null;
            base.Disable();
        }
        public static void Spawn(Player player)
        {
            var rot = player.Rotation;
            var pos = player.Position;
            player.Role.Set(RoleTypeId.Tutorial);
            player.ChangeAppearance(RoleTypeId.Scp0492, default);
            player.Position = pos;
            player.Rotation = rot;
            player.Health = Config.Health;
            player.CustomInfo = "SCP-035";
            player.EnableEffect(EffectType.Disabled);
            player.Broadcast(10, $"<color=yellow>\"Вы стали SCP-035 - Маска Одержимости.\nУ вас {Config.Health} ХП.");

            var hatObject = ObjectSpawner.SpawnSchematic("SCP035", player.CameraTransform.position + new Vector3(0, 0, 0), Quaternion.Euler(player.Rotation), new Vector3(1, 1, 1));
            hatObject.transform.parent = player.CameraTransform;

            _schematicScp035.Add(player, hatObject);
        }
        public static void Destroy(Player player)
        {
            if (player != null)
            {
                player.CustomInfo = string.Empty;
                player.Scale = new Vector3(1, 1, 1);
            }
            _schematicScp035.TryGetValue(player, out var scpObject);
            scpObject.Destroy();
            _schematicScp035.Remove(player);
        }
    }
}
