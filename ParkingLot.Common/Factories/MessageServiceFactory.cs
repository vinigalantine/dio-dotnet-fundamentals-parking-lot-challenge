using System.Globalization;
using System.Resources;
using ParkingLot.Common.Interfaces;
using ParkingLot.Common.Models;
using ParkingLot.Common.Services;

namespace ParkingLot.Common.Factories;

public class MessageServiceFactory
{
    public static MessageService CreateMessageService(IUserInputOutput userInputOutput)
    {
        ResourceManager resourceManager = new ResourceManager("ParkingLot.Common.Resources.Messages", typeof(ParkingManager).Assembly);
        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        bool showLanguageMenu = true;

        while (showLanguageMenu)
        {
            userInputOutput.Clear();
            userInputOutput.WriteLine("Choose your language | Escolha o idioma:");
            userInputOutput.WriteLine("1 - Português Brasileiro");
            userInputOutput.WriteLine("2 - English");
            userInputOutput.WriteLine("3 - Exit | Sair");
            switch (userInputOutput.ReadLine())
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
                    userInputOutput.WriteLine("Invalid option | Opção inválida");
                    break;
            }
        }

        return new MessageService(resourceManager, cultureInfo);
    }
}
