using System.ComponentModel;
using Exiled.API.Interfaces;

namespace AdminRoleSelect
{
    public class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not you can only select SCP roles with the command.")]
        public bool AllowScpOnly { get; set; } = false;
    }
}