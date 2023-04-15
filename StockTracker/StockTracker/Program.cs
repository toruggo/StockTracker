using System.Globalization;
using System.Text.Json;

var sender = EmailSender.fromConfigurationFile("C:\\Users\\AMD\\Desktop\\VHProjects\\StockTracker\\StockTracker\\EmailConfig.json");

sender.sendSellMessage("PETR4", 24.5M);