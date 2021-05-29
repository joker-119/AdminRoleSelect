using System;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace AdminRoleSelect.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SelectRole : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("ars.select"))
            {
                response = "Permission denied. Required: ars.select";
                return false;
            }

            if (Round.IsStarted)
            {
                response = "You may only use this command before a round has started.";
                return false;
            }

            Player player = Player.Get(((CommandSender) sender).SenderId);

            if (player == null || player.IsHost)
            {
                response = "Your player object is null. Did you send this command from the server console..?";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "You must select a role.";
                return false;
            }

            if (!Enum.TryParse(arguments.At(0), out RoleType type))
            {
                response = $"You provided an invalid role type: {arguments.At(0)}";
                return false;
            }

            if (arguments.Count == 2)
                player = Player.Get(arguments.At(1));
            if (player == null)
            {
                response = $"Defined player {arguments.At(1)} not found.";
                return false;
            }

            if (type.GetSide() != Side.Scp && Plugin.Instance.Config.AllowScpOnly)
            {
                response = "You may only select SCP roles.";
                return false;
            }

            Plugin.Instance.SelectedRoles[player] = type;
            response = $"You have selected {type} for {player.Nickname}'s starting role.";
            return false;
        }

        public string Command { get; } = "select";
        public string[] Aliases { get; } = new[] { "s" };
        public string Description { get; } = "Selects your spawn role.";
    }
}