using System;

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

    public int verifyPriceBounds(decimal price)
    {
        if(price >= UpperBound)
        {
            return 1;
        }
        else if(price <= LowerBound)
        {
            return 2;
        }
        return -1;
    }

    // faz chamadas intervaladas e notifica usuario
    public async Task TrackStockAsync()
    {
        bool outOfBounds = false;
        while (true)
        {
            Timer timer = new((state) =>
            {
                decimal currentPrice = _stockApi.GetStockPrice(StockSymbol);

                if (currentPrice >= UpperBound)
                {
                    // so manda mensagem na primeira vez que sai do intervalo
                    if (!outOfBounds)
                        _messageSender.SendSellMessage(StockSymbol, currentPrice);
                    outOfBounds = true;

                    Console.WriteLine("Disparei email de venda!");
                }
                else if (currentPrice <= LowerBound)
                {
                    if (!outOfBounds)
                        _messageSender.SendBuyMessage(StockSymbol, currentPrice);
                    outOfBounds = true;
                    Console.WriteLine("Disparei email de compra!");

                }
                // reseta quando volta pro intervalo
                else 
                { 
                    Console.WriteLine("Voltou pro intervalo normal.");
                    outOfBounds = false;
                }

                Console.WriteLine("Chamou! O preco agora eh " + currentPrice);
            }, null, 0, 30000);

            await Task.Delay(Timeout.Infinite);
        }
    }
}
