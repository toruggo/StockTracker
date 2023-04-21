using System.Globalization;
using System.Text.Json;

// inicializa servicos de envio de email e conecta com API de cotacao
string currentDirectory = Directory.GetCurrentDirectory();
string configDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));

var sender = EmailSender.fromConfigurationFile(configDirectory + "\\EmailConfig.json");
TwelveDataAPI twelveDataAPI = new TwelveDataAPI("6b2237a02d264d7abde364c863df906d");

string[] inputTokens = Console.ReadLine().Split();

if(inputTokens.Length < 3)
{
    Console.WriteLine("Invalid input. Please restart and try again.");
    return;
}

// para ler decimais separados por ponto e nao virgula
Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

string stockSymbol = inputTokens[0];
decimal lowerBound = Convert.ToDecimal(inputTokens[1]);
decimal upperBound = Convert.ToDecimal(inputTokens[2]);

StockTracker stockTracker = new StockTracker(stockSymbol, lowerBound, upperBound, twelveDataAPI, sender);

await stockTracker.TrackStockAsync();