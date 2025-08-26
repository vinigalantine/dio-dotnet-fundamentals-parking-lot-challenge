using ParkingLot.Common.Interfaces;

namespace ParkingLot.Common;

public class UserInputOutput : IUserInputOutput
{
    public void Clear()
    {
        Console.Clear();
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
