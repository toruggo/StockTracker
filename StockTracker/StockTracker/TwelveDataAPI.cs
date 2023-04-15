using System;
using System.Globalization;
using System.Text.Json;

public class TwelveDataAPI : IStockAPI
{

    public const string BaseUrl = "https://api.twelvedata.com/";

    private readonly string _apiKey;
    private readonly HttpClientHandler _httpHandler;
    private readonly HttpClient _httpClient;


    public TwelveDataAPI(string apiKey)
	{
        _apiKey=apiKey;

        _httpHandler = new HttpClientHandler();
        _httpClient = new HttpClient(_httpHandler)
        {
            BaseAddress = new Uri(BaseUrl)
        };
    }

    public decimal GetStockPrice(string stockSymbol)
    {
        var response = _httpClient.GetAsync($"price?symbol={stockSymbol}&apikey={_apiKey}").Result;
        response.EnsureSuccessStatusCode();

        string jsonText = response.Content.ReadAsStringAsync().Result;
        var deserializedJson = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);

        decimal price = System.Convert.ToDecimal(deserializedJson?["price"], CultureInfo.InvariantCulture);
        return price;
    }
}
