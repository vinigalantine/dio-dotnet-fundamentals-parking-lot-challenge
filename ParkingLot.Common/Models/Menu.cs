using System;
using ParkingLot.Common.Interfaces;

namespace ParkingLot.Common.Models;

public static class Menu
{
    public static void ShowMenu(IUserInputOutput userInputOutput, IMessageService messageService, IParkingManager parkingManager)
    {
        bool showMenu = true;
        while (showMenu)
        {
            userInputOutput.Clear();
            userInputOutput.WriteLine(messageService.GetMessage("MenuPrompt"));
            userInputOutput.WriteLine(messageService.GetMessage("MenuOptionAdd"));
            userInputOutput.WriteLine(messageService.GetMessage("MenuOptionRemove"));
            userInputOutput.WriteLine(messageService.GetMessage("MenuOptionList"));
            userInputOutput.WriteLine(messageService.GetMessage("MenuOptionPrices"));
            userInputOutput.WriteLine(messageService.GetMessage("MenuOptionExit"));

            switch (userInputOutput.ReadLine())
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
                    userInputOutput.WriteLine(messageService.GetMessage("MenuInvalidOption"));
                    break;
            }

            userInputOutput.WriteLine(messageService.GetMessage("PressAnyKey"));
            userInputOutput.ReadLine();
        }

        userInputOutput.WriteLine(messageService.GetMessage("GoodbyeMessage"));
    }
}
