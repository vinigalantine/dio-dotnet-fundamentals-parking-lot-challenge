using System.Globalization;
using System.Resources;

namespace ParkingLot.Common.Interfaces;

public interface IMessageService
{
    CultureInfo CultureInfo { get; }
    string? GetMessage(string messageId);
    string? GetMessage(string messageId, string? arg);
    string? GetMessage(string messageId, object[] args);
}
