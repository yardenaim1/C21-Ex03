using System.Text;
using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    using System.Runtime.CompilerServices;

    public static class ConsoleUI
    {
        public static void Run()
        {
            Garage garage = new Garage();
            Vehicle v1 = new Truck("BMW", "1234", new FuelEnergy(FuelEnergy.eFuelType.Soler, 222f));
            bool isExist;
            garage.AddVehicle(v1, "Yarden", "052-333",out isExist);
            garage.InitWheels("1234","bla",20f);
            garage.InitEnergySource("1234",40);

            string info = garage.GetVehicleInfo("1234");
            Console.WriteLine(info);

        }
    }
}
