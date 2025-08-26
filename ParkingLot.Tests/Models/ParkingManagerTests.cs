using System.Globalization;
using System.Resources;
using ParkingLot.Common.Models;
using ParkingLot.Common.Services;
using ParkingLot.Tests.TestHelpers;

namespace ParkingLot.Tests.Models;

public class ParkingManagerTests
{
    private readonly ResourceManager resourceManager = new ResourceManager("ParkingLot.Common.Resources.Messages", typeof(ParkingManager).Assembly);
    private readonly string consoleCleared = "Console Cleared";

    [Fact]
    public void AddVehicle_WhenNewVehicleProvided_AddsVehicleSuccessfully()
    {
        string[] inputs = { "ABC" };
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput(inputs);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleParkedSuccessfully")
        ];

        parkingManager.AddVehicle();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void AddVehicle_WhenProdivedWithReturnOption_ReturnsToMenu()
    {
        string[] inputs = { "-" };
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput(inputs);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("Returning")
        ];

        parkingManager.AddVehicle();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void AddVehicle_WhenVehicleAlreadyExists_ReturnsDuplicateVehicleMessage()
    {
        string[] inputs = { "ABC", "ABC", "-" };
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput(inputs);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleParkedSuccessfully"),
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("LicensePlateAlreadyParked"),
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("Returning")
        ];

        parkingManager.AddVehicle();
        parkingManager.AddVehicle();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void RemoveVehicle_WhenParkedVehicleRemoved_RemovesVehicleSuccessfully()
    {
        string licensePlate = "ABC";
        int hours = 2;
        decimal initialPrice = 5, pricePerHour = 2.50M;
        string[] inputs = { licensePlate, licensePlate, hours.ToString() };

        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput(inputs);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, initialPrice, pricePerHour);

        decimal expectedTotal = initialPrice + hours * pricePerHour;
        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleParkedSuccessfully"),
            messageService.GetMessage("AskForVehicleLicensePlateToRemove"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("AskHowManyHoursVehicleIsParked"),
            messageService.GetMessage("VehicleRemovedAndTotalatoPay", new object[] { licensePlate, expectedTotal.ToString("F2") })
        ];

        parkingManager.AddVehicle();
        parkingManager.RemoveVehicle();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void RemoveVehicle_WhenProdivedWithReturnOption_ReturnsToMenu()
    {
        string[] inputs = { "ABC", "-" };
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput(inputs);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        
        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleParkedSuccessfully"),
            messageService.GetMessage("AskForVehicleLicensePlateToRemove"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("Returning")
        ];

        parkingManager.AddVehicle();
        parkingManager.RemoveVehicle();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void RemoveVehicle_WhenVehicleNotFound_ReturnsNotFoundMessage()
    {
        string[] inputs = { "ABC", "DEF", "-" };
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput(inputs);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        
        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleParkedSuccessfully"),
            messageService.GetMessage("AskForVehicleLicensePlateToRemove"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleNotFound"),
            messageService.GetMessage("AskForVehicleLicensePlateToRemove"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("Returning"),
        ];

        parkingManager.AddVehicle();
        parkingManager.RemoveVehicle();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void RemoveVehicle_WhenNoVehicleParked_ReturnsNoVehiclesMessage()
    {
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput();
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        List<string> expectedOutput = [
            messageService.GetMessage("NoVehiclesParkedHere"),
        ];

        parkingManager.RemoveVehicle();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void ListVehicles_WhenVehiclesExist_ListsAllPlates()
    {
        string[] inputs = { "ABC", "DEF" };
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput(inputs);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleParkedSuccessfully"),
            consoleCleared,
            messageService.GetMessage("AskForVehicleLicensePlateToPark"),
            messageService.GetMessage("LeaveOption", "-"),
            messageService.GetMessage("VehicleParkedSuccessfully"),
            messageService.GetMessage("TheseAreTheVehiclesParkedHere"),
            inputs[0],
            inputs[1],
        ];

        parkingManager.AddVehicle();
        parkingManager.AddVehicle();
        parkingManager.ListVehicles();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void ListVehicles_WhenNoVehicles_ReturnsNoVehiclesMessage()
    {
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput();
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, 5, 2.50M);
        List<string> expectedOutput = [
            messageService.GetMessage("NoVehiclesParkedHere"),
        ];

        parkingManager.ListVehicles();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }

    [Fact]
    public void ShowPrices_WhenCalled_DisplaysFormattedPrices()
    {
        decimal initialPrice = 5, pricePerHour = 2.50M;
        FakeUserInputOutput fakeUserInputOutput = new FakeUserInputOutput();
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        ParkingManager parkingManager = new ParkingManager(fakeUserInputOutput, messageService, initialPrice, pricePerHour);
        List<string> expectedOutput = [
            messageService.GetMessage("InitialPrice", initialPrice.ToString("F2")),
            messageService.GetMessage("PricePerHour", pricePerHour.ToString("F2"))
        ];

        parkingManager.ShowPrices();

        Assert.Equal(expectedOutput, fakeUserInputOutput.Outputs);
    }
}
