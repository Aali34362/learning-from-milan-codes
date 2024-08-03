using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Walkthrough
{
    public class Functions
    {
        private readonly ILogger _logger;

        public Functions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Functions>();
        }

        [Function("HttpFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(
                AuthorizationLevel.Anonymous,
                "get", "post",
                Route = "walkthrough")]
            HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse();

            await response.WriteAsJsonAsync(new
            {
                Name = "Azure Function",
                CurrentTime = DateTime.UtcNow
            });

            return response;
        }

        [Function("CurrentTime")]
        public async Task<HttpResponseData> CurrentTime([HttpTrigger(
                AuthorizationLevel.Anonymous,
                "get",
                Route = "current-time-utc")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();

            await response.WriteAsJsonAsync(new
            {
                currentTime = DateTime.UtcNow
            });

            return response;
        }
    }
}
