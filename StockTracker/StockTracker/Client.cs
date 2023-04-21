using System.Globalization;
using System.Reflection;
using System.Text.Json;

class Client
{
    static async Task Main(string[] args)
    {
        // inicializa servicos de envio de email e conecta com API de cotacao
        string currentDirectory = Directory.GetCurrentDirectory();
        string configDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));

        var sender = EmailSender.fromConfigurationFile(configDirectory + "\\EmailConfig.json");
        TwelveDataAPI twelveDataAPI = new TwelveDataAPI("6b2237a02d264d7abde364c863df906d");

        if (args.Length < 3)
        {
            Console.WriteLine("Invalid input. Please restart and try again.");
            return;
        }

        // para ler decimais separados por ponto e nao virgula
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        string stockSymbol = args[0];
        decimal upperBound = Convert.ToDecimal(args[1]);
        decimal lowerBound = Convert.ToDecimal(args[2]);

        StockTracker stockTracker = new StockTracker(stockSymbol, lowerBound, upperBound, twelveDataAPI, sender);

        await stockTracker.TrackStockAsync();
    }
}