using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Utf8Json.Resolvers.Internal;

namespace AdminRoleSelect
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        private bool first = true;

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Log.Info($"{nameof(OnChangingRole)}: fired");
            if (!first)
                return;
            
            if (plugin.SelectedRoles.ContainsKey(ev.Player))
                ev.NewRole = plugin.SelectedRoles[ev.Player];
            else if (plugin.SelectedRoles.Values.Any(r => r == ev.NewRole && (r.GetSide() == Side.Scp || (Player.Get(r).Count() > 3 && r != RoleType.ClassD && r != RoleType.ChaosInsurgency))))
                ev.NewRole = RoleType.ClassD;
        }

        public void OnWaitingForPlayers() => first = true;
        public void OnRoundStarted() => Timing.CallDelayed(2f, () => first = false);
    }
}