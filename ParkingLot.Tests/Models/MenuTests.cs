using System.Globalization;
using System.Resources;
using ParkingLot.Common.Models;
using ParkingLot.Common.Services;
using ParkingLot.Tests.TestHelpers;
using Moq;
using ParkingLot.Common.Interfaces;

namespace ParkingLot.Tests.Models;

public class MenuTests
{
    private readonly ResourceManager resourceManager = new ResourceManager("ParkingLot.Common.Resources.Messages", typeof(ParkingManager).Assembly);
    private readonly string consoleCleared = "Console Cleared";

    [Fact]
    public void ShowMenu_ExitImmediately_WritesGoodbye()
    {
        FakeUserInputOutput userInputOutput = new FakeUserInputOutput(["5", ""]);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        Mock<IParkingManager> parkingManager = new Mock<IParkingManager>();

        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("MenuPrompt"),
            messageService.GetMessage("MenuOptionAdd"),
            messageService.GetMessage("MenuOptionRemove"),
            messageService.GetMessage("MenuOptionList"),
            messageService.GetMessage("MenuOptionPrices"),
            messageService.GetMessage("MenuOptionExit"),
            messageService.GetMessage("PressAnyKey"),
            messageService.GetMessage("GoodbyeMessage"),
        ];

        Menu.ShowMenu(userInputOutput, messageService, parkingManager.Object);

        Assert.Equal(expectedOutput, userInputOutput.Outputs);
        
        parkingManager.Verify(m => m.AddVehicle(), Times.Never);
        parkingManager.Verify(m => m.RemoveVehicle(), Times.Never);
        parkingManager.Verify(m => m.ListVehicles(), Times.Never);
        parkingManager.Verify(m => m.ShowPrices(), Times.Never);
    }

	[Fact]
	public void ShowMenu_SelectAdd_CallsAddVehicle()
	{
		FakeUserInputOutput userInputOutput = new FakeUserInputOutput([ "1", "", "5", "" ]);
		MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        Mock<IParkingManager> parkingManager = new Mock<IParkingManager>();

        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("MenuPrompt"),
            messageService.GetMessage("MenuOptionAdd"),
            messageService.GetMessage("MenuOptionRemove"),
            messageService.GetMessage("MenuOptionList"),
            messageService.GetMessage("MenuOptionPrices"),
            messageService.GetMessage("MenuOptionExit"),
            messageService.GetMessage("PressAnyKey"),
            messageService.GetMessage("GoodbyeMessage"),
        ];

        Menu.ShowMenu(userInputOutput, messageService, parkingManager.Object);

		parkingManager.Verify(m => m.AddVehicle(), Times.Exactly(1));
	}

    [Theory]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    public void ShowMenu_SelectOption_CallsExpectedParkingManagerMethod(string option)
    {
        FakeUserInputOutput userInputOutput = new FakeUserInputOutput([option, "", "5", ""]);
        MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        Mock<IParkingManager> parkingManager = new Mock<IParkingManager>();

        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("MenuPrompt"),
            messageService.GetMessage("MenuOptionAdd"),
            messageService.GetMessage("MenuOptionRemove"),
            messageService.GetMessage("MenuOptionList"),
            messageService.GetMessage("MenuOptionPrices"),
            messageService.GetMessage("MenuOptionExit"),
            messageService.GetMessage("PressAnyKey"),
            consoleCleared,
            messageService.GetMessage("MenuPrompt"),
            messageService.GetMessage("MenuOptionAdd"),
            messageService.GetMessage("MenuOptionRemove"),
            messageService.GetMessage("MenuOptionList"),
            messageService.GetMessage("MenuOptionPrices"),
            messageService.GetMessage("MenuOptionExit"),
            messageService.GetMessage("PressAnyKey"),
            messageService.GetMessage("GoodbyeMessage"),
        ];

        Menu.ShowMenu(userInputOutput, messageService, parkingManager.Object);

        Assert.Equal(expectedOutput, userInputOutput.Outputs);

        parkingManager.Verify(m => m.RemoveVehicle(), Times.Exactly(option == "2" ? 1 : 0));
        parkingManager.Verify(m => m.ListVehicles(), Times.Exactly(option == "3" ? 1 : 0));
        parkingManager.Verify(m => m.ShowPrices(), Times.Exactly(option == "4" ? 1 : 0));
	}

	[Fact]
	public void ShowMenu_InvalidOption_WritesInvalidOptionMessage()
	{
		FakeUserInputOutput userInputOutput = new FakeUserInputOutput([ "x", "", "5", "" ]);
		MessageService messageService = new MessageService(this.resourceManager, CultureInfo.InvariantCulture);
        Mock<IParkingManager> parkingManager = new Mock<IParkingManager>();

        List<string> expectedOutput = [
            consoleCleared,
            messageService.GetMessage("MenuPrompt"),
            messageService.GetMessage("MenuOptionAdd"),
            messageService.GetMessage("MenuOptionRemove"),
            messageService.GetMessage("MenuOptionList"),
            messageService.GetMessage("MenuOptionPrices"),
            messageService.GetMessage("MenuOptionExit"),
            messageService.GetMessage("MenuInvalidOption"),
            messageService.GetMessage("PressAnyKey"),
            consoleCleared,
            messageService.GetMessage("MenuPrompt"),
            messageService.GetMessage("MenuOptionAdd"),
            messageService.GetMessage("MenuOptionRemove"),
            messageService.GetMessage("MenuOptionList"),
            messageService.GetMessage("MenuOptionPrices"),
            messageService.GetMessage("MenuOptionExit"),
            messageService.GetMessage("PressAnyKey"),
            messageService.GetMessage("GoodbyeMessage"),
        ];

		Menu.ShowMenu(userInputOutput, messageService, parkingManager.Object);

        Assert.Equal(expectedOutput, userInputOutput.Outputs);
	}
}
