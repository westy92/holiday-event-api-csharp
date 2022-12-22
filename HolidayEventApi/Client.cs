﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<GetEventsResponse> GetEvents() {
            return await request<GetEventsResponse>("events");
        }

        private async Task<T> request<T>(string endpoint) {
            var response = await client.GetAsync(endpoint);
            var result = await response.Content.ReadAsAsync<T>();
            return result;
        }
    }
}
