using System.Globalization;
using System.Text.Json;

// inicializa servicos de envio de email e conecta com API de cotacao
var sender = EmailSender.fromConfigurationFile("C:\\Users\\PC\\Desktop\\StockTracker-main\\StockTracker\\StockTracker\\EmailConfig.json");
TwelveDataAPI twelveDataAPI = new TwelveDataAPI("6b2237a02d264d7abde364c863df906d");

// input
string[] inputTokens = Console.ReadLine().Split();

string stockSymbol = inputTokens[0];
decimal lowerBound = Convert.ToDecimal(inputTokens[1]);
decimal upperBound = Convert.ToDecimal(inputTokens[2]);

// chama stock tracker com intervalo
StockTracker stockTracker = new StockTracker(stockSymbol, lowerBound, upperBound, twelveDataAPI, sender);

await stockTracker.TrackStockAsync();