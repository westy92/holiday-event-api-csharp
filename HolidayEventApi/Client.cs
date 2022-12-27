using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace HolidayEventApi
{
    public class Client
    {
        private HttpClient client;
        protected virtual HttpClient ClientFactory() => new HttpClient(new HttpClientHandler 
        {
            AllowAutoRedirect = true, 
            MaxAutomaticRedirections = 3,
        });

        /// <summary>
        /// Creates a HolidayEventApi Client.
        /// </summary>
        /// <param name="apiKey">Your API Key. Get one at https://apilayer.com/marketplace/checkiday-api#pricing.</param>
        /// <exception cref="ArgumentException">Thrown when the provided API Key is blank or missing.</exception>
        public Client(string apiKey) {
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Please provide a valid API key. Get one at https://apilayer.com/marketplace/checkiday-api#pricing.");
            client = ClientFactory();
            client.BaseAddress = new Uri("https://api.apilayer.com/checkiday/");
            client.DefaultRequestHeaders.Add("apikey", apiKey);
            var version = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;
            client.DefaultRequestHeaders.Add("User-Agent", "HolidayApiDotNet/" + version);
            client.DefaultRequestHeaders.Add("X-Platform-Version", System.Environment.Version.ToString());
        }

        /// <summary>
        /// Gets the Events for the provided Date.
        /// </summary>
        /// <param name="date">Whether or not to include adult events.</param>
        /// <param name="adult">The date to get Events for.</param>
        /// <param name="timezone">The timezone used to determine events on the given date.</param>
        /// <returns>The Events.</returns>
        public async Task<GetEventsResponse> GetEvents(string? date = null, bool adult = false, string? timezone = null) {
            var queryParams = HttpUtility.ParseQueryString(String.Empty);
            queryParams.Add("adult", adult.ToString().ToLower());
            if (date != null) queryParams.Add("date", date);
            if (timezone != null) queryParams.Add("timezone", timezone);

            return await request<GetEventsResponse>("events", queryParams);
        }

        /// <summary>
        /// Get additional information for an Event.
        /// </summary>
        /// <param name="id">The Event id.</param>
        /// <param name="start">The starting year for returned occurrences.</param>
        /// <param name="end">The ending year for returned occurrences.</param>
        /// <returns>The Event information.</returns>
        /// <exception cref="ArgumentException">Thrown when the event id is missing or empty.</exception>
        public async Task<GetEventInfoResponse> GetEventInfo(string id, int? start = null, int? end = null) {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentException("Event id is required.");
            var queryParams = HttpUtility.ParseQueryString(String.Empty);
            queryParams.Add("id", id);
            if (start != null) queryParams.Add("start", start.ToString());
            if (end != null) queryParams.Add("end", end.ToString());

            return await request<GetEventInfoResponse>("event", queryParams);
        }

        /// <summary>
        /// Searches for Events with the given criteria
        /// </summary>
        /// <param name="query">Your search query</param>
        /// <param name="adult">Whether or not adult events should be included.</param>
        /// <returns>The search results.</returns>
        /// <exception cref="ArgumentException">Thrown when the search query is missing or empty.</exception>
        public async Task<SearchResponse> Search(string query, bool adult = false) {
            if (String.IsNullOrEmpty(query))
                throw new ArgumentException("Search query is required.");
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
                map["rateLimit"] = new Dictionary<string, string>{
                    {"limitMonth", response.Headers.TryGetValues("X-RateLimit-Limit-Month", out var limitMonth) ? limitMonth.First() : "0"},
                    {"remainingMonth", response.Headers.TryGetValues("X-RateLimit-Remaining-Month", out var remainingMonth) ? remainingMonth.First() : "0"},
                };
                // TODO convert map to T instead of re-serializing
                var newJson = JsonConvert.SerializeObject(map);
                var result = JsonConvert.DeserializeObject<T>(newJson);
                return result!;
            } catch (Exception e) {
                if (response?.IsSuccessStatusCode == true) {
                    throw new SystemException("Unable to parse response.");
                } else {
                    var error = map.TryGetValue("error", out var value) ? value : 
                        response?.ReasonPhrase ?? response?.StatusCode.ToString() ?? e.Message;
                    throw new SystemException(error);
                }
            }
        }
    }
}
