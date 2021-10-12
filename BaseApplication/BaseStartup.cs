
using System;
using System.Linq;
using BaseApplication.Interfaces;
using BaseRepositories;
using Cache;
using Configs;
using Microsoft.AspNetCore.Http;
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
            // connect SQL Server
            services.AddTransient<IDbConnectionFactory>(p =>
                new DbConnectionFactory(ConfigSettingEnum.DbConnectionString.GetConfig()));
            registerServiceFunc(services);
            
            // connect redis
            var connection = new RedisConnection(ConfigSettingEnum.RedisHostIps.GetConfig(), ConfigSettingEnum.RedisPassword.GetConfig());
            connection.MakeConnection().Wait();
            services.AddSingleton<IRedisStorage>(provider => new RedisStorage(connection));
            /*services.AddTransient<IContextService>(p =>
            {
                IHttpContextAccessor httpContextAccessor = p.GetRequiredService<IHttpContextAccessor>();
                return new ContextService(httpContextAccessor, p);
            });*/
        }
    } 
}