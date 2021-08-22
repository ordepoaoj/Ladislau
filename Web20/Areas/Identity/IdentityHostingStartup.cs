using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web20.Areas.Identity.Data;
using Web20.Data;

[assembly: HostingStartup(typeof(Web20.Areas.Identity.IdentityHostingStartup))]
namespace Web20.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Web20Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Web20ContextConnection")));

                services.AddDefaultIdentity<Web20User>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<Web20Context>()
                    .AddErrorDescriber<CustomIdentityErrorDescriber>();

                
                
            });
        }
    }
}