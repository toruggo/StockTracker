using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

public class EmailConfig
{
    public string RecipientName { get; set; }
    public string SenderEmailAddress { get; set; }
    public string SenderEmailPassword { get; set; }
    public string RecipientEmailAdress { get; set; }
    public string BuyMessageTemplate { get; set; }
    public string SellMessageTemplate { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
}

public class EmailSender : IMessageSender
{
    private readonly EmailConfig _emailConfig;
    private readonly SmtpClient _smtpClient;



    public EmailSender(EmailConfig emailConfig)
    {
        _emailConfig = emailConfig;

        _smtpClient = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.SmtpPort);
        _smtpClient.UseDefaultCredentials = false;
        _smtpClient.Credentials = new NetworkCredential(_emailConfig.SenderEmailAddress, _emailConfig.SenderEmailPassword);
        _smtpClient.EnableSsl = true;
    }

    public static EmailSender fromConfigurationFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        //JsonSerializerSettings settings = new JsonSerializerSettings
        //{
        //    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor, // Permite o uso de construtores não públicos
        //};

        string jsonText = File.ReadAllText(filePath);
        EmailConfig? emailConfig = JsonSerializer.Deserialize<EmailConfig>(jsonText);

        if (emailConfig == null)
        {
            throw new Exception("JSON File is not valid.");
        }
        return new EmailSender((EmailConfig)emailConfig);
    }

    private void SendEmail(string subject, string body)
    {
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(_emailConfig.SenderEmailAddress)
        };
        mailMessage.To.Add(_emailConfig.RecipientEmailAdress);

        byte[] bytes = Encoding.Default.GetBytes(body);
        body = Encoding.UTF8.GetString(bytes);

        mailMessage.Subject = subject;
        mailMessage.Body = body;

        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        _smtpClient.Send(mailMessage);
    }

    public string RenderStringTemplate(string template, string stockSymbol, decimal stockPrice)
    {
        template = template.Replace("{RecipientName}", _emailConfig.RecipientName)
                           .Replace("{StockSymbol}", stockSymbol)
                           .Replace("{StockPrice}", stockPrice.ToString());
        return template;
    }

    public void SendBuyMessage(string stockSymbol, decimal stockPrice)
    {
        string messageBody = RenderStringTemplate(_emailConfig.BuyMessageTemplate, stockSymbol, stockPrice);
        SendEmail("Compre agora!", messageBody);
    }

    public void SendSellMessage(string stockSymbol, decimal stockPrice)
    {
        string messageBody = RenderStringTemplate(_emailConfig.SellMessageTemplate, stockSymbol, stockPrice);
        SendEmail("Venda agora!", messageBody);
    }
}
