using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 16;
        private const int k_MaxWheelsAirPressure = 26;
        private const float k_MaxFuelCapacity = 120;
        private const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Soler;
        
        private bool m_IsDrivesHazardousMaterials;
        private float m_MaximumCarryingWeight;

        public Truck(string i_ModelName, string i_LicenseNumber)
          : base(i_ModelName, i_LicenseNumber, k_NumberOfWheels)
        {
            this.m_EnergyManager = new FuelEnergy(k_FuelType, k_MaxFuelCapacity);
        }

        public override void InitWheelsAndEnergy(
            string i_ManufacturerName,
            float i_CurrentAirPressure,
            float i_CurrentEnergy)
        {
            for(int i = 0; i < k_NumberOfWheels; i++) 
            {
                this.m_Wheels.Add(new Wheel());
                this.m_Wheels[i].ManufacturerName = i_ManufacturerName;
                this.m_Wheels[i].MaxAirPressure = k_MaxWheelsAirPressure;
                this.m_Wheels[i].CurrentAirPressure = i_CurrentEnergy;
            }

            ((FuelEnergy)this.m_EnergyManager).AddFuel(i_CurrentEnergy, k_FuelType);
        }
    }
}
