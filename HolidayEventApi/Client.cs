using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

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

        public async Task<GetEventsResponse> GetEvents(string? date = null, bool adult = false, string? timezone = null) {
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
            HttpResponseMessage? response = null;
            Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
            try {
                var url = endpoint + '?' + parameters;
                response = await client.GetAsync(url);
                map = await response.Content.ReadAsAsync<Dictionary<string, dynamic>>();
                // TODO add rate-limit
                // TODO convert map to T instead of re-serializing
                var newJson = JsonConvert.SerializeObject(map);
                var result = JsonConvert.DeserializeObject<T>(newJson);
                return result;
            } catch {
                if (response?.IsSuccessStatusCode == true) {
                    throw new SystemException("Unable to parse response.");
                } else {
                    var error = map.TryGetValue("error", out var value) ? value : response?.ReasonPhrase ?? "Unknown error";
                    throw new SystemException(error);
                }
            }
        }
    }
}
