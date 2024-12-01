using System.Net.Http;
using System.Threading.Tasks;
using MyFunctionApp.Functions.Models;
using MyFunctionApp.Functions.Orchestrations;
using MyFunctionApp.Services.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MyFunctionApp.Functions.Triggers
{
    /// <summary>
    /// Allows for third party invocation
    /// </summary>
    public class HttpTrigger
    {
        private readonly ILoggingService _loggingService;

        public HttpTrigger(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [FunctionName(nameof(HttpTrigger))]
        public async Task<HttpResponseMessage> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            var instanceId = await starter.StartNewAsync(nameof(GreetingsOrchestrator),new GreetingsModel
            {
                Cities = new []{"Tokyo","Seattle","London"}
            });

            _loggingService.Log($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}