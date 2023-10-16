using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Net;
using System;

namespace HolidayEventApi.Test
{
    [TestClass]
    public class TestGetEventInfo
    {
        private static readonly string getEventInfoDefault = File.ReadAllText("Data/getEventInfo.json");
        private static readonly string getEventInfoParameters = File.ReadAllText("Data/getEventInfo-parameters.json");

        [TestInitialize()]
        public void BeforeEach()
        {
            MockClient.Handler.ResetExpectations();
            MockClient.Handler.ResetBackendDefinitions();
        }

        [TestMethod]
        public async Task TestGetEventInfoWithDefaultParameters()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/event")
                .WithExactQueryString(new Dictionary<string,string> {
                    {"id", "f90b893ea04939d7456f30c54f68d7b4"},
                })
                .Respond("application/json", getEventInfoDefault);
            var result = await client.GetEventInfo("f90b893ea04939d7456f30c54f68d7b4");
            Assert.AreEqual(2, result.Event.Hashtags.Count);
            result.Event.Tags.Should().BeEquivalentTo(new List<Tag> { new Tag { Name = "Pets & Animals" } });
        }

        [TestMethod]
        public async Task TestGetEventInfoWithSetParameters()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/event")
                .WithExactQueryString(new Dictionary<string,string> {
                    {"id", "f90b893ea04939d7456f30c54f68d7b4"},
                    {"start", "2002"},
                    {"end", "2003"},
                })
                .Respond("application/json", getEventInfoParameters);
            var result = await client.GetEventInfo("f90b893ea04939d7456f30c54f68d7b4", 2002, 2003);
            Assert.AreEqual(2, result.Event.Occurrences.Count);
            result.Event.Occurrences[0].Should().BeEquivalentTo(new Occurrence {
                Date = "08/08/2002",
                Length = 1,
            });
        }

        [TestMethod]
        public async Task TestGetEventInfoInvalidEvent()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/event")
                .WithExactQueryString(new Dictionary<string,string> {
                    { "id", "hi" },
                })
                .Respond(HttpStatusCode.NotFound, "application/json", "{'error':'Event not found.'}");
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.GetEventInfo("hi"));
            Assert.AreEqual("Event not found.", ex.Message);
        }

        [TestMethod]
        public async Task TestGetEventInfoMissingId()
        {
            var client = new MockClient("abc123");
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetEventInfo(null));
            Assert.AreEqual("Event id is required.", ex.Message);
            ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetEventInfo(""));
            Assert.AreEqual("Event id is required.", ex.Message);
        }
    }
}
