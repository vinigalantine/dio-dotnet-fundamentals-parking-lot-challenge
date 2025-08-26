using System.Globalization;
using ParkingLot.Common.Factories;
using ParkingLot.Common.Services;
using ParkingLot.Tests.TestHelpers;

namespace ParkingLot.Tests.Factories;

public class MessageServiceFactoryTests
{
    [Fact]
    public void CreateMessageService_SelectPortuguese_ReturnsPtBr()
    {
        var userInputOutput = new FakeUserInputOutput(["1"]);

        var service = MessageServiceFactory.CreateMessageService(userInputOutput);

        Assert.IsType<MessageService>(service);
        Assert.Equal("pt-BR", service.CultureInfo.Name);
        Assert.Equal("Console Cleared", userInputOutput.Outputs[0]);
        Assert.Equal("Choose your language | Escolha o idioma:", userInputOutput.Outputs[1]);
    }

    [Fact]
    public void CreateMessageService_SelectEnglish_ReturnsInvariantCulture()
    {
        var userInputOutput = new FakeUserInputOutput(["2"]);

        var service = MessageServiceFactory.CreateMessageService(userInputOutput);

        Assert.IsType<MessageService>(service);
        Assert.Equal(CultureInfo.InvariantCulture, service.CultureInfo);
    }

    [Fact]
    public void CreateMessageService_InvalidOption_ShowsInvalidMessageAndThenExits()
    {
        var userInputOutput = new FakeUserInputOutput(["x","3"]);

        var service = MessageServiceFactory.CreateMessageService(userInputOutput);

        Assert.IsType<MessageService>(service);
        Assert.Contains("Invalid option | Opção inválida", userInputOutput.Outputs);
        Assert.Equal(CultureInfo.InvariantCulture, service.CultureInfo);
    }
}
