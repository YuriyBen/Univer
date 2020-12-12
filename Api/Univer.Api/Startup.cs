using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Univer.BLL.Services;
using Univer.DAL;
using Univer.DAL.Helpers;
using Univer.DAL.Helpers.Settings;

namespace Univer.Api
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

            // load balancer  ->  https://codeburst.io/load-balancing-an-asp-net-core-web-app-using-nginx-and-docker-66753eb08204

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddCors(options => options.AddPolicy("UniverCorsPolicy",
                             builer => builer.AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader()));

            services.AddDbContext<AppDbContext>(
                optionsAction: dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(
                    this.Configuration["ConnectionStrings:DefaultConnection"])
                    //b => b.MigrationsAssembly("PizzeriaBackend"))
            );

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var phoneVerificationSection = Configuration.GetSection("TwilioPhoneVerification");
            services.Configure<TwilioPhoneVerification>(phoneVerificationSection);

            #region JWT_Configuration
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.JWT_SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMathService, MathService>();
            services.AddScoped<IAdminService, AdminService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("UniverCorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            int[] ports = new int[] { 44326, 5000 };
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    context.Response.ContentType = "text/plain";

                    // Host info
                    var name = Dns.GetHostName(); // get container id
                    var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                    await context.Response.WriteAsync($"Host Name: {Environment.MachineName}{Environment.NewLine}");
                    await context.Response.WriteAsync($"Port: {ports.OrderBy(n => Guid.NewGuid()).Select(x => x).Take(1).SingleOrDefault()}{Environment.NewLine}");
                    await context.Response.WriteAsync(Environment.NewLine);

                    // Request method, scheme, and path
                    await context.Response.WriteAsync($"Request Method: {context.Request.Method}{Environment.NewLine}");
                    await context.Response.WriteAsync($"Request Scheme: {context.Request.Scheme}{Environment.NewLine}");
                    await context.Response.WriteAsync($"Request URL: {context.Request.GetDisplayUrl()}{Environment.NewLine}");
                    await context.Response.WriteAsync($"Request Path: {context.Request.Path}{Environment.NewLine}");

                    // Headers
                    await context.Response.WriteAsync($"Request Headers:{Environment.NewLine}");
                    foreach (var (key, value) in context.Request.Headers)
                    {
                        await context.Response.WriteAsync($"\t {key}: {value}{Environment.NewLine}");
                    }
                    await context.Response.WriteAsync(Environment.NewLine);

                    // Connection: RemoteIp
                    await context.Response.WriteAsync($"Request Remote IP: {context.Connection.RemoteIpAddress}");
                });

            });
        }
    }
}
