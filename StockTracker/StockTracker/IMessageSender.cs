using System;

public interface IMessageSender
{
    public void sendBuyMessage(string stockSymbol, decimal stockPrice);
    public void sendSellMessage(string stockSymbol, decimal stockPrice);
}