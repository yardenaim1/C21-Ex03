using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInfo> m_GarageVehicles;

        public Garage()
        {
            this.m_GarageVehicles = new Dictionary<string, VehicleInfo>();
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone, out bool o_isExists)
        {
            o_isExists = isExistsInGarage(i_Vehicle.LicenseNumber);

            if (!o_isExists)
            {
                this.m_GarageVehicles.Add(
                i_Vehicle.LicenseNumber, 
                new VehicleInfo(i_Vehicle,i_OwnerName,i_OwnerPhone));
            }
            else
            {
                this.m_GarageVehicles[i_Vehicle.LicenseNumber].StateInGarage = VehicleInfo.eStateInGarage.Repairing;
            }
        }

        private bool isExistsInGarage(string i_LicenseNumber)
        {
            return this.m_GarageVehicles.ContainsKey(i_LicenseNumber);
        }

        public class VehicleInfo
        {
            private Vehicle m_Vehicle;
            private string m_OwnerName;
            private string m_OwnePhoneNumber;
            private eStateInGarage m_CurrentStateInGarage;

            public VehicleInfo(Vehicle i_Vehicle, string i_OwnerName, string i_OwnePhoneNumber)
            {
                this.m_Vehicle = i_Vehicle;
                this.m_OwnerName = i_OwnerName;
                this.m_OwnePhoneNumber = i_OwnePhoneNumber;
                this.m_CurrentStateInGarage = eStateInGarage.Repairing;
            }

            public Vehicle GetVehicle
            {
                get
                {
                    return this.m_Vehicle;
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
                    return this.m_OwnePhoneNumber;
                }

                set
                {
                    this.m_OwnePhoneNumber = value;
                }
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
