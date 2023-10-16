# The Official Holiday and Event API for .NET

[![Nuget](https://img.shields.io/nuget/v/HolidayEventApi)](https://www.nuget.org/packages/HolidayEventApi)
[![Build Status](https://github.com/westy92/holiday-event-api-csharp/actions/workflows/github-actions.yml/badge.svg)](https://github.com/westy92/holiday-event-api-csharp/actions)
[![Code Coverage](https://codecov.io/gh/westy92/holiday-event-api-csharp/branch/main/graph/badge.svg)](https://codecov.io/gh/westy92/holiday-event-api-csharp)
[![Funding Status](https://img.shields.io/github/sponsors/westy92)](https://github.com/sponsors/westy92)

Industry-leading Holiday and Event API for .NET. Over 5,000 holidays and thousands of descriptions. Trusted by the World’s leading companies. Built by developers for developers since 2011.

## Supported .NET Versions

Latest version of the the Holiday and Event API supports all actively-supported [.NET](https://endoflife.date/dotnet) and [.NET Framework](https://endoflife.date/dotnetfx) versions.

## Authentication

Access to the Holiday and Event API requires an API Key. You can get for one for FREE [here](https://apilayer.com/marketplace/checkiday-api#pricing), no credit card required! Note that free plans are limited. To access more data and have more requests, a paid plan is required.

## Installation

```console
dotnet add package HolidayEventApi
```

## Example

```cs
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
```

## Publishing Updates

1. Run `nuget pack`.
1. Upload generated files to <https://www.nuget.org/packages/manage/upload>.
