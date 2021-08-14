using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    using System;
    using System.Reflection.Emit;

    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_PercentageEnergyRemaining;
        public List<Wheel> m_Wheels = null;
        protected EnergyManager m_EnergyManager = null;

        internal Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels)
        {
            this.m_ModelName = i_ModelName;
            this.m_LicenseNumber = i_LicenseNumber;
            this.m_Wheels = new List<Wheel>(i_NumOfWheels);

        }

        public virtual void InitWheelsAndEnergy(
            string i_ManufacturerName,
            float i_CurrentAirPressure,
            float i_CurrentEnergy)
        {
        }
    }
}
