using ParkingLot.Common.Interfaces;
using ParkingLot.Common.Services;

namespace ParkingLot.Common.Models;

public class ParkingManager
{
    private IUserInputOutput userInputOutput;

    private readonly MessageService messageService;
    private decimal initialPrice = 0;
    private decimal pricePerHour = 0;
    private List<string> vehicles = new List<string>();

    public ParkingManager(IUserInputOutput userInputOutput, MessageService messageService, decimal initialPrice, decimal pricePerHour)
    {
        this.userInputOutput = userInputOutput;
        this.messageService = messageService;
        this.initialPrice = initialPrice;
        this.pricePerHour = pricePerHour;
    }

    public void AddVehicle()
    {
        string? licensePlate = null;
        while (string.IsNullOrEmpty(licensePlate))
        {
            Console.Clear();
            userInputOutput.WriteLine(messageService.GetMessage("AskForVehicleLicensePlateToPark"));
            userInputOutput.WriteLine(messageService.GetMessage("LeaveOption", "-"));
            licensePlate = userInputOutput.ReadLine().ToUpper().Trim();

            if (string.IsNullOrEmpty(licensePlate) || licensePlate == "")
            {
                userInputOutput.WriteLine(messageService.GetMessage("PleaseInformTheLicensePlateCorrectly"));
                Thread.Sleep(1500);
            }
            else if (licensePlate == "-")
            {
                userInputOutput.WriteLine(messageService.GetMessage("Returning"));
                return;
            }
            else if (vehicles.Any(vehicles => vehicles == licensePlate))
            {
                userInputOutput.WriteLine(messageService.GetMessage("LicensePlateAlreadyParked"));
                Thread.Sleep(1500);
                licensePlate = null;
            }
        }
        vehicles.Add(licensePlate);
        userInputOutput.WriteLine(messageService.GetMessage("VehicleParkedSuccessfully"));
    }

    public void RemoveVehicle()
    {
        string? licensePlate = null;
        while (string.IsNullOrEmpty(licensePlate))
        {
            userInputOutput.WriteLine(messageService.GetMessage("AskForVehicleLicensePlateToRemove"));
            licensePlate = userInputOutput.ReadLine().ToUpper().Trim();

            if (string.IsNullOrEmpty(licensePlate) || licensePlate == "")
            {
                userInputOutput.WriteLine(messageService.GetMessage("PleaseInformTheLicensePlateCorrectly"));
                Thread.Sleep(1500);
            }
            else if (licensePlate == "-")
            {
                userInputOutput.WriteLine(messageService.GetMessage("Returning"));
                return;
            }
            else if (!vehicles.Any(x => x == licensePlate.ToUpper()))
            {
                userInputOutput.WriteLine(messageService.GetMessage("VehicleNotFound"));
                Thread.Sleep(1500);
                licensePlate = null;
            }
        }

        int? hours = null;
        while (hours == null)
        {
            userInputOutput.WriteLine(messageService.GetMessage("AskHowManyHoursVehicleIsParked"));
            string userInput = userInputOutput.ReadLine();
            try
            {
                hours = int.Parse(userInput);
            }
            catch (FormatException e)
            {
                userInputOutput.WriteLine(messageService.GetMessage("PleaseProvideTheHoursCorrectly"));
                hours = null;
                continue;
            }
        }
        vehicles.Remove(licensePlate);
        decimal total = this.initialPrice + (decimal)hours * this.pricePerHour;
        userInputOutput.WriteLine(messageService.GetMessage("VehicleRemovedAndTotalatoPay", new object[] { licensePlate, total.ToString("F2") }));
    }

    public void ListVehicles()
    {
        if (vehicles.Any())
        {
            Console.WriteLine("Os veículos estacionados são:");
            // TODO: [EN] Loop through and display the parked vehicles
            // *IMPLEMENT HERE*
            // TODO: [PT-BR] Realizar um laço de repetição, exibindo os veículos estacionados
            // *IMPLEMENTE AQUI*
        }
        else
        {
            Console.WriteLine("Não há veículos estacionados.");
        }
    }
}

