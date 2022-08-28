using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenERP.ErpDbContext.DataModel;
using OpenERP.Services;
using OpenERP.Data.Repositories;

namespace OpenERP
{

    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>(
                cfg =>
                {
                    //requirements for login validation
                    cfg.User.RequireUniqueEmail = true;
                    cfg.Password.RequireUppercase = true;

                })
                .AddEntityFrameworkStores<ErpDbContext.DataModel.OpenERPContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"])),
                        ValidateAudience = false
                    };
                });

            services.AddDbContext<ErpDbContext.DataModel.OpenERPContext>(cfg =>
               {
                   cfg.UseSqlServer("Name=OpenERPContextDb");
               });

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation()
                    .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddRazorPages();
            services.AddDistributedMemoryCache();

            services.AddTransient<OpenERPSeeder>();
            
            //add the implementations for the interfaces in here so dependency injection chooses the right implementation
            services.AddScoped<IRepository<Part>, PartRepository>();
            services.AddScoped<IRepository<Uom>, UomRepository>();
            services.AddScoped<IRepository<Company>, CompanyRepository>();
            services.AddLogging();



            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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
                app.UseExceptionHandler("/error");
            }
            //important that the order below remains
            //app.UseDefaultFiles(); //allows index.html to be picked up as default
            app.UseStaticFiles(); //allows the server to host html files under wwwroot folder          


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(cfg =>
            {
                cfg.MapRazorPages();
                cfg.MapControllerRoute("Default",
                "/{controller}/{action}/{id?}",
                new { controller = "App", action = "Index" });
            });
            /* app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
          {
              await context.Response.WriteAsync("Hello world!");
          }); 

            }); */

        }
    }
}
