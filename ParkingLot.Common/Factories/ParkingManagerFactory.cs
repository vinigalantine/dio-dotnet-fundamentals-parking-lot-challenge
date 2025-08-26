using System;
using System.Globalization;
using ParkingLot.Common.Interfaces;
using ParkingLot.Common.Models;

namespace ParkingLot.Common.Factories;

public class ParkingManagerFactory
{
    public static ParkingManager CreateParkingManager(IUserInputOutput userInputOutput, IMessageService messageService)
    {
        decimal? initialPrice = null;
        while (initialPrice == null)
        {
            userInputOutput.WriteLine(messageService.GetMessage("PromptInitialPrice"));
            string initialPriceInput = userInputOutput.ReadLine();

            if (!decimal.TryParse(initialPriceInput, NumberStyles.Number, messageService.CultureInfo, out decimal parsedInitialPrice))
            {
                if (!decimal.TryParse(initialPriceInput, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out parsedInitialPrice))
                {
                    userInputOutput.WriteLine(messageService.GetMessage("PleaseProvideTheInitialPriceCorrectly"));
                    initialPrice = null;
                    continue;
                }
            }

            initialPrice = parsedInitialPrice;
        }

        decimal? pricePerHour = null;
        while (pricePerHour == null)
        {
            userInputOutput.WriteLine(messageService.GetMessage("PromptPricePerHour"));
            string pricePerHourInput = userInputOutput.ReadLine();

            if (!decimal.TryParse(pricePerHourInput, NumberStyles.Number, messageService.CultureInfo, out decimal parsedPricePerHour))
            {
                if (!decimal.TryParse(pricePerHourInput, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out parsedPricePerHour))
                {
                    userInputOutput.WriteLine(messageService.GetMessage("PleaseProvideTheHoursCorrectly"));
                    pricePerHour = null;
                    continue;
                }
            }

            pricePerHour = parsedPricePerHour;
        }

        return new ParkingManager(new UserInputOutput(), messageService, initialPrice.Value, pricePerHour.Value);
    }
}
