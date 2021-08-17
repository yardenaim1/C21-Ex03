using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    using System.ComponentModel;
    using System.Dynamic;
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
            while(!exit)
            {
                try
                {
                    displayMenu();
                    if(!int.TryParse(Console.ReadLine(), out int userInput))
                    {
                        throw new FormatException("You must enter a number");
                    }

                    eMenuOptions chosenMenuOption = (eMenuOptions)userInput;
                    Console.Clear();
                    switch(chosenMenuOption)
                    {
                        case eMenuOptions.AddVehicle:
                            {
                                addVehicle();
                                break;
                            }

                        case eMenuOptions.DisplayLicenseNumbers:
                            {
                                break;
                            }
                        
                        case eMenuOptions.ChangeVehicleState:
                            {
                                break;
                            }
                        
                        case eMenuOptions.InflateWheels:
                            {
                                break;
                            }
                        
                        case eMenuOptions.FuelVehicle:
                            {
                                break;
                            }
                        
                        case eMenuOptions.ChargeVehicle:
                            {
                                break;
                            }
                        
                        case eMenuOptions.ViewVehicleInfo:
                            {
                                break;
                            }
                        
                        case eMenuOptions.Exit:
                            {
                                break;
                            }
                        
                        default:
                            {
                                break;
                            }
                    }

                }
                catch
                {

                }
            }
        }

        private static void addVehicle()
        {
            getOwnerInformation(out string o_OwnerName, out string o_OwnerPhone);
            getVehicleProperties(
                out VehicleFactory.eVehicleTypes o_VehicleType,
                out string o_LicenseNumber,
                out string o_ModelName);
            Vehicle toAdd = VehicleFactory.CreateVehicle(o_VehicleType, o_LicenseNumber, o_ModelName);
            sr_Garage.AddVehicle(toAdd, o_OwnerName, o_OwnerPhone, out bool o_isExists);
            if(o_isExists)
            {
                Console.WriteLine("The vehicle is already in the garage, the state changed to repairing"); 
                return;
            }
        
            getWheelsInformation(out string o_ManufacturerName, out float o_CurrentPSI);
            sr_Garage.InitWheels(o_LicenseNumber, o_ManufacturerName, o_CurrentPSI);
            string[]anotherQuestions = toAdd.GetParamsQuestions();
            string anotherAnswers = getAnotherVehicleInformation(anotherQuestions);
            sr_Garage.InitOtherParams(o_LicenseNumber, anotherAnswers);
        }

        private static void getWheelsInformation(out string o_ManufacturerName, out float o_CurrentPSI)
        {
           Console.WriteLine(" Please enter the manufacturer name of the wheels:");
           o_ManufacturerName = Console.ReadLine();
           Console.WriteLine("Please enter the current PSI the wheels:");
           if(!float.TryParse(Console.ReadLine(), out o_CurrentPSI))
           {
               throw new FormatException("The PSI should be a number");
           }
        }

        private static string getAnotherVehicleInformation(string[] i_Questions)
        {
            StringBuilder answers = new StringBuilder();
            foreach(string question in i_Questions)
            {
                Console.WriteLine(question);
                answers.AppendLine(Console.ReadLine());
            }

            return answers.ToString();
        }

        private static void getOwnerInformation(out string o_OwnerName, out string o_OwnerPhone)
        {
            Console.WriteLine("Please enter your name:");
            o_OwnerName = Console.ReadLine();
            Console.WriteLine("Please enter your phone:");
            o_OwnerPhone = Console.ReadLine();
            try
            {
                if(!int.TryParse(o_OwnerPhone, out int result))
                {
                    throw new FormatException("Phone should be a number,try again:");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                getOwnerInformation(out o_OwnerName, out o_OwnerName);
            }
        }

        private static void getVehicleProperties(
            out VehicleFactory.eVehicleTypes o_VehicleType,
            out string o_LicenseNumber,
            out string o_ModelName)
        {
            o_LicenseNumber = getVehicleLicenseNumber();
            Console.WriteLine("Please enter model name:");
            o_ModelName = Console.ReadLine();
            o_VehicleType = geVehicleType();
        }

        private static string getVehicleLicenseNumber()
        {
            string licenseNumber;
            Console.WriteLine("Please enter license number:");
            do
            {
                Console.WriteLine("License number should be a number");
            }
            while (!int.TryParse(licenseNumber = Console.ReadLine(), out int result));

            return licenseNumber;
        }

        private static VehicleFactory.eVehicleTypes geVehicleType()
        {
            string[] vehiclesTypes = VehicleFactory.VehiclesTypes;
            StringBuilder msg = new StringBuilder();
            int typeCount = 1;
            int result;

            foreach (string type in vehiclesTypes)
            {
                msg.AppendFormat(@"{0} - {1}{2}", typeCount++, type, Environment.NewLine);
            }

            Console.WriteLine("Please enter vehicle type:");
            Console.WriteLine(msg);
            do
            {
                Console.WriteLine("Invalid choice");
            }
            while (!int.TryParse(Console.ReadLine(), out result));

            VehicleFactory.eVehicleTypes vehicleType = (VehicleFactory.eVehicleTypes)result;

            return vehicleType;
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
