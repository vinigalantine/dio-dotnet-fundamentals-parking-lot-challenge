using System.Globalization;
using System.Resources;
using ParkingLot.Common.Models;
using ParkingLot.Common.Services;

namespace ParkingLot.Tests.Services;

public class MessageServiceTests
{
	private readonly ResourceManager resourceManager = new ResourceManager("ParkingLot.Common.Resources.Messages", typeof(ParkingManager).Assembly);

	[Fact]
	public void GetMessage_ReturnsResourceValue()
	{
		var service = new MessageService(resourceManager, CultureInfo.InvariantCulture);

		var actual = service.GetMessage("WelcomeMessage");

		Assert.Equal("Welcome to Parking Lot!", actual);
	}

	[Fact]
	public void GetMessage_WithStringArg_FormatsWhenArgProvided()
	{
		var service = new MessageService(resourceManager, CultureInfo.InvariantCulture);

		var actual = service.GetMessage("InitialPrice", "5");

		Assert.Equal("Initial price: $ 5", actual);
	}

	[Fact]
	public void GetMessage_WithStringArg_ReturnsTemplateWhenArgNullOrEmpty()
	{
		var service = new MessageService(resourceManager, CultureInfo.InvariantCulture);

		var actualNull = service.GetMessage("InitialPrice", (string?)null);
		var actualEmpty = service.GetMessage("InitialPrice", string.Empty);

		Assert.Equal("Initial price: $ {0}", actualNull);
		Assert.Equal("Initial price: $ {0}", actualEmpty);
	}

	[Fact]
	public void GetMessage_WithArgsArray_FormatsMultipleArgs()
	{
		var service = new MessageService(resourceManager, CultureInfo.InvariantCulture);

		var actual = service.GetMessage("VehicleRemovedAndTotalatoPay", ["ABC-123", "10"]);

		Assert.Equal("The vehicle with license plate ABC-123 has been removed and the total price was: $ 10", actual);
	}

	[Fact]
	public void GetMessage_WithEmptyArgsArray_ReturnsTemplate()
	{
		var service = new MessageService(resourceManager, CultureInfo.InvariantCulture);

		var actual = service.GetMessage("VehicleRemovedAndTotalatoPay", Array.Empty<object>());

		Assert.Equal("The vehicle with license plate {0} has been removed and the total price was: $ {1}", actual);
	}

	[Fact]
	public void GetMessage_CultureSpecific_ReturnsPortuguese()
	{
		var service = new MessageService(resourceManager, CultureInfo.GetCultureInfo("pt-BR"));

		var actual = service.GetMessage("MenuPrompt");

		Assert.Equal("Digite a sua opção:", actual);
	}

	[Fact]
	public void GetMessage_MissingKey_ReturnsNull()
	{
		var service = new MessageService(resourceManager, CultureInfo.InvariantCulture);

		var actual = service.GetMessage("ThisKeyDoesNotExist");

		Assert.Null(actual);
	}
}
