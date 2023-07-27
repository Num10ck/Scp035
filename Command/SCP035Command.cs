using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using PlayerRoles;
using System;

namespace PluginUtils.Plugins.SCP035.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SCP035Command : ICommand
    {
        public string Command { get; } = "scp035";

        public string[] Aliases { get; } = new string[] { "scp035 (id / all)" };

        public string Description { get; } = "Введите scp035 (id / all), чтобы стать Маской одержимости";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(".scp035"))
            {
                response = "У вас нет прав на использование этой команды.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Используйте: scp035 (id / all)";
                return false;
            }

            switch (arguments.At(0))
            {
                case "*":
                case "all":
                    {
                        foreach (Player pl in Player.List)
                        {
                            if (pl.Role == RoleTypeId.Spectator || pl.Role == RoleTypeId.None)
                                continue;
                            Plugin.Spawn(pl);
                        }
                        response = "<color=green>Игроки стали Маской одержимости</color>";
                        return false;
                    }
                default:
                    Player ply = Player.Get(arguments.At(0));
                    if (ply == null)
                    {
                        response = $"<color=red>Игрок не найден: {arguments.At(0)}</color>";
                        return false;
                    }

                    if (ply.IsDead)
                    {
                        response = $"<color=red>SCP035: Игрок мертв.</color>";
                        return false;
                    }

                    Plugin.Spawn(ply);

                    response = "<color=green>Игрок стал Маской одержимости</color>";
                    return false;
            }
        }
    }
}
