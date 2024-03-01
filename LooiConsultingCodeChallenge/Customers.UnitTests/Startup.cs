using Common.Infra;
using Customers.Application.Interfaces;
using Customers.Application.Services;
using Customers.Domain.Interfaces;
using Customers.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace Customers.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            using var log = new LoggerConfiguration().CreateLogger();
            services.AddSingleton<ILogger>(log);

            services
                .AddScoped<ICustomersRepository, CustomersRepository>()
                .AddScoped<ICustomersService, CustomersService>()
                .AddDbContext<EfContext>(opt => opt.UseInMemoryDatabase("dbTest"));

            var serviceProvider = services.BuildServiceProvider();
            var customerService = serviceProvider.GetRequiredService<ICustomersService>();
            customerService.GenerateMoqDataAsync().Wait();
        }

        //public void Configure(IApplicationBuilder app
        //    , IConfiguration configuration
        //    , IWebHostEnvironment env)
        //{
        //    try
        //    {
        //        app.UseCulture();
        //        app.UseRateLimiterConfiguration();

        //        //if (env.IsDevelopment())
        //        //{
        //        app.UseSwaggerConfig();
        //        //}

        //        if (env.IsProduction())
        //        {
        //            app.UseExceptionHandler("/error");
        //            //https://github.com/dotnet/aspnetcore/issues/11233
        //            app.Use((context, next) => {
        //                context.SetEndpoint(null);
        //                return next();
        //            });
        //            app.UseHsts();
        //        }
        //        else
        //        {
        //            app.UseExceptionHandler("/error-local-development");
        //            //https://github.com/dotnet/aspnetcore/issues/11233
        //            app.Use((context, next) => {
        //                context.SetEndpoint(null);
        //                return next();
        //            });
        //        }

        //        //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0
        //        app.UseRouting();

        //        //https://jasonwatmore.com/post/2021/05/26/net-5-api-allow-cors-requests-from-any-origin-and-with-credentials
        //        //https://stackoverflow.com/questions/42199757/enable-options-header-for-cors-on-net-core-web-api
        //        app.UseCors(x => x
        //            .AllowAnyMethod()
        //            .AllowAnyHeader()
        //            .SetIsOriginAllowed(origin => true) // allow any origin
        //            .AllowCredentials());

        //        //JWT
        //        app.UseAuthentication();
        //        app.UseAuthorization();

        //        app.UseStatusCodePages();
        //        app.UseHttpsRedirection();

        //        //https://stackoverflow.com/questions/43090718/setting-index-html-as-default-page-in-asp-net-core
        //        app.UseFileServer();

        //        app.UseHttpHeaders(env);

        //        app.UseCookiePolicy(
        //            new CookiePolicyOptions
        //            {
        //                CheckConsentNeeded = (context) => true,
        //                //ConsentCookie = null, //app.UseHangfireDashboard() error
        //                HttpOnly = HttpOnlyPolicy.Always,
        //                MinimumSameSitePolicy = SameSiteMode.Strict,
        //                OnAppendCookie = null,
        //                OnDeleteCookie = null,
        //                Secure = CookieSecurePolicy.Always
        //            });

        //        app.UseEndpoints(endpoints =>
        //        {
        //            endpoints.MapControllers();

        //            //endpoints.MapHangfireDashboard(options: new DashboardOptions
        //            //{
        //            //    Authorization = new[] { new HangfireDashboardAuthorizationFilter() },
        //            //    IgnoreAntiforgeryToken = true, //https://stackoverflow.com/questions/67749213/unable-to-refresh-the-statistics-in-hangfire-dashboard
        //            //});

        //            //SignalR
        //            //endpoints.MapHub<SignalRHub>("/SignalRHub");
        //        });

        //        //GlobalHost.HubPipeline.RequireAuthentication(); //DependencyInjection error

        //        //app.UseHangfire(configuration);
        //    }
        //    catch (Exception ex)
        //    {
        //        NLogger.Fatal(ex);
        //        throw;
        //    }
        //}
    }
}
