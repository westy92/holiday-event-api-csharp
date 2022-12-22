using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayEventApi.Test {
    [TestClass]
    public class TestConstructor
    {
        [TestMethod]
        public void TestBlankApiKey()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new Client(""));
            Assert.AreEqual("Please provide a valid API key. Get one at https://apilayer.com/marketplace/checkiday-api#pricing.", ex.Message);
        }

        [TestMethod]
        public void TestNullApiKey()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new Client(null));
            Assert.AreEqual("Please provide a valid API key. Get one at https://apilayer.com/marketplace/checkiday-api#pricing.", ex.Message);
        }

        [TestMethod]
        public void TestValidApiKey()
        {
            Assert.IsInstanceOfType<Client>(new Client("abc123"));
        }
    }
}
