using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using SQASpeakerService.ProviderTest.ProviderTest.Middleware.MiddlewareBase;
using Xunit;
using Xunit.Abstractions;

namespace SQASpeakerService.ProviderTest.ProviderTest
{
    public class PricingProviderTest : IDisposable
    {
        private string PactBrokerUrl { get; }

        private ITestOutputHelper OutputHelper { get; }

        private IWebHost Host { get; }

        private PactVerifierConfig PactVerifierConfig { get; }

        private const string ProviderName = "sqa-speakers-service";
        
        private const string PactUrlTemplate = "/pacts/provider/sqa-speakers-service/consumer/{0}/latest";

        private const string PactMiddlewareServiceUrl = "http://localhost:9000";

        private const string ProviderUrl = "http://localhost:5000";

        public PricingProviderTest(ITestOutputHelper output)
        {
            OutputHelper = output;
            PactVerifierConfig = new PactVerifierConfig {
                Outputters =
                    new List<IOutput> {
                        new XUnitOutput(OutputHelper)
                    },
                Verbose = true
            };
            PactBrokerUrl = "http://localhost";

            Host = WebHost.CreateDefaultBuilder()
                .UseUrls(PactMiddlewareServiceUrl)
                .UseStartup<TestStartup>()
                .Build();

            Host.Start();
        }

        [Fact]
        public void EnsurePricingApiHonoursPactWithEnergyProfile()
        {
            const string consumerServiceName = "sqa-conference-service";

            var pactUrl = string.Format(PactUrlTemplate, consumerServiceName);

            IPactVerifier pactVerifier = new PactVerifier(PactVerifierConfig);

            pactVerifier
                .ProviderState($"{PactMiddlewareServiceUrl}/provider-states")
                .ServiceProvider(ProviderName, ProviderUrl)
                .HonoursPactWith(consumerServiceName)
                .PactUri($"{PactBrokerUrl}{pactUrl}")
                .Verify(providerState: "There are speakers details available for speakers with id 1, 2 and 3");
        }

        public void Dispose()
        {
            Host.StopAsync().GetAwaiter().GetResult();
            Host.Dispose();
        }
    }
}
