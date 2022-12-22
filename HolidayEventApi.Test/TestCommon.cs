using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Net;
using System;

namespace HolidayEventApi.Test
{
    [TestClass]
    public class TestCommon
    {
        private static string getEventsDefault = File.ReadAllText("Data/getEvents-default.json");

        [TestInitialize()]
        public void BeforeEach()
        {
            MockClient.Handler.ResetExpectations();
            MockClient.Handler.ResetBackendDefinitions();
        }

        [TestMethod]
        public async Task TestSendsApiKey()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .Expect("https://api.apilayer.com/checkiday/events")
                .WithHeaders("apikey", "abc123")
                .Respond("application/json", getEventsDefault);
            var result = await client.GetEvents();
            MockClient.Handler.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task TestSendsUserAgent()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .Expect("https://api.apilayer.com/checkiday/events")
                .WithHeaders("User-Agent", "HolidayApiDotNet/1.0.0")
                .Respond("application/json", getEventsDefault);
            var result = await client.GetEvents();
            MockClient.Handler.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task TestPassesAlongError()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Respond(HttpStatusCode.Unauthorized, "application/json", "{'error':'MyError!'}");
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.GetEvents());
            Assert.AreEqual("MyError!", ex.Message);
        }

        [TestMethod]
        public async Task TestServerError500()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Respond(HttpStatusCode.InternalServerError, "application/json", "{}");
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.GetEvents());
            Assert.AreEqual("Internal Server Error", ex.Message);
        }
    }
}
