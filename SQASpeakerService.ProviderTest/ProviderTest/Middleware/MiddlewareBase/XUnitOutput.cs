using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace SQASpeakerService.ProviderTest.ProviderTest.Middleware.MiddlewareBase
{
    /// <summary>
    /// This is a customization of the output behavior in order to make all events happening during the
    /// execution of the provider tests visible on the command line.
    /// </summary>
    public class XUnitOutput : IOutput
    {
            private readonly ITestOutputHelper _output;

            public XUnitOutput(ITestOutputHelper output)
            {
                _output = output;
            }

            public void WriteLine(string line)
            {
                _output.WriteLine(line);
            }
    }
}
