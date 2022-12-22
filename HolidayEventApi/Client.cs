using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HolidayEventApi
{
    public class Client
    {
        private HttpClient client = new HttpClient() {
            BaseAddress = new Uri("https://api.apilayer.com/checkiday/"),
        };


        public Client(string apiKey) {
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Please provide a valid API key. Get one at https://apilayer.com/marketplace/checkiday-api#pricing.");
            client.DefaultRequestHeaders.Add("apikey", apiKey);
            client.DefaultRequestHeaders.Add("User-Agent", "HolidayApiDotNet/1.0.0"); // TODO auto version?
        }

        public async Task<GetEventsResponse> GetEvents() {
            var response = await client.GetAsync("events");
            var result = await response.Content.ReadAsAsync<GetEventsResponse>();
            return result;
        }
    }
}
