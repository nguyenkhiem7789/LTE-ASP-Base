using System.ComponentModel.DataAnnotations;

namespace Configs
{
    public enum ConfigSettingEnum
    {
        [Display(Name = "ConnectionStrings:DbConnectionString")]
        DbConnectionString,
    }
}