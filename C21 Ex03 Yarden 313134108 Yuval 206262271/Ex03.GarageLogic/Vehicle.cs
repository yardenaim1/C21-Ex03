using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    using System;
    using System.Reflection.Emit;
    using System.Text;

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

        public abstract string[] GetParamsQuestions();

        public abstract void InitParams(string i_Params);

        public override string ToString()
        {
            StringBuilder resString = new StringBuilder(string.Format(
@"Plate number - {0}
Model - {1}
",
                                    this.m_LicenseNumber,
                                    this.m_ModelName));
            resString.Append(this.m_Wheels[0].ToString();
            resString.Append(this.m_EnergyManager.ToString();
            return resString.ToString();
        }
    }
}
