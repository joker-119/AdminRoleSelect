using System;
using CommandSystem;

namespace AdminRoleSelect.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public sealed class AdminRoleSelect : ParentCommand
    {
        public AdminRoleSelect() => LoadGeneratedCommands();
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new SelectRole());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Valid sub commands: select RoleType\nIE: ars select Scp173";
            return false;
        }

        public override string Command { get; } = "adminroleselect";
        public override string[] Aliases { get; } = { "ars" };
        public override string Description { get; } = "Selects your spawn role.";
    }
}