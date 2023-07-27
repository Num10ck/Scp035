using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace PluginUtils.Plugins.SCP035.EventHandler
{
    public class PlayerHandler
    {
        public void OnChangeRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                Plugin.Destroy(ev.Player);
            }
        }
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                Plugin.Destroy(ev.Player);
            }
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                Plugin.Destroy(ev.Player);
            }
        }
        public void OnLeave(LeftEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                Plugin.Destroy(ev.Player);
            }
        }
        public void OnEntirePocketDimension(EnteringPocketDimensionEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnHandcuff(HandcuffingEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnDoorInteract(InteractingDoorEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-035")
            {
                switch(ev.Door.Type)
                {
                    case DoorType.GateA:
                    case DoorType.GateB:
                    case DoorType.Scp914Gate:
                        { } break;
                    default:
                        {
                            ev.IsAllowed = true;
                        }
                        break;
                }
            }
        }
        public void OnPickUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Player.CustomInfo != "SCP-035" && ev.Pickup.Serial == ServerHandler._SCP035.Serial)
            {
                Plugin.Spawn(ev.Player);
            }
        }
        
    }
}
