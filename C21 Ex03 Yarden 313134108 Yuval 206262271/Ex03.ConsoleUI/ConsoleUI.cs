using System.Text;
using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class ConsoleUI
    {
        public static void Run()
        {
            Garage garage = new Garage();
            Vehicle v1 = new Truck("BMW", "1234", new FuelEnergy(FuelEnergy.eFuelType.Soler, 222f));
            bool isExist;
            garage.AddVehicle(v1, "Yarden", "052-333",out isExist);
            string plateList = garage.GetPlateNumbers(Garage.VehicleInfo.eStateInGarage.Repairing);
            Console.WriteLine(plateList);
        }
    }
}
