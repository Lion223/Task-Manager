using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Concrete;
using TaskManager.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.WebUI
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
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            // DI Tasks Db
            services.AddScoped<ITaskRepository>(_ =>
                new EFTaskRepository(Configuration.GetConnectionString("EFDbContext")));

            // Identity configuration
            services.AddIdentity<UserModel, RoleModel>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityAppContext>();

            // DI Identity Db
            services.AddDbContext<IdentityAppContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDbContext"));
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Tasks}/{action=List}/{id?}");
            });
        }
    }
}
