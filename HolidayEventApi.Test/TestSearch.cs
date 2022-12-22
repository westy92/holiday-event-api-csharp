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
    public class TestSearch
    {
        private static string searchDefault = File.ReadAllText("Data/search-default.json");
        private static string searchParameters = File.ReadAllText("Data/search-parameters.json");

        [TestInitialize()]
        public void BeforeEach()
        {
            MockClient.Handler.ResetExpectations();
            MockClient.Handler.ResetBackendDefinitions();
        }

        [TestMethod]
        public async Task TestSearchWithDefaultParameters()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/search")
                .WithExactQueryString(new Dictionary<string,string> {
                    { "adult", "false" },
                    { "query", "zucchini" },
                })
                .Respond("application/json", searchDefault);
            var result = await client.Search("zucchini");

            Assert.AreEqual(false, result.Adult);
            Assert.AreEqual(3, result.Events.Count);
            result.Events[0].Should().BeEquivalentTo(new EventSummary {
                Id = "cc81cbd8730098456f85f69798cbc867",
                Name = "National Zucchini Bread Day",
                Url = "https://www.checkiday.com/cc81cbd8730098456f85f69798cbc867/national-zucchini-bread-day",
            });
        }

        [TestMethod]
        public async Task TestSearchWithSetParameters()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/search")
                .WithExactQueryString(new Dictionary<string,string> {
                    { "adult", "true" },
                    { "query", "porch day" },
                })
                .Respond("application/json", searchParameters);
            var result = await client.Search(
                query: "porch day",
                adult: true
            );

            Assert.AreEqual(true, result.Adult);
            Assert.AreEqual(1, result.Events.Count);
            result.Events[0].Should().BeEquivalentTo(new EventSummary {
                Id = "61363236f06e4eb8e4e14e5925c2503d",
                Name = "Sneak Some Zucchini Onto Your Neighbor's Porch Day",
                Url = "https://www.checkiday.com/61363236f06e4eb8e4e14e5925c2503d/sneak-some-zucchini-onto-your-neighbors-porch-day",
            });
        }

        [TestMethod]
        public async Task TestSearchQueryTooShort()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/search")
                .WithExactQueryString(new Dictionary<string,string> {
                    { "query", "a" },
                    { "adult", "false" },
                })
                .Respond(HttpStatusCode.BadRequest, "application/json", "{'error':'Please enter a longer search term.'}");
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.Search("a"));
            Assert.AreEqual("Please enter a longer search term.", ex.Message);
        }

        [TestMethod]
        public async Task TestSearchTooManyResults()
        {
            var client = new MockClient("abc123");
            MockClient.Handler
                .When("https://api.apilayer.com/checkiday/search")
                .WithExactQueryString(new Dictionary<string,string> {
                    { "query", "day" },
                    { "adult", "false" },
                })
                .Respond(HttpStatusCode.BadRequest, "application/json", "{'error':'Too many results returned. Please refine your query.'}");
            var ex = await Assert.ThrowsExceptionAsync<SystemException>(() => client.Search("day"));
            Assert.AreEqual("Too many results returned. Please refine your query.", ex.Message);
        }

        [TestMethod]
        public async Task TestSearchMissingParameters()
        {
            var client = new MockClient("abc123");
            var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.Search(null));
            Assert.AreEqual("Search query is required.", ex.Message);
            ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => client.Search(""));
            Assert.AreEqual("Search query is required.", ex.Message);
        }
    }
}
