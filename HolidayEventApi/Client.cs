using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace HolidayEventApi
{
    public class Client
    {
        private HttpClient client;
        protected virtual HttpClient ClientFactory() => new HttpClient();

        public Client(string apiKey) {
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Please provide a valid API key. Get one at https://apilayer.com/marketplace/checkiday-api#pricing.");
            client = ClientFactory();
            client.BaseAddress = new Uri("https://api.apilayer.com/checkiday/");
            client.DefaultRequestHeaders.Add("apikey", apiKey);
            client.DefaultRequestHeaders.Add("User-Agent", "HolidayApiDotNet/1.0.0"); // TODO auto version?
        }

        public async Task<GetEventsResponse> GetEvents(string date = null, bool adult = false, string timezone = null) {
            var query = HttpUtility.ParseQueryString(String.Empty);
            query.Add("adult", adult.ToString().ToLower());
            if (date != null) query.Add("date", date);
            if (timezone != null) query.Add("timezone", timezone);

            return await request<GetEventsResponse>("events", query);
        }

        private async Task<T> request<T>(string endpoint, NameValueCollection parameters) {
            var url = endpoint + '?' + parameters;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsAsync<T>();
            return result;
        }
    }
}
