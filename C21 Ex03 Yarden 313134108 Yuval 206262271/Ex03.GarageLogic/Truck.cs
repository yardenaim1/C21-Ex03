using System;
using System.Linq;
using System.Text;

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

        public Truck(string i_ModelName, string i_LicenseNumber, EnergyManager i_EnergyManager)
          : base(i_ModelName, i_LicenseNumber, k_NumberOfWheels, i_EnergyManager)
        {
            ((FuelEnergy)m_EnergyManager).MaxFuel = k_MaxFuelCapacity;
            ((FuelEnergy)m_EnergyManager).FuelType = k_FuelType;
        }

        public override void InitWheels(
            string i_ManufacturerName,
            float i_CurrentAirPressure)
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
            ((FuelEnergy)m_EnergyManager).AddFuel(k_FuelType, i_CurrentEnergy);
            this.m_PercentageEnergyRemaining = this.m_EnergyManager.GetEnergyPercentage();
        }

        public bool IsDrivesHazardousMaterials
        {
            get
            {
                return m_IsDrivesHazardousMaterials;
            }

            set
            {
                m_IsDrivesHazardousMaterials = value;
            }
        }

        public float MaximumCarryingWeight
        {
            get
            {
                return m_MaximumCarryingWeight;
            }

            set
            {
                m_MaximumCarryingWeight = value;
            }
        }

        public FuelEnergy.eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
        }

        public override string[] GetParamsQuestions()
        {
            StringBuilder truckParamsQuestions = new StringBuilder();
            string[] truckSeparateParamsQuestions = new string[2];

            truckParamsQuestions.AppendFormat(
                @"The truck transporting hazardous materials? ( Y / N ){0}", Environment.NewLine);
            truckSeparateParamsQuestions[0] = truckParamsQuestions.ToString();
            truckParamsQuestions.Clear();

            truckParamsQuestions.AppendFormat(
                @"What is the maximum Carry weight? ( in kilograms ){0}", Environment.NewLine);
            truckSeparateParamsQuestions[1] = truckParamsQuestions.ToString();
            return truckSeparateParamsQuestions;
        }

        public override void InitParams(string i_Params) 
        {
            string[] givenParams = i_Params.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if(givenParams[0].ToLower() != "y" && givenParams[0].ToLower() != "n")
            {
                // todo : trow exception invalid transport choice 
            }

            m_IsDrivesHazardousMaterials = givenParams[0].ToLower() == "y" ? true : false;

            if (!float.TryParse(givenParams[1], out this.m_MaximumCarryingWeight))
            {
                // todo : trow exception invalid carry weight
            }

            // === ================================================================================================================ ===
            // === if we choose to check the params validation before so just                                                       ===
            // ===                                                                                                                  ===
            // === string[] givenParams = i_Params.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);     ===
            // === m_IsDrivesHazardousMaterials = givenParams[0].ToLower() == "y" ? true : false;                                   ===
            // === float.TryParse(givenParams[1], out this.m_MaximumCarryingWeight);                                                ===
            // ===                                                                                                                  ===
            // === ================================================================================================================ ===
        }

        public override string ToString()
        {
            StringBuilder resString = new StringBuilder(base.ToString());

            resString.AppendFormat(
                @"Number of wheels - {0}
Is Drives Hazardous Materials - {1}
Maximum Carrying Weight - {2}
",
                k_NumberOfWheels,
                this.m_IsDrivesHazardousMaterials ? "yes" : "no",
                m_MaximumCarryingWeight);
           
            return resString.ToString();
        }
    }
}
