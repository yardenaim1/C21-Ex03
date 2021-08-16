using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        private const int k_MaxWheelsAirPressure = 30;
        private const float k_MaxFuelCapacity = 6;
        private const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Octan98;
        private const float k_MaxBatteryHours = 1.8f;

        private eLicenseType m_LicenseType;
        private int m_CubicCapacity;

        public Motorcycle(string i_ModelName, string i_LicenseNumber, EnergyManager i_EnergyManager)
            : base(i_ModelName, i_LicenseNumber, k_NumberOfWheels, i_EnergyManager)
        {
            if (i_EnergyManager is FuelEnergy)
            {
                ((FuelEnergy)m_EnergyManager).FuelType = k_FuelType;
                ((FuelEnergy)m_EnergyManager).MaxFuel = k_MaxFuelCapacity;
            }
            else
            {
                ((ElectricEnergy)this.m_EnergyManager).MaxBatteryHours = k_MaxBatteryHours;
            }
        }

        public override void InitWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                this.m_Wheels.Add(new Wheel());
                this.m_Wheels[i].MaxAirPressure = k_MaxWheelsAirPressure;
            }

            base.InitWheels(i_ManufacturerName, i_CurrentAirPressure);
        }

        public override void InitEnergySource(float i_CurrentEnergy)
        {
            if (this.m_EnergyManager is FuelEnergy)
            {
                ((FuelEnergy)m_EnergyManager).AddFuel(k_FuelType, i_CurrentEnergy);
            }
            else
            {
                ((ElectricEnergy)m_EnergyManager).Charge(i_CurrentEnergy);
            }

            this.m_PercentageEnergyRemaining = this.m_EnergyManager.GetEnergyPercentage();
        }

        public override string[] GetParamsQuestions()
        {
            StringBuilder carParamsQuestions = new StringBuilder();
            string[] carSeparateParamsQuestions = new string[2];

            carParamsQuestions.AppendFormat(
                @"Insert the license type of the motorcycle:{0}", Environment.NewLine);
            string[] LicenseTypes = Enum.GetNames(typeof(eLicenseType));
            int countTypes = 1;
            foreach (string type in LicenseTypes)
            {
                carParamsQuestions.AppendFormat(@"{0} -{1}{2}", countTypes++, type, Environment.NewLine);
            }

            carSeparateParamsQuestions[0] = carParamsQuestions.ToString();
            carParamsQuestions.Clear();
            carParamsQuestions.AppendFormat(
                @"Insert the Cubic Capacity:{0}", Environment.NewLine);
            carSeparateParamsQuestions[1] = carParamsQuestions.ToString();

            return carSeparateParamsQuestions;
        }

        public override void InitParams(string i_Params)
        {
            string[] givenParams = i_Params.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (!Enum.TryParse(givenParams[0], out m_LicenseType))
            {
                throw new FormatException("Invalid license type");
            }

            if (!int.TryParse(givenParams[1], out m_CubicCapacity))
            {
                throw new FormatException("Invalid cubic capacity");
            }
        }

        public override string ToString()
        {
            StringBuilder resString = new StringBuilder(base.ToString());

            resString.AppendFormat(
                @"Number of wheels - {0}
License type - {1}
Cubic Capacity - {2}
",
                k_NumberOfWheels,
                m_LicenseType,
                m_CubicCapacity);

            return resString.ToString();
        }

        private enum eLicenseType
        {
            A = 1,
            B1,
            AA,
            BB
        }
    }
}
