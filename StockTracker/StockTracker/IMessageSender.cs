using System;

public interface IMessageSender
{
    public void SendBuyMessage(string stockSymbol, decimal stockPrice);
    public void SendSellMessage(string stockSymbol, decimal stockPrice);
}