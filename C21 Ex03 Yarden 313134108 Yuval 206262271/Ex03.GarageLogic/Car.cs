using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const int k_MaxWheelsAirPressure = 32;
        private const float k_MaxFuelCapacity = 45;
        private const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Octan95;
        private const float k_MaxBatteryHours = 3.2f;

        private eColors m_Color;
        private eNumberOfDoors m_NumOfDoors;

        public Car(string i_ModelName, string i_LicenseNumber, EnergyManager i_EnergyManager)
            : base(i_ModelName, i_LicenseNumber, k_NumberOfWheels, i_EnergyManager)
        {
            if(i_EnergyManager is FuelEnergy)
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
            for(int i = 0; i < k_NumberOfWheels; i++)
            {
                this.m_Wheels.Add(new Wheel());
                this.m_Wheels[i].MaxAirPressure = k_MaxWheelsAirPressure;
            }

            base.InitWheels(i_ManufacturerName, i_CurrentAirPressure);
        }

        public override void InitEnergySource(float i_CurrentEnergy)
        {
            if(this.m_EnergyManager is FuelEnergy)
            {
                ((FuelEnergy)m_EnergyManager).AddFuel(i_CurrentEnergy, k_FuelType);
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
                @"Insert the color of the car:{0}", Environment.NewLine);
            string[] colors = Enum.GetNames(typeof(eColors));
            int countColors = 1;
            foreach(string color in colors)
            {
                carParamsQuestions.AppendFormat(@"{0} -{1}{2}", countColors++, color, Environment.NewLine);
            }

            carSeparateParamsQuestions[0] = carParamsQuestions.ToString();
            carParamsQuestions.Clear();
            carParamsQuestions.AppendFormat(
                @"Insert number of doors:{0}", Environment.NewLine);
            string[] numOfDoors = Enum.GetNames(typeof(eNumberOfDoors));
            int countDoors = 1;
            foreach (string door in numOfDoors)
            {
                carParamsQuestions.AppendFormat(@"{0} -{1}{2}", countDoors++, door, Environment.NewLine);
            }

            carSeparateParamsQuestions[1] = carParamsQuestions.ToString();

            return carSeparateParamsQuestions;
        }

        public override void InitParams(string i_Params)
        {
            string[] givenParams = i_Params.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if(!Enum.TryParse(givenParams[0], out m_Color))
            {
                //todo: exception
            }

            if(!Enum.TryParse(givenParams[1], out m_NumOfDoors))
            {
                //todo: exception
            }
        }

        public override string ToString()
        {
            StringBuilder resString = new StringBuilder(base.ToString());

            resString.AppendFormat(
                @"Number of wheels - {0}
Car Color - {1}
Number of doors - {2}
",
                k_NumberOfWheels,
                m_Color,
                m_NumOfDoors);

            return resString.ToString();
        }

        private enum eColors
        {
            Red = 1,
            Silver,
            White,
            Black
        }

        private enum eNumberOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }
    }
}
