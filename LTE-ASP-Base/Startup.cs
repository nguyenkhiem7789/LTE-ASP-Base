using System;
using SystemManager.Services;
using SystemManager.Shared;
using SystemRepository;
using SystemRepositorySQLImplement;
using AccountManager;
using AccountManager.Shared;
using AccountRepository;
using AccountRepositorySQLImplement;
using BaseApplication;
using BaseApplication.Implements;
using BaseApplication.Interfaces;
using Cache;
using Hangfire;
using Hangfire.MemoryStorage;
using LTE_ASP_Base.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationManager.Services;
using NotificationManager.Shared;
using NotificationRepository;

namespace LTE_ASP_Base
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BaseStartup.InitConfig(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BaseStartup.ConfigureServices(services, Configuration, servicesCollection =>
            {
                servicesCollection.AddControllersWithViews();
                // configure strongly typed settings object
                servicesCollection.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
                // configure DI for application services
                servicesCollection.AddHttpContextAccessor();
                //servicesCollection.AddScoped<IContextService, ContextS>()
                servicesCollection.AddTransient<IContextService, ContextService>();
                servicesCollection.AddTransient<IUserService, UserService>();
                servicesCollection.AddTransient<ISignalRService, SignalRService>();
                servicesCollection.AddScoped<IUserRepository, UserRepository>();
                servicesCollection.AddTransient<ICommonService, CommonService>();
                servicesCollection.AddScoped<ICommonRepository, CommonRepository>();
                servicesCollection.AddScoped<INotificationRepository, NotificationRepositorySQLImplement.NotificationRepository>();
                servicesCollection.AddSignalR();
                servicesCollection.AddSingleton<ISignalRService, SignalRService>();
                servicesCollection.AddTransient<INotificationService, NotificationService>();
                servicesCollection.AddHangfire(config =>                 
                    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseDefaultTypeSerializer()
                        .UseMemoryStorage());
                servicesCollection.AddHangfireServer();
                servicesCollection.AddDistributedRedisCache(options =>
                {
                    options.Configuration = "127.0.0.1";
                    options.InstanceName = "master";
                });
                return servicesCollection;
            }, false, true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider) {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            // custom jwt auth middleware 
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<SignalRHub>("/signalr");
            });

            app.UseHangfireDashboard();
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hangfire job!"));
            //recurringJobManager.AddOrUpdate("Run every minute", () => serviceProvider.GetService<INotificationService>().Test(), "* * * * *");
        }
    }
}