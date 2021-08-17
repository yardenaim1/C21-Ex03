using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public static class ConsoleUI
    {
        private enum eMenuOptions
        {
            AddVehicle = 1,

            DisplayLicenseNumbers,

            ChangeVehicleState,

            InflateWheels,

            FuelVehicle,

            ChargeVehicle,

            ViewVehicleInfo,

            Exit
        }

        private static readonly Garage sr_Garage = new Garage();

        private static readonly string[] sr_MenuOfGarage =
            {
                @"--------------Welcome To The Garage !--------------

Please chose one of the options:",
@"{0}. Add vehicle ", 
@"{0}. Display vehicles license numbers in the garage",
@"{0}. Change the state of a vehicle", 
@"{0}. Inflate the wheels of a vehicle to the maximum ",
@"{0}. Fuel a vehicle ", 
@"{0}. Charge a vehicle ", 
@"{0}. View vehicle Information ", 
@"{0}. Exit "
            };

        public static void Run()
        {
            bool exit = false;
            eMenuOptions chosenMenuOption;

            while(!exit)
            {
                try
                {
                    displayMenu();
                    if(!int.TryParse(Console.ReadLine(), out int userInput))
                    {
                        throw new FormatException("You must enter a number");
                    }

                    chosenMenuOption = (eMenuOptions)userInput;
                    switch(chosenMenuOption)
                    {
                     
                    }

                }
                catch
                {

                }
            }
        }

        private static void displayMenu()
        {
            int optionNumber = 0;
            foreach(string option in sr_MenuOfGarage)
            {
                Console.WriteLine(option, optionNumber++);
            }
        }
    }
}
