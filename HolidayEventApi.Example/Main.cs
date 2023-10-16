using System;
using System.Threading.Tasks;

namespace HolidayEventApi.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try {
                // Get a FREE API key from https://apilayer.com/marketplace/checkiday-api#pricing
                var client = new HolidayEventApi.Client("<Your API Key Here>");

                // Get Events for a given Date
                var events = await client.GetEvents(
                    // These parameters are the defaults but can be specified:
                    // date: "today",
                    // adult: false,
                    // timezone: "America/Chicago"
                );

                var e = events.Events[0];
                Console.WriteLine("Today is {0}! Find more information at: {1}", e.Name, e.Url);
                Console.WriteLine("Rate limit remaining: {0}/{1} (month).", events.RateLimit.RemainingMonth, events.RateLimit.LimitMonth);

                // Get Event Information
                var eventInfo = await client.GetEventInfo(
                    id: e.Id
                    // These parameters can be specified to calculate the range of eventInfo.Event.Occurrences
                    // start: 2020,
                    // end: 2030
                );

                Console.WriteLine("The Event's hashtags are {0}.", string.Join(", ", eventInfo.Event.Hashtags));

                // Search for Events
                var query = "pizza day";
                var search = await client.Search(
                    query: query
                    // These parameters are the defaults but can be specified:
                    // adult: false
                );

                Console.WriteLine("Found {0} events, including '{1}', that match the query '{2}'.", search.Events.Count, search.Events[0].Name, query);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
