using System.Globalization;
using System.Resources;
using ParkingLot.Common.Interfaces;

namespace ParkingLot.Common.Services;

public class MessageService : IMessageService
{
    private readonly ResourceManager resourceManager;
    private readonly CultureInfo cultureInfo;
    public CultureInfo CultureInfo => this.cultureInfo;

    public MessageService(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        this.resourceManager = resourceManager;
        this.cultureInfo = cultureInfo;
    }

    public string? GetMessage(string messageId)
    {
        return resourceManager.GetString(messageId, cultureInfo);
    }
    public string? GetMessage(string messageId, string? arg)
    {
        string message = resourceManager.GetString(messageId, cultureInfo);

        if (string.IsNullOrEmpty(arg))
            return message;

        return string.Format(message, arg);    
    }
    public string? GetMessage(string messageId, object[] args)
    {
        string message = resourceManager.GetString(messageId, cultureInfo);

        if (args.Length == 0)
            return message;

        return string.Format(message, args);    
    }
}
