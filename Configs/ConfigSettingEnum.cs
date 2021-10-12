using System.ComponentModel.DataAnnotations;

namespace Configs
{
    public enum ConfigSettingEnum
    {
        [Display(Name = "ConnectionStrings:DbConnectionString")]
        DbConnectionString,
        [Display(Name = "RedisHostIps")]
        RedisHostIps,
        [Display(Name = "RedisPassword")]
        RedisPassword
    }
}