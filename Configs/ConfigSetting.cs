using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Extensions;
using Microsoft.Extensions.Configuration;

namespace Configs
{
    public static class ConfigSetting
    {
        public static IDictionary<ConfigSettingEnum, object> Configs;
        
        public static void Init(IConfigurationProvider configurationProvider)
        {
            Configs = new Dictionary<ConfigSettingEnum, object>();
            var keys = Enum.GetValues(typeof(ConfigSettingEnum));
            foreach (ConfigSettingEnum key in keys)
            {
                if (!Configs.ContainsKey(key))
                {
                    string keyConfig = key.GetDisplayName();
                    if (configurationProvider.TryGet(keyConfig, out string valueConfig))
                    {
                        object value = null;
                        var order = (ConfigSettingTypeEnum) key.GetOrder();
                        switch (order)
                        {
                            case ConfigSettingTypeEnum.Bool:
                                value = valueConfig.AsInt() == 1;
                                break;
                            case ConfigSettingTypeEnum.Int:
                                value = valueConfig.AsInt();
                                break;
                            default:
                                value = valueConfig;
                                break;
                        }
                        Configs.Add(key, value);
                    }
                }
            }
        }
    }
}