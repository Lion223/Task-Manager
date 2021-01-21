using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Concrete;
using TaskManager.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

            // Identity configuration
            services.AddIdentity<UserModel, RoleModel>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<IdentityAppContext>();

            // Set the sign-in path
            // Otherwise, "Account/Login" path is used by default in Identity
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Signin";
            });

            // DI Identity Db
            services.AddDbContext<IdentityAppContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityAppContext"));
            });

            // DI Tasks Db
            services.AddDbContext<EFDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EFDbContext"));
            });

            // DI Repository
            services.AddScoped<ITaskRepository, EFTaskRepository>();            
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

            // Disable defined routes to use only attribute routing
            // In that case, every action needs to have the attribute in order to be accessed to
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(
                //name: "default",
                //pattern: "{controller=Tasks}/{action=List}/{id?}");
            });
        }
    }
}
