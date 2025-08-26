using System;

namespace ParkingLot.Common.Interfaces;

public interface IParkingManager
{
    public void AddVehicle();
    public void RemoveVehicle();
    public void ListVehicles();
    public void ShowPrices();
}
