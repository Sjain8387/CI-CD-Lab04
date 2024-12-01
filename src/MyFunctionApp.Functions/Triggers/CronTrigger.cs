using System;
using System.Threading.Tasks;
using LineTen.Core;
using MyFunctionApp.Functions.Models;
using MyFunctionApp.Functions.Orchestrations;
using MyFunctionApp.Services.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace MyFunctionApp.Functions.Triggers
{
    /// <summary>
    /// Runs on a timer
    /// </summary>
    public class CronTrigger
    {
        private readonly ILoggingService _loggingService;

        public CronTrigger(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [FunctionName(nameof(CronTrigger))]
        public async Task RunAsync([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, [DurableClient] IDurableOrchestrationClient starter,ILogger log)
        {
            // Function input comes from the request content.
            var instanceId = await starter.StartNewAsync(nameof(GreetingsOrchestrator),new GreetingsModel
            {
                Cities = new []{"Tokyo","Seattle","London"}
            });

            _loggingService.Log($"Started orchestration with ID = '{instanceId}'. at {DateTimeProvider.UtcNow}");
        }
    }
}
