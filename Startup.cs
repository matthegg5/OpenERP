    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using OpenERP.Services;

namespace OpenERP
{

    public class Startup
    {
<<<<<<< HEAD
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
         services.AddDbContext<ErpDbContext.Models.OpenERPContext>(cfg =>
=======

        public void ConfigureServices(IServiceCollection services)
        {
         services.AddDbContext<ErpDbContext.DataModel.OpenERPContext>(cfg =>
>>>>>>> 5fd5afd (	new file:   Controllers/AppController.cs)
            {
                cfg.UseSqlServer("Name=OpenERPContextDb");
            }); 

<<<<<<< HEAD
            services.AddTransient<IMailService, NullMailService>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
=======
            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation()
                    .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddRazorPages();
            services.AddDistributedMemoryCache();

            

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

>>>>>>> 5fd5afd (	new file:   Controllers/AppController.cs)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

          if(env.IsDevelopment())
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

<<<<<<< HEAD
=======
            //app.UseAuthorization();

            app.UseSession();

>>>>>>> 5fd5afd (	new file:   Controllers/AppController.cs)
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
