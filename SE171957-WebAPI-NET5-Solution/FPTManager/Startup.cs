using FPTManager.Entities;
using FPTManager.Models;
using FPTManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTManager
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

            services.AddControllers();

            // Add AppSettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Add DbContext
            services.AddDbContext<PRN211DemoADOContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DataSource"))
            );

            // Add Authentication
            var secretKey = Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt => {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,

                            // Sign in Token 
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                            ClockSkew = TimeSpan.Zero
                        };
                    });


            // Add Repository
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStudentService, StudentService>();



            //// Add Authentication
            //var secretKey = Configuration["AppSettings:SecretKey"];
            //var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(opt => {
            //            opt.TokenValidationParameters = new TokenValidationParameters()
            //            {
            //                ValidateIssuer = false,
            //                ValidateAudience = false,

            //                // Sign in Token
            //                ValidateIssuerSigningKey = true,
            //                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

            //                ClockSkew = TimeSpan.Zero
            //            };
            //        });

            //// Add DbContext
            //services.AddDbContext<PRN211DemoADOContext>(options => 
            //    options.UseSqlServer(Configuration.GetConnectionString("DataSource"))
            //);

            //// Add App Settings
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FPTManager", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FPTManager v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
