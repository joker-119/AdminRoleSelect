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
                response = "Permission denied.";
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

            string[] args = arguments.Array;
            if (args == null || args.Length < 3)
            {
                response = "You must select a role.";
                return false;
            }

            RoleType type = RoleType.None;
            try
            {
                type = (RoleType) Enum.Parse(typeof(RoleType), args[2]);
            }
            catch (Exception)
            {
                response = $"You provided an invalid role type: {args[2]}";
                return false;
            }

            if (type.GetSide() != Side.Scp && Plugin.Instance.Config.AllowScpOnly)
            {
                response = "You may only select SCP roles.";
                return false;
            }

            if (Plugin.Instance.SelectedRoles.ContainsKey(player))
                Plugin.Instance.SelectedRoles[player] = type;
            else
                Plugin.Instance.SelectedRoles.Add(player, type);

            response = $"You have selected {type} for your spawn role.";
            return false;
        }

        public string Command { get; } = "select";
        public string[] Aliases { get; } = new[] { "s" };
        public string Description { get; } = "Selects your spawn role.";
    }
}