using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreClean.Infra.Data.Context;
using CoreClean.Domain.Models;
using CoreClean.Infra.Data.Ioc;
using CoreClean.Web.Helpers;
using CoreClean.Application.Interfaces;
using CoreClean.Application.Services;
using Microsoft.AspNetCore.Http;

namespace CoreClean.Web
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
            services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("ProjectConnection")).UseLazyLoadingProxies();

            });
  
            services.AddRepository();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Add(new ServiceDescriptor(typeof(IPhotoService), typeof(PhotoService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ICategoryService), typeof(CategoryService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ICommentService), typeof(CommentService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IUserService), typeof(UserService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IAlbumService), typeof(AlbumService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IFollowService), typeof(FollowService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ITagService), typeof(TagService), ServiceLifetime.Transient));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSignalR();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ProjectDbContext>();
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
