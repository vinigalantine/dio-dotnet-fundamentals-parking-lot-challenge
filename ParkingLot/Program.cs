using ParkingLot.Common;
using ParkingLot.Common.Factories;
using ParkingLot.Common.Models;
using ParkingLot.Common.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

UserInputOutput userInputOutput = new UserInputOutput();
MessageService messageService = MessageServiceFactory.CreateMessageService(userInputOutput);
ParkingManager parkingManager = ParkingManagerFactory.CreateParkingManager(userInputOutput, messageService);

Menu.ShowMenu(userInputOutput, messageService, parkingManager);

Console.WriteLine(messageService.GetMessage("WelcomeMessage"));


