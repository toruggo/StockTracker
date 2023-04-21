using System;
using System.Data;

public class StockTracker
{
    public string StockSymbol { get; }
    public decimal LowerBound { get; }
    public decimal UpperBound { get; }

    private readonly IStockAPI _stockApi;

    private readonly IMessageSender _messageSender;

    public StockTracker(string stockSymbol, decimal lowerBound, decimal upperBound, IStockAPI stockApi, IMessageSender messageSender)
    {
        StockSymbol = stockSymbol;
        LowerBound = lowerBound;
        UpperBound = upperBound;
        _stockApi = stockApi;
        _messageSender = messageSender;
    }

    public async Task TrackStockAsync()
    {
        bool outOfBounds = false;
        while (true)
        {
            Timer timer = new((state) =>
            {
                decimal currentPrice = _stockApi.GetStockPrice(StockSymbol);
                Console.WriteLine("[" + DateTime.Now + "] - " + StockSymbol + ": " + currentPrice);

                if (currentPrice >= UpperBound)
                {
                    // so manda mensagem na primeira vez que sai do intervalo
                    if (!outOfBounds)
                    { 
                        _messageSender.SendSellMessage(StockSymbol, currentPrice);
                        Console.WriteLine("Crossed UpperBound. Sell email sent.");
                    }
                    outOfBounds = true;
                }
                else if (currentPrice <= LowerBound)
                {
                    if (!outOfBounds) 
                    { 
                        _messageSender.SendBuyMessage(StockSymbol, currentPrice);
                        Console.WriteLine("Crossed LowerBound. Buy email sent!");
                    }
                    outOfBounds = true;
                }
                // reseta quando volta pro intervalo
                else if(outOfBounds)
                { 
                    Console.WriteLine("Back to monitoring interval.");
                    outOfBounds = false;
                }
            }, null, 0, 30000);

            await Task.Delay(Timeout.Infinite);
        }
    }
}
