using FluentAssertions;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

class MockHolidayEventApi : HolidayEventApi.Client {
    public static MockHttpMessageHandler Handler = new MockHttpMessageHandler();
    protected override HttpClient ClientFactory() => new HttpClient(Handler);

    public MockHolidayEventApi(string apikey) : base(apikey) {
        
    }
}

namespace HolidayEventApi.Test {
    [TestClass]
    public class TestGetEvents
    {
        private static string getEventsDefault = File.ReadAllText("Data/getEvents-default.json"); 

        [TestMethod]
        public async Task TestTODOName()
        {
            var client = new MockHolidayEventApi("apikey123");
            MockHolidayEventApi.Handler
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
    }
}
