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
            var queryParams = HttpUtility.ParseQueryString(String.Empty);
            queryParams.Add("adult", adult.ToString().ToLower());
            if (date != null) queryParams.Add("date", date);
            if (timezone != null) queryParams.Add("timezone", timezone);

            return await request<GetEventsResponse>("events", queryParams);
        }

        public async Task<SearchResponse> Search(string query, bool adult = false) {
            var queryParams = HttpUtility.ParseQueryString(String.Empty);
            queryParams.Add("query", query);
            queryParams.Add("adult", adult.ToString().ToLower());

            return await request<SearchResponse>("search", queryParams);
        }

        private async Task<T> request<T>(string endpoint, NameValueCollection parameters) {
            var url = endpoint + '?' + parameters;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsAsync<T>();
            return result;
        }
    }
}
