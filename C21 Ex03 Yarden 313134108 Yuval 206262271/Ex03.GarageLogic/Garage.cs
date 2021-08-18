using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInfo> r_GarageVehicles;
 
        public Garage()
        {
            this.r_GarageVehicles = new Dictionary<string, VehicleInfo>();
        }

        public VehicleInfo.eStateInGarage GetStateInGarage(string i_LicensePlateNumber)
        {
            return r_GarageVehicles[i_LicensePlateNumber].StateInGarage;
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone, out bool o_IsExists)
        {
            o_IsExists = this.r_GarageVehicles.ContainsKey(i_Vehicle.LicensePlateNumber);

            if (!o_IsExists)
            {
                this.r_GarageVehicles.Add(
                    i_Vehicle.LicensePlateNumber,
                    new VehicleInfo(i_Vehicle, i_OwnerName, i_OwnerPhone));
            }
            else
            {
                this.r_GarageVehicles[i_Vehicle.LicensePlateNumber].StateInGarage = VehicleInfo.eStateInGarage.Repairing;
            }
        }

        public string GetPlateNumbers(VehicleInfo.eStateInGarage? i_State)
        {
            StringBuilder plateNumbers = new StringBuilder(string.Empty);
            int platesNumbering = 1;

            foreach(KeyValuePair<string, VehicleInfo> vehicle in this.r_GarageVehicles)
            {
                if (i_State == null || vehicle.Value.StateInGarage == i_State)
                {
                    plateNumbers.AppendFormat(
                        @"{0}. {1}{2}",
                        platesNumbering++,
                        vehicle.Value.GetVehicle.LicensePlateNumber,
                        Environment.NewLine);
                }
            }

            if (string.IsNullOrEmpty(plateNumbers.ToString()))
            {
                plateNumbers.AppendLine("No plate numbers to show.");
            }

            return plateNumbers.ToString();
        }

        public void ChangeVehicleState(VehicleInfo.eStateInGarage i_NewState, string i_LicensePlateNumber)
        {
            if (!IsVehicleExists(i_LicensePlateNumber))
            {
                throw new ArgumentException("No matching vehicle found");
            }
            
            if (this.r_GarageVehicles[i_LicensePlateNumber].StateInGarage == i_NewState)
            {
                throw new ArgumentException("This is already the state of your vehicle");
            }
            else
            {
                this.r_GarageVehicles[i_LicensePlateNumber].StateInGarage = i_NewState;
            }
        }

        public void FillUpAirPressureInWheels(string i_LicensePlateNumber)
        {
            if(!IsVehicleExists(i_LicensePlateNumber))
            {
                throw new ArgumentException("No matching vehicle found");
            }

            List<Wheel> wheelList = this.r_GarageVehicles[i_LicensePlateNumber].GetVehicle.Wheels;
            foreach(Wheel wheel in wheelList)
            {
                wheel.FillAirPressureToMax();
            }
        }

        public void FuelVehicle(string i_LicensePlateNumber, FuelEnergy.eFuelType i_FuelType, float i_AmountToFuel)
        {
            if(!IsVehicleExists(i_LicensePlateNumber))
            {
                throw new ArgumentException("No matching vehicle found");
            }

            Vehicle toFuel = this.r_GarageVehicles[i_LicensePlateNumber].GetVehicle;
            FuelEnergy toFill = toFuel.EnergyManager as FuelEnergy;

            if (toFill != null)
            {
                toFill.AddFuel(i_FuelType, i_AmountToFuel);
            }
            else
            {
                throw new ArgumentException("Vehicle requested is not fuel operated");
            }
        }

        public void ChargeVehicle(string i_LicensePlateNumber, float i_MinutesToCharge)
        {
            if(!IsVehicleExists(i_LicensePlateNumber))
            {
                throw new ArgumentException("No matching vehicle found");
            }

            Vehicle toFuel = this.r_GarageVehicles[i_LicensePlateNumber].GetVehicle;
            ElectricEnergy toFill = toFuel.EnergyManager as ElectricEnergy;

            if (toFill != null)
            {
                toFill.Charge(i_MinutesToCharge / 60);
            }
            else
            {
                throw new ArgumentException("Vehicle requested is not electrically operated");
            }
        }

        public string GetVehicleInfo(string i_LicensePlateNumber)
        {
            if (!IsVehicleExists(i_LicensePlateNumber))
            {
                throw new ArgumentException("No matching vehicle found");
            }

            VehicleInfo vehicleInfo = this.r_GarageVehicles[i_LicensePlateNumber];
            return vehicleInfo.ToString();
        }

        public bool IsVehicleExists(string i_LicensePlateNumber)
        {
            return this.r_GarageVehicles.ContainsKey(i_LicensePlateNumber);
        }

        public class VehicleInfo
        {
            private readonly Vehicle r_Vehicle;
            private string m_OwnerName;
            private string m_OwnerPhoneNumber;
            private eStateInGarage m_CurrentStateInGarage;

            public VehicleInfo(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
            {
                this.r_Vehicle = i_Vehicle;
                this.m_OwnerName = i_OwnerName;
                this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
                this.m_CurrentStateInGarage = eStateInGarage.Repairing;
            }

            public Vehicle GetVehicle
            {
                get
                {
                    return this.r_Vehicle;
                }
            }

            public string OwnerName
            {
                get
                {
                    return this.m_OwnerName;
                }

                set
                {
                    this.m_OwnerName = value;
                }
            }

            public string OwnerPhoneNumber
            {
                get
                {
                    return this.m_OwnerPhoneNumber;
                }

                set
                {
                    this.m_OwnerPhoneNumber = value;
                }
            }

            public override string ToString()
            {
                StringBuilder resString = new StringBuilder();

                resString.AppendFormat(
                    @"Owner Name - {0}
State in garage - {1}
",
                    this.m_OwnerName,
                    this.m_CurrentStateInGarage);
                resString.AppendLine(this.r_Vehicle.ToString());

                return resString.ToString();
            }

            public eStateInGarage StateInGarage
            {
                get
                {
                    return this.m_CurrentStateInGarage;
                }

                set
                {
                    this.m_CurrentStateInGarage = value;
                }
            }
            
            public enum eStateInGarage
            {
                Repairing = 1,
                Repaired,
                Paid
            }
        }
    }
}
