using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFunctionApp.Functions.Activities;
using MyFunctionApp.Functions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace MyFunctionApp.Functions.Orchestrations
{
    /// <summary>
    /// Says hello to the world
    /// </summary>
    public class GreetingsOrchestrator
    {
        /// <summary>
        /// Run the orchestrator
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [FunctionName(nameof(GreetingsOrchestrator))]
        public async Task<List<string>> RunAsync([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();
            var greetingsModel = context.GetInput<GreetingsModel>();
            var activities = greetingsModel.Cities.Select(x => context.CallActivityAsync<string>(nameof(SayHelloActivity), x)).ToList();
            await Task.WhenAll(activities);
            foreach (var activity in activities)
            {
                outputs.Add(activity.Result);
            }
            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }
    }
}
