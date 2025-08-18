using System.Globalization;
using System.Resources;

namespace ParkingLot.Common.Services;

public class MessageService
{
    private ResourceManager resourceManager;
    private CultureInfo cultureInfo;

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
