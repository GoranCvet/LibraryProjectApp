using System;
using LibraryProject.Data;
using LibraryProject.Domain.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(WebApp.Areas.Identity.IdentityHostingStartup))]
namespace WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => 
            {
                services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                })
                .AddDefaultUI()
               .AddEntityFrameworkStores<AppDbContext>();
               
            });
        }
    }
}