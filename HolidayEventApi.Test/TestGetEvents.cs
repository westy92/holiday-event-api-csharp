using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayEventApi.Test {
    [TestClass]
    public class TestGetEvents
    {
        [TestMethod]
        public async Task TestTODOName()
        {
            var client = new Client("apikey123");
            var result = await client.GetEvents();
            Assert.AreEqual(false, result.Adult);
            Assert.AreEqual("12/21/2022", result.Date);
            Assert.AreEqual("America/Chicago", result.Timezone);
        }
    }
}
