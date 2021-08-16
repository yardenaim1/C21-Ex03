using System.Collections.Generic;
using System;
using System.Reflection.Emit;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        protected float m_PercentageEnergyRemaining;
        protected List<Wheel> m_Wheels = null;
        protected EnergyManager m_EnergyManager = null;

        internal Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels, EnergyManager i_EnergyManager)
        {
            this.m_ModelName = i_ModelName;
            this.m_LicenseNumber = i_LicenseNumber;
            this.m_Wheels = new List<Wheel>(i_NumOfWheels);
            this.m_EnergyManager = i_EnergyManager;
        }

        public string LicenseNumber
        {
            get
            {
                return this.m_LicenseNumber;
            }
        }

        public virtual void InitWheels(
            string i_ManufacturerName,
            float i_CurrentAirPressure)
        {
            foreach(Wheel wheel in this.m_Wheels)
            {
                wheel.ManufacturerName = i_ManufacturerName;
                wheel.FillAirPressure(i_CurrentAirPressure);
            }
        }

        public virtual void InitEnergySource(float i_CurrentEnergy)
        {
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public EnergyManager EnergyManager
        {
            get
            {
                return this.m_EnergyManager;
            }
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
            resString.Append(this.m_Wheels[0].ToString());
            resString.Append(this.m_EnergyManager.ToString());
          
            return resString.ToString();
        }
    }
}
