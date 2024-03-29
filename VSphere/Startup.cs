﻿using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using VSphere.Application;
using VSphere.Application.Interface;
using VSphere.AutoMapper;
using VSphere.Context;
using VSphere.Models.Identity;
using VSphere.Repositories;
using VSphere.Repositories.Base;
using VSphere.Repositories.Interfaces;
using VSphere.Repositories.Interfaces.Base;
using VSphere.Services;
using VSphere.Services.Inteface;
using VSphere.Utils;

namespace VSphere
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<VSphereContext>()
              .AddDefaultTokenProviders()
              .AddDefaultUI();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 0;
            });

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingsProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Login";
                //options.ExpireTimeSpan = TimeSpan.FromSeconds(15);
            });

            services.AddDbContext<VSphereContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("VsphereSQLConnection")));

            var customAssemblyContext = new CustomAssemblyLoadContext();
            customAssemblyContext.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));


            #region Dependency Injector

            services.AddHttpContextAccessor();
            services.AddSingleton<RequestHandler>();
            services.AddTransient<EmailHelper>();

            services.AddSingleton(typeof(IRepositoryBaseGET<>), typeof(RepositoryBaseGET<>));
            services.AddSingleton(typeof(IRepositoryBasePOST<>), typeof(RepositoryBasePOST<>));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddTransient<IVMApplication, VMApplication>();
            services.AddTransient<IVMRepository, VMRepository>();

            services.AddTransient<IHostApplication, HostApplication>();
            services.AddTransient<IHostRepository, HostRepository>();

            services.AddTransient<IServerApplication, ServerApplication>();
            services.AddTransient<IServerRepository, ServerRepository>();

            services.AddTransient<IDataStoreApplication, DataStoreApplication>();
            services.AddTransient<IDataStoreRepository, DataStoreRepository>();

            services.AddTransient<IUserApplication, UserApplication>();

            services.AddTransient<IFolderApplication, FolderApplication>();

            services.AddTransient<IResourcePoolApplication, ResourcePoolApplication>();


            services.AddTransient<IService, Service>();

            #endregion

            //var appSettingsSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            //var appSettings = appSettingsSection.Get<AppSettings>();

            #region If you would like to use JWT Authentication and you will change the .NET just to working with WebAPI

            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    //Aceitação de chamada HTTP/HTML/XML
            //    x.RequireHttpsMetadata = true;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        //Validando quem foi o emissor do Token
            //        ValidateIssuerSigningKey = true,
            //        //Inserindo a Key de autenticação 
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        //Indica que eu vou validar o emissor do token 
            //        ValidateIssuer = true,
            //        //validar que eu vou validar a audiencia 
            //        ValidateAudience = true,
            //        //Inserindo a audiencia
            //        ValidAudience = appSettings.ValidationIn,
            //        //Inserindo a emissão do token
            //        ValidIssuer = appSettings.Issuer
            //    };
            //});

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseErrorPage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                context.Context.Response.Headers.Add("Cache-Control", "no-cache")
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=MainLogin}/{id?}");
            });
        }
    }
}
