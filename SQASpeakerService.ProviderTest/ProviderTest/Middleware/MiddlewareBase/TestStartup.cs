using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SQASpeakerService.ProviderTest.ProviderTest.Middleware.MiddlewareBase
{
    /// <summary>
    /// This startup is responsible for starting the middleware that handles the creation of preconditions needed
    /// to verify the expected behavior of the provider service.
    /// </summary>
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ProviderStatesMiddleware>();
            app.UseMvc();
        }

    }
}
