namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicleTypes
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static Vehicle CreateVehicle(eVehicleTypes i_VehicleType, string i_PlateNumber, string i_ModelName, string i_ManufacturerName)
        {
            Vehicle newVehicle = null;

            switch(i_VehicleType)
            {
                case eVehicleTypes.FuelCar:
                    {
                        newVehicle = new Car(i_ModelName, i_PlateNumber, new FuelEnergy());
                        // init energy and wheels will be outside???
                        break;
                    }
                case eVehicleTypes.ElectricCar:
                    {
                        newVehicle = new Car(i_ModelName, i_PlateNumber, new ElectricEnergy());
                        break;
                    }
                case eVehicleTypes.FuelMotorcycle:
                    {
                        newVehicle = new Motorcycle(i_ModelName, i_PlateNumber, new FuelEnergy());
                        break;
                    }
                case eVehicleTypes.ElectricMotorcycle:
                    {
                        newVehicle = new Motorcycle(i_ModelName, i_PlateNumber, new ElectricEnergy());
                        break;
                    }
                case eVehicleTypes.Truck:
                    {
                        newVehicle = new Truck(i_ModelName, i_PlateNumber, new FuelEnergy());
                        break;
                    }
            }

            return newVehicle;
        }
    }
}
