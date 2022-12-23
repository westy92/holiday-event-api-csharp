using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Net;
using System;
using System.Collections.Generic;

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
        public async Task TestSendsPlatformVersion()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .Expect("https://api.apilayer.com/checkiday/events")
                .WithHeaders("X-Platform-Version", System.Environment.Version.ToString())
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

        [TestMethod]
        public async Task TestServerErrorUnknown()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Respond((HttpStatusCode)599, "application/json", "{}");
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.GetEvents());
            Assert.AreEqual("599", ex.Message);
        }

        [TestMethod]
        public async Task TestServerError()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Throw(new TaskCanceledException());
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.GetEvents());
            Assert.AreEqual("A task was canceled.", ex.Message);
        }

        [TestMethod]
        public async Task TestServerErrorMalformedResponse()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Respond("application/json", "{");
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.GetEvents());
            Assert.AreEqual("Unable to parse response.", ex.Message);
        }

        /*
        TODO figure out how to test redirects.
        [TestMethod]
        public async Task TestFollowsRedirects()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Respond(HttpStatusCode.Redirect, new List<KeyValuePair<string, string>>{ new KeyValuePair<string, string>("Location", "https://api.apilayer.com/checkiday/redirect")}, "application/json", "");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/redirect")
                .Respond("application/json", getEventsDefault);
            var result = await client.GetEvents();
            Assert.AreEqual(false, result.Adult);
        }*/

        [TestMethod]
        public async Task TestReportsRateLimits()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Respond(new List<KeyValuePair<string, string>>{ 
                    new KeyValuePair<string, string>("X-RateLimit-Remaining-Month", "123"),
                    new KeyValuePair<string, string>("X-RateLimit-Limit-Month", "456"),
                }, "application/json", getEventsDefault);
            var result = await client.GetEvents();
            Assert.AreEqual(123, result.RateLimit.RemainingMonth);
            Assert.AreEqual(456, result.RateLimit.LimitMonth);
        }
    }
}
