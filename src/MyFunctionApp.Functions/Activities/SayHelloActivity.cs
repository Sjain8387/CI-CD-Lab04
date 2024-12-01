using System;
using System.Threading.Tasks;
using MyFunctionApp.Services.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace MyFunctionApp.Functions.Activities
{
    /// <summary>
    /// Says hello to a specific City
    /// </summary>
    public class SayHelloActivity
    {
        private readonly ILoggingService _loggingService;
        private static readonly Random Random = new Random();
        public SayHelloActivity(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }
        
        /// <summary>
        /// Runs the activity function
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName(nameof(SayHelloActivity))]
        public async Task<string> RunAsync([ActivityTrigger] string name, ILogger log)
        {
            var millisecondsDelay = Random.Next(0, 2000);
            _loggingService.Log($"Saying hello to {name} in {millisecondsDelay}.");
            await Task.Delay(millisecondsDelay);
            return $"Hello {name}!";
        }
    }
}
