using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.SqlServer;

namespace LJH.RegionMonitor.WebAPI
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
            services.AddMemoryCache();
            services.AddRouting();
            services.AddMvc();
            services.AddResponseCompression(); //增加HTTP回复压缩功能
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Clock skew compensates for server time drift.
                        // We recommend 5 minutes or less:
                        ClockSkew = TimeSpan.FromMinutes(5),
                        // 强制TOKEN进行签名
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"])), //
                        RequireSignedTokens = true,
                        // Ensure the token hasn't expired:
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        // Ensure the token audience matches our audience value (default true):
                        ValidateAudience = true,
                        ValidAudience = Configuration["ValidAudience"],
                        // Ensure the token was issued by a trusted authorization server (default true):
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["ValidIssuer"]
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseResponseCompression(); //HTTP返回包压缩
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
