using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Thon.Hotels.PactVerifier;

namespace SQASpeakerService.ProviderTest.ProviderTest.Middleware.MiddlewareBase
{
    /// <summary>
    /// This middleware works as an independent application that sets up the preconditions for the provider contract tests.
    /// It encapsulates all technical setup for the business logic setup in order to execute the provider contract tests.
    /// For this you need implement a middleware class that holds all business setup logic, e.g. <see cref="ProviderStatesMiddleware"/> 
    /// </summary>
    public class BaseProviderStateMiddleware
    {
        protected IDictionary<string, Action> ProviderStates { private get; set; }
        
        protected string ConsumerName { private get; set; }
        
        protected Action ResetMethod { private get; set; }
        
        private readonly RequestDelegate _next;

        protected BaseProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;           
        }  

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                HandleProviderStatesRequest(context);
                await context.Response.WriteAsync(string.Empty);
            }
            else
            {
                await _next(context);
            }
        }

        private void HandleProviderStatesRequest(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (!string.Equals(context.Request.Method, HttpMethod.Post.ToString(),
                    StringComparison.CurrentCultureIgnoreCase) || context.Request.Body == null) {
                return;
            }

            var jsonRequestBody = string.Empty;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                jsonRequestBody = reader.ReadToEnd();
            }

            var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

            if (providerState != null && !string.IsNullOrEmpty(providerState.State) &&
                providerState.Consumer == ConsumerName) {
                ResetMethod.Invoke();
                ProviderStates[providerState.State].Invoke();
            }
        }
    }
}
