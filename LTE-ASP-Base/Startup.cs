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
using LTE_ASP_Base.Helpers;
using LTE_ASP_Base.RMS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                servicesCollection.AddScoped<IUserService, UserService>();
                servicesCollection.AddTransient<IUserRepository, UserRepository>();
                servicesCollection.AddScoped<ICommonService, CommonService>();
                servicesCollection.AddScoped<ICommonRepository, CommonRepository>();
                servicesCollection.AddSignalR();
                servicesCollection.AddSingleton<INotificationService, NotificationService>();
                return servicesCollection;
            }, false, true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
                endpoints.MapHub<NotificationHub>("/signalr");
            });
        }
    }
}