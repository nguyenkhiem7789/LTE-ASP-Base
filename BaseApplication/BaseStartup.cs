
using System;
using System.Linq;
using BaseRepositories;
using Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace BaseApplication
{
    public class BaseStartup
    {
        public static void InitConfig(IConfiguration configuration)
        {
            ConfigurationRoot configurationRoot = (ConfigurationRoot) configuration;
            var configFile = "appsettings.json";
            var configurationProvider = configurationRoot.Providers
                .Where(p => p.GetType() == typeof(JsonConfigurationProvider))
                .FirstOrDefault(p => ((JsonConfigurationProvider)p).Source.Path == configFile);
            ConfigSetting.Init(configurationProvider);
        }
        
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            Func<IServiceCollection, IServiceCollection> registerServiceFunc, bool isWeb, bool isCheckAuthen)
        {
            services.AddCors();
            services.AddTransient<IDbConnectionFactory>(p =>
                new DbConnectionFactory(ConfigSettingEnum.DbConnectionString.GetConfig()));
            registerServiceFunc(services);
        }
    }
}