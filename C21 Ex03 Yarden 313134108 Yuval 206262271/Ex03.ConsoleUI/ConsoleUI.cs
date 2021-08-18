using System;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
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
                    Console.Clear();
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
                                displayLicenseNumbers();
                                break;
                            }
                        
                        case eMenuOptions.ChangeVehicleState:
                            {
                                changeVehicleState();
                                break;
                            }
                        
                        case eMenuOptions.InflateWheels:
                            {
                                inflateWheels();
                                break;
                            }
                        
                        case eMenuOptions.FuelVehicle:
                            {
                                fuelVehicle();
                                break;
                            }
                        
                        case eMenuOptions.ChargeVehicle:
                            {
                                chargeVehicle();
                                break;
                            }
                        
                        case eMenuOptions.ViewVehicleInfo:
                            {
                                printVehicleInfo();
                                break;
                            }
                        
                        case eMenuOptions.Exit:
                            {
                                exit = true;
                                Console.WriteLine(@"Thanks for choosing our garage!
See you next time.");
                                break;
                            }
                        
                        default:
                            {
                                Console.WriteLine("invalid choice");
                                break;
                            }
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(@"{0}
{1}", 
                        ex.Source,
                        ex.Message );
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(@"{0}
{1}",
                        ex.ParamName,
                        ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(@"{0}", ex.Message);
                }
                finally
                {
                    continueIfKeyPressed();
                }
            }
        }

        private static void addVehicle()
        {
            getVehicleProperties(
                out VehicleFactory.eVehicleTypes o_VehicleType,
                out string o_LicenseNumber,
                out string o_ModelName);
            Vehicle toAdd = VehicleFactory.CreateVehicle(o_VehicleType, o_LicenseNumber, o_ModelName);
            getOwnerInformation(out string o_OwnerName, out string o_OwnerPhone);
            getWheelsAndEnergyInformation(out string o_ManufacturerName, out float o_CurrentPSI, out float o_CurrentEnergyState);
            toAdd.InitWheels(o_ManufacturerName, o_CurrentPSI);
            toAdd.InitEnergySource(o_CurrentEnergyState);
            string[] anotherQuestions = toAdd.GetParamsQuestions();
            string anotherParams = getAnotherVehicleInformation(anotherQuestions);
            toAdd.InitParams(anotherParams);
            sr_Garage.AddVehicle(toAdd, o_OwnerName, o_OwnerPhone, out bool o_isExists);
            if (o_isExists)
            {
                throw new ArgumentException("The vehicle is already in the garage");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Thanks {0}, your vehicle was added to our garage successfully.", o_OwnerName);
            }
        }

        private static void changeVehicleState()
        {
            string licensePlateNumber = getVehicleLicenseNumber();
            string[] vehicleStates = Enum.GetNames(typeof(Garage.VehicleInfo.eStateInGarage));
            int choiceNumber = 1;
            StringBuilder message = new StringBuilder();

            Console.WriteLine("Please choose the new state: ");
            foreach (string state in vehicleStates)
            {
                message.AppendFormat(@"{0} - {1}{2}", choiceNumber++, state, Environment.NewLine);
            }

            Console.WriteLine(message);
            Garage.VehicleInfo.eStateInGarage vehicleState = getVehicleState();
            sr_Garage.ChangeVehicleState(vehicleState, licensePlateNumber);
            Console.WriteLine("Your vehicle state updated to {0}.", vehicleState);
        }

        private static void fuelVehicle()
        {
            string licensePlateNumber = getVehicleLicenseNumber();
            string[] fuelTypes = Enum.GetNames(typeof(FuelEnergy.eFuelType));
            int choiceNumbering = 1;
            StringBuilder msg = new StringBuilder();
            
            Console.WriteLine("Please choose fuel type: (and then press 'enter')");
            foreach(string type in fuelTypes)
            {
                msg.AppendFormat(@"{0}. {1}{2}", choiceNumbering++, type, Environment.NewLine);
            }

            Console.WriteLine(msg);
            FuelEnergy.eFuelType fuelType = getFuelType();
            Console.WriteLine("Please enter amount of fuel to add - by liters: (and then press 'enter')");

            string userInput = Console.ReadLine();
            float amountToAdd;
            while (!float.TryParse(userInput, out amountToAdd))
            {
                userInput = Console.ReadLine();
            }

            sr_Garage.FuelVehicle(licensePlateNumber, fuelType, amountToAdd);
            Console.WriteLine("Fuel successful");
        }

        private static void chargeVehicle()
        {
            string licensePlateNumber = getVehicleLicenseNumber();

            Console.WriteLine("Please enter the amount in minutes to charge: (and then press enter)");
            string userInput = Console.ReadLine();
            float minutesToCharge;
            while (!float.TryParse(userInput, out minutesToCharge))
            {
                userInput = Console.ReadLine();
            }

            sr_Garage.ChargeVehicle(licensePlateNumber, minutesToCharge);
            Console.WriteLine("Charge successful");
        }

        private static void printVehicleInfo()
        {
            string licensePlateNumber = getVehicleLicenseNumber();
            string info = sr_Garage.GetVehicleInfo(licensePlateNumber);
            Console.WriteLine(info);
        }

        private static void getWheelsAndEnergyInformation(out string o_ManufacturerName, out float o_CurrentPSI, out float o_EnergyState)
        {
           Console.WriteLine("Please enter the manufacturer name of the wheels:");
           o_ManufacturerName = Console.ReadLine();
           Console.WriteLine("Please enter the current PSI the wheels:");
           if(!float.TryParse(Console.ReadLine(), out o_CurrentPSI))
           {
               throw new FormatException("The PSI should be a number");
           }

           Console.WriteLine(@"Please enter the current state of energy in your car:
For a fuel-powered vehicle, please enter the amount of fuel (by liters) in the fuel tank
For an  electric vehicle, please enter the time left (by hours) in the battery
");
            if (!float.TryParse(Console.ReadLine(), out o_EnergyState))
            {
                throw new FormatException("The current state of energy should be a number");
            }
        }

        private static string getAnotherVehicleInformation(string[] i_Questions)
        {
            StringBuilder answers = new StringBuilder();
            foreach(string question in i_Questions)
            {
                Console.Write(question);
                answers.AppendLine(Console.ReadLine());
            }

            return answers.ToString();
        }

        private static void displayLicenseNumbers()
        {
            Console.WriteLine("Would you like to filter the vehicles according to their state in the garage? (Y / N) ");
            string answer = Console.ReadLine();
            Garage.VehicleInfo.eStateInGarage? stateToDisplay = null;
           
            while (answer.ToUpper() != "Y" && answer.ToUpper() != "N")
            {
                Console.WriteLine("Invalid choice. Please chose Y / N ");
                answer = Console.ReadLine();
            }

            if(answer == "Y")
            {
                string[] vehicleStates = Enum.GetNames(typeof(Garage.VehicleInfo.eStateInGarage));
                int choiceNumber = 1;
                StringBuilder message = new StringBuilder();

                Console.WriteLine("Please choose the state to be display: ");
                foreach (string state in vehicleStates)
                {
                    message.AppendFormat(@"{0} - {1}{2}", choiceNumber++, state, Environment.NewLine);
                }

                Console.WriteLine(message);
                stateToDisplay = getVehicleState();
            }
            
            Console.WriteLine(sr_Garage.GetPlateNumbers(stateToDisplay));
        }

        private static void getOwnerInformation(out string o_OwnerName, out string o_OwnerPhone)
        {
            Console.WriteLine("Please enter your name:");
            o_OwnerName = Console.ReadLine();
            Console.WriteLine("Please enter your phone:");
            o_OwnerPhone = Console.ReadLine();
            if(!int.TryParse(o_OwnerPhone, out int result))
            {
                throw new FormatException("Phone should be a number,try again:");
            }
        }

        private static void getVehicleProperties(
            out VehicleFactory.eVehicleTypes o_VehicleType,
            out string o_LicenseNumber,
            out string o_ModelName)
        {
            o_LicenseNumber = getVehicleLicenseNumber();
            if (sr_Garage.IsVehicleExists(o_LicenseNumber))
            {
                Garage.VehicleInfo.eStateInGarage currentState = sr_Garage.GetStateInGarage(o_LicenseNumber);
                if(currentState != Garage.VehicleInfo.eStateInGarage.Repairing)
                {
                    sr_Garage.ChangeVehicleState(Garage.VehicleInfo.eStateInGarage.Repairing, o_LicenseNumber);
                    throw new ArgumentException("Your vehicle already in the garage, changing the state to 'repairing'");
                }
                else
                {
                    throw new FormatException("Your vehicle already in the garage, in repairing state");
                }
            }
            else
            {
                Console.WriteLine("Please enter model name:");
                o_ModelName = Console.ReadLine();
                o_VehicleType = geVehicleType();
            }
        }

        private static string getVehicleLicenseNumber()
        {
            Console.WriteLine("Please enter license plate number: ( and then press 'enter' )");
            return Console.ReadLine();
        }

        private static VehicleFactory.eVehicleTypes geVehicleType()
        {
            string[] vehiclesTypes = VehicleFactory.VehiclesTypes;
            StringBuilder msg = new StringBuilder();
            int typeCount = 1;

            foreach (string type in vehiclesTypes)
            {
                msg.AppendFormat(@"{0} - {1}{2}", typeCount++, type, Environment.NewLine);
            }

            Console.WriteLine("Please enter vehicle type:");
            Console.WriteLine(msg);
            if(!int.TryParse(Console.ReadLine(), out int result))
            {
                throw new FormatException("invalid vehicle type");
            }

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

        private static void inflateWheels()
        {
            string licensePlateNumber = getVehicleLicenseNumber();
           
            sr_Garage.FillUpAirPressureInWheels(licensePlateNumber);
        }

        private static void continueIfKeyPressed()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static FuelEnergy.eFuelType getFuelType()
        {
            if (!Enum.TryParse(Console.ReadLine(), out FuelEnergy.eFuelType fuelType))
            {
                throw new FormatException("Invalid fuel type");
            }

            if (!Enum.IsDefined(typeof(FuelEnergy.eFuelType), fuelType))
            {
                throw new FormatException("Invalid fuel type");
            }

            return fuelType;
        }

        private static Garage.VehicleInfo.eStateInGarage getVehicleState()
        {
            if (!Enum.TryParse(Console.ReadLine(), out Garage.VehicleInfo.eStateInGarage vehicleState))
            {
                throw new FormatException("Invalid vehicle state");
            }

            if (!Enum.IsDefined(typeof(Garage.VehicleInfo.eStateInGarage), vehicleState))
            {
                throw new FormatException("Invalid vehicle state");
            }

            return vehicleState;
        }
    }
}
