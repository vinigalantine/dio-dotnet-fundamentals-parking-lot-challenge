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
}
