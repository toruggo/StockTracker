using System;

public class StockTracker
{
    public string StockSymbol { get; }
    public decimal LowerBound { get; }
    public decimal UpperBound { get; }

    private IStockAPI _stockApi;

    public StockTracker(string stockSymbol, decimal lowerBound, decimal upperBound, IStockAPI stockApi)
    {
        StockSymbol = stockSymbol;
        LowerBound = lowerBound;
        UpperBound = upperBound;
        _stockApi = stockApi;


        // stockPrice = getStockPrice();
        // request price with API to respective label

        // check if price

        // email sender class object .sendEmail(emailCode buy/sell);  ou sendBuyEmail, sendSellEmail
        // manter emails em arquivo separado para enviar emails genericamente
    }

    public void verifyPriceBounds ()
    {
        decimal price = _stockApi.GetStockPrice(StockSymbol);

        if(price >= UpperBound)
        {

        }
        else if(price <= LowerBound)
        {

        }
    }
}
