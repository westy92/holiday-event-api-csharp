using System.Net.Http;
using RichardSzalay.MockHttp;

class MockClient : HolidayEventApi.Client {
    public static MockHttpMessageHandler Handler = new MockHttpMessageHandler();
    protected override HttpClient ClientFactory() => new HttpClient(Handler);

    public MockClient(string apikey) : base(apikey) {}
}
