using ParkingLot.Common.Factories;
using ParkingLot.Common.Models;
using ParkingLot.Tests.TestHelpers;

namespace ParkingLot.Tests.Factories;

public class ParkingManagerFactoryTests
{
    [Fact]
    public void CreateParkingManager_ValidInputs_ReturnsParkingManagerAndShowsPrompts()
    {
        var languageUserInputOutput = new FakeUserInputOutput(["2"]);
        var messageService = MessageServiceFactory.CreateMessageService(languageUserInputOutput);

        var factoryUserInputOutput = new FakeUserInputOutput(["2.50", "1.50"]);

        var parkingManager = ParkingManagerFactory.CreateParkingManager(factoryUserInputOutput, messageService);

        Assert.IsType<ParkingManager>(parkingManager);
        Assert.Contains(messageService.GetMessage("PromptInitialPrice"), factoryUserInputOutput.Outputs);
        Assert.Contains(messageService.GetMessage("PromptPricePerHour"), factoryUserInputOutput.Outputs);
    }

    [Fact]
    public void CreateParkingManager_InvalidThenValidInputs_ShowsErrorMessagesAndSucceeds()
    {
        var languageUserInputOutput = new FakeUserInputOutput(["2"]);
        var messageService = MessageServiceFactory.CreateMessageService(languageUserInputOutput);

        var factoryUserInputOutput = new FakeUserInputOutput(["invalid", "3.00", "bad", "2.00"]);

        var parkingManager = ParkingManagerFactory.CreateParkingManager(factoryUserInputOutput, messageService);

        Assert.IsType<ParkingManager>(parkingManager);
        Assert.Contains(messageService.GetMessage("PleaseProvideTheInitialPriceCorrectly"), factoryUserInputOutput.Outputs);
        Assert.Contains(messageService.GetMessage("PleaseProvideTheHoursCorrectly"), factoryUserInputOutput.Outputs);
    }

    [Fact]
    public void CreateParkingManager_PtBrCulture_ParsesDotInputUsingEnUsFallback()
    {
        var languageUserInputOutput = new FakeUserInputOutput(["1"]);
        var messageService = MessageServiceFactory.CreateMessageService(languageUserInputOutput);

        var factoryUserInputOutput = new FakeUserInputOutput(["2.50", "1.25"]);

        var parkingManager = ParkingManagerFactory.CreateParkingManager(factoryUserInputOutput, messageService);

        Assert.IsType<ParkingManager>(parkingManager);
        Assert.Contains(messageService.GetMessage("PromptInitialPrice"), factoryUserInputOutput.Outputs);
        Assert.Contains(messageService.GetMessage("PromptPricePerHour"), factoryUserInputOutput.Outputs);
    }
}
