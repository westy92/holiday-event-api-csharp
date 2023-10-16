using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Collections.Generic;

namespace HolidayEventApi.Test
{
    [TestClass]
    public class TestGetEvents
    {
        private static readonly string getEventsDefault = File.ReadAllText("Data/getEvents-default.json");
        private static readonly string getEventsParameters = File.ReadAllText("Data/getEvents-parameters.json");

        [TestInitialize()]
        public void BeforeEach()
        {
            MockClient.Handler.ResetExpectations();
            MockClient.Handler.ResetBackendDefinitions();
        }

        [TestMethod]
        public async Task TestGetEventsWithDefaultParameters()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .Respond("application/json", getEventsDefault);
            var result = await client.GetEvents();

            Assert.AreEqual(false, result.Adult);
            Assert.AreEqual("05/05/2025", result.Date);
            Assert.AreEqual("America/Chicago", result.Timezone);
            Assert.AreEqual(2, result.Events.Count);
            Assert.AreEqual(1, result.MultidayStarting.Count);
            Assert.AreEqual(2, result.MultidayOngoing.Count);
            result.Events[0].Should().BeEquivalentTo(new EventSummary {
                Id = "b80630ae75c35f34c0526173dd999cfc",
                Name = "Cinco de Mayo",
                Url = "https://www.checkiday.com/b80630ae75c35f34c0526173dd999cfc/cinco-de-mayo",
            });
        }

        [TestMethod]
        public async Task TestGetEventsWithSetParameters()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/events")
                .WithExactQueryString(new Dictionary<string,string> {
                    { "adult", "true" },
                    { "date", "7/16/1992" },
                    { "timezone", "America/New_York" },
                })
                .Respond("application/json", getEventsParameters);
            var result = await client.GetEvents(
                date: "7/16/1992",
                adult: true,
                timezone: "America/New_York"
            );

            Assert.AreEqual(true, result.Adult);
            Assert.AreEqual("America/New_York", result.Timezone);
            Assert.AreEqual(2, result.Events.Count);
            Assert.AreEqual(0, result.MultidayStarting.Count);
            Assert.AreEqual(1, result.MultidayOngoing.Count);
            result.Events[0].Should().BeEquivalentTo(new EventSummary {
                Id = "6ebb6fd5e483de2fde33969a6c398472",
                Name = "Get to Know Your Customers Day",
                Url = "https://www.checkiday.com/6ebb6fd5e483de2fde33969a6c398472/get-to-know-your-customers-day",
            });
        }
    }
}
