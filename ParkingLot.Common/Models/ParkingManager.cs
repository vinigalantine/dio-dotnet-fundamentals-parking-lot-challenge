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
            licensePlate = userInputOutput.ReadLine().Trim();

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
        Console.WriteLine("Digite a placa do veículo para remover:");

        // TODO: [EN] Ask the user to enter the license plate and store it in the variable 'placa'
        // *IMPLEMENT HERE*
        // TODO: [PT-BR] Pedir para o usuário digitar a placa e armazenar na variável placa
        // *IMPLEMENTE AQUI*
        string licensePlate = "";

        if (vehicles.Any(x => x.ToUpper() == licensePlate.ToUpper()))
        {
            Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

            // TODO: [EN] Ask the user to enter the number of hours the vehicle was parked,
            // TODO: [EN] Perform the following calculation: "initialPrice + pricePerHour * hours" for the variable totalValue
            // *IMPLEMENT HERE*
            // TODO: [PT-BR] Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
            // TODO: [PT-BR] Realizar o seguinte cálculo: "initialPrice + pricePerHour * hours" para a variável valorTotal                
            // *IMPLEMENTE AQUI*
            int hours = 0;
            decimal valorTotal = 0;

            // TODO: [EN] Remove the entered license plate from the list of vehicles
            // *IMPLEMENT HERE*
            // TODO: [PT-BR] Remover a placa digitada da lista de veículos
            // *IMPLEMENTE AQUI*

            Console.WriteLine($"O veículo {licensePlate} foi removido e o preço total foi de: R$ {valorTotal}");
        }
        else
        {
            Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
        }
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

