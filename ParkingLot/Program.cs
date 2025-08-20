using System.Globalization;
using System.Resources;
using ParkingLot.Common;
using ParkingLot.Common.Models;
using ParkingLot.Common.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
ResourceManager resourceManager = new ResourceManager("ParkingLot.Common.Resources.Messages", typeof(ParkingManager).Assembly);
CultureInfo cultureInfo = CultureInfo.InvariantCulture;

bool showLanguageMenu = true;

while (showLanguageMenu)
{
    Console.Clear();
    Console.WriteLine("Choose your language | Escolha o idioma:");
    Console.WriteLine("1 - Português Brasileiro");
    Console.WriteLine("2 - English");
    Console.WriteLine("3 - Exit | Sair");
    switch (Console.ReadLine())
    {
        case "1":
            cultureInfo = new CultureInfo("pt-BR");
            showLanguageMenu = false;
            break;
        case "2":
        case "3":
            showLanguageMenu = false;
            break;

        default:
            Console.WriteLine("Invalid option | Opção inválida");
            break;
    }
}

MessageService messageService = new MessageService(resourceManager, cultureInfo);

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine(messageService.GetMessage("WelcomeMessage"));

decimal? initialPrice = null;
while (initialPrice == null)
{
    Console.WriteLine(messageService.GetMessage("PromptInitialPrice"));
    string initialPriceInput = Console.ReadLine();

    // Try parsing using the selected culture first, then fall back to en-US so users can enter either ',' or '.'
    if (!decimal.TryParse(initialPriceInput, System.Globalization.NumberStyles.Number, cultureInfo, out decimal parsedInitialPrice))
    {
        if (!decimal.TryParse(initialPriceInput, System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out parsedInitialPrice))
        {
            Console.WriteLine(messageService.GetMessage("PleaseProvideTheInitialPriceCorrectly"));
            initialPrice = null;
            continue;
        }
    }

    initialPrice = parsedInitialPrice;
}

decimal? pricePerHour = null;
while (pricePerHour == null)
{
    Console.WriteLine(messageService.GetMessage("PromptPricePerHour"));
    string pricePerHourInput = Console.ReadLine();

    if (!decimal.TryParse(pricePerHourInput, System.Globalization.NumberStyles.Number, cultureInfo, out decimal parsedPricePerHour))
    {
        if (!decimal.TryParse(pricePerHourInput, System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out parsedPricePerHour))
        {
            Console.WriteLine(messageService.GetMessage("PleaseProvideTheHoursCorrectly"));
            pricePerHour = null;
            continue;
        }
    }

    pricePerHour = parsedPricePerHour;
}

// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
ParkingManager parkingManager = new ParkingManager(new UserInputOutput(), messageService, initialPrice.Value, pricePerHour.Value);

bool showMenu = true;

// Realiza o loop do menu
while (showMenu)
{
    Console.Clear();
    Console.WriteLine(messageService.GetMessage("MenuPrompt"));
    Console.WriteLine(messageService.GetMessage("MenuOptionAdd"));
    Console.WriteLine(messageService.GetMessage("MenuOptionRemove"));
    Console.WriteLine(messageService.GetMessage("MenuOptionList"));
    Console.WriteLine(messageService.GetMessage("MenuOptionPrices"));
    Console.WriteLine(messageService.GetMessage("MenuOptionExit"));

    switch (Console.ReadLine())
    {
        case "1":
            parkingManager.AddVehicle();
            break;

        case "2":
            parkingManager.RemoveVehicle();
            break;

        case "3":
            parkingManager.ListVehicles();
            break;

        case "4":
            parkingManager.ShowPrices();
            break;

        case "5":
            showMenu = false;
            break;

        default:
            Console.WriteLine(messageService.GetMessage("MenuInvalidOption"));
            break;
    }

    Console.WriteLine(messageService.GetMessage("PressAnyKey"));
    Console.ReadLine();
}

Console.WriteLine(messageService.GetMessage("GoodbyeMessage"));
