using System;

namespace Ex03.GarageLogic
{
    public abstract class EnergyManager
    {
        protected float m_MaxEnergyCapacity;
        protected float m_CurrentEnergy;

        protected EnergyManager()
        {
        }

        protected EnergyManager(float i_MaxCapacity)
        {
            this.m_MaxEnergyCapacity = i_MaxCapacity;
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return this.m_MaxEnergyCapacity;
            }

            set
            {
                this.m_MaxEnergyCapacity = value;
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return this.m_CurrentEnergy;
            }

            set
            {
                this.m_CurrentEnergy = value;
            }
        }

        public float GetEnergyPercentage()
        {
            return this.m_CurrentEnergy / this.m_MaxEnergyCapacity * 100;
        }
    }
}

