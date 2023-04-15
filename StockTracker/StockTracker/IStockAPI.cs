using System;

public interface IStockAPI
{
    public decimal GetStockPrice(string stockSymbol);
}
