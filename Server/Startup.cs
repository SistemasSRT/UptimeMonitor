using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using System.Web.Cors;

[assembly: OwinStartup(typeof(Server.Startup))]

namespace Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

            var policy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true
            };

            policy.ExposedHeaders.Add("Content-Disposition");
            policy.ExposedHeaders.Add("X-Filename");
            policy.ExposedHeaders.Add("X-SRT-ErrorCode");

            app.UseCors(new Microsoft.Owin.Cors.CorsOptions()
            {
                PolicyProvider = new CorsPolicyProvider()
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });

            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("uptimeMonitor");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            

            app.MapSignalR();

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerAuthenticationOptions auth = new OAuthBearerAuthenticationOptions() { };
            //Token Consumption            
            app.UseOAuthBearerAuthentication(auth);

        }
    }
}
