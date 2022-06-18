using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using BRO.Domain;
using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.IServices;
using BRO.Infrastructure.Repositories;
using BRO.Infrastructure.Services;
using BRO.Infrastructure.Services.Settings;
using BRO.Infrastucture.Data;
using BRO.Infrastucture.Data.Initializer;
using BRO.Infrastucture.Extensions;
using BRO.UI.Extensions;
using BRO.UI.Middleware;
using BRO.UI.Utilities;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Globalization;

namespace BRO.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options=>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BROStorage")));
            services.AddIdentity<ApplicationUser, Role>(options =>
            {
                 options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmationTokenProvider";

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
            .AddTokenProvider<CustomEmailConfirmationTokenProvider<ApplicationUser>>("CustomEmailConfirmationTokenProvider");
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(options => { options.TokenLifespan = TimeSpan.FromHours(24);});
            services.Configure<DataProtectionTokenProviderOptions>(options =>{options.TokenLifespan = TimeSpan.FromHours(10);});
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/Login");
                options.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/Logout");
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/AccessDenied");
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
                options.SlidingExpiration = true;

            });
            services.ConfigureIdentity();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddTransient<IEmailService, SendGridEmailService>();
            services.AddTransient<IPaymentService, StripeService>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.Configure<EmailSettings>(Configuration.GetSection("SendGrid"));
            services.Configure<GoogleReCaptchaSettings>(Configuration.GetSection("GoogleReCaptcha"));
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            services.Configure<GoogleReCaptchaSettings>(config => 
            {
                config.SecretKey = Configuration.GetSection("GoogleReCaptcha").GetValue(typeof(string), "SecretKey") as string;
                config.SiteKey = Configuration.GetSection("GoogleReCaptcha").GetValue(typeof(string), "SiteKey") as string;
            });
        }
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); 
            containerBuilder.RegisterAutoMapper(typeof(IDependencyResolver).Assembly);         
            containerBuilder.ConfigureMediator();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IDbInitializer dbInitializer)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseMiddleware<ErrorHandlingMiddleware>();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var cultureInfo = new CultureInfo("pl-PL");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            dbInitializer.Initialize();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
