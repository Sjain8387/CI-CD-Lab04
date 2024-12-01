using System.Net.Http;
using MyFunctionApp.Functions.Models;
using MyFunctionApp.Functions.Orchestrations;
using MyFunctionApp.Functions.Triggers;
using LineTen.Unit.Tests.Framework;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MyFunctionApp.Functions.Tests.Unit.Triggers.HttpTriggerTests
{
    public class when_invoking_http_trigger_function :WithSubject<HttpTrigger>
    {
        private HttpResponseMessage _result;
        private HttpRequestMessage _httpRequestMessage;

        public when_invoking_http_trigger_function()
        {
            var starter = The<IDurableOrchestrationClient>();
            var logger = The<ILogger>();
            starter.Setup(x => x.StartNewAsync(It.IsAny<string>(), It.IsAny<GreetingsModel>())).ReturnsAsync("newinstance");
            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            _result = Subject.RunAsync(_httpRequestMessage, starter.Object, logger.Object).Result;
        }
        [Fact]
        public void it_should_trigger_durable_function_starter()
        {
            The<IDurableOrchestrationClient>().Verify(x=> x.StartNewAsync(nameof(GreetingsOrchestrator), It.IsAny<GreetingsModel>()), Times.Exactly(1));
        }
        [Fact]
        public void it_should_return_a_202_accepted()
        {
            The<IDurableOrchestrationClient>().Verify(x => x.CreateCheckStatusResponse(_httpRequestMessage, "newinstance", false), Times.Exactly(1));
        }
    }
}
