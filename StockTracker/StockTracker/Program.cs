using System.Globalization;
using System.Text.Json;

var sender = EmailSender.fromConfigurationFile("C:\\Users\\PC\\Desktop\\StockTracker-main\\StockTracker\\StockTracker\\EmailConfig.json");

string[] inputTokens = Console.ReadLine().Split();

string stockSymbol = inputTokens[0];
decimal lowerBound = Convert.ToDecimal(inputTokens[1]);
decimal upperBound = Convert.ToDecimal(inputTokens[2]);

while (true)
{
    Timer timer = new((object state) =>
    {
        var APIHelper = new TwelveDataAPI("6b2237a02d264d7abde364c863df906d");

        decimal currentPrice = APIHelper.GetStockPrice("PETR4");

        Console.WriteLine("Chamou! O preco agora eh " + currentPrice);
    }, null, 0, 60000);

    await Task.Delay(Timeout.Infinite);
}

//sender.SendSellMessage("PETR4", 24.5M);