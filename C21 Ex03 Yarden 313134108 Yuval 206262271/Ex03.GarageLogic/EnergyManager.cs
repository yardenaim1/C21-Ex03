namespace Ex03.GarageLogic
{
    public abstract class EnergyManager
    {
        protected readonly float r_MaxEnergyCapacity;
        protected float m_CurrentEnergy;

        protected EnergyManager(float i_MaxCapacity)
        {
            this.r_MaxEnergyCapacity = i_MaxCapacity;
        }

        protected float MaxEnergyCapacity
        {
            get
            {
                return this.r_MaxEnergyCapacity;
            }
        }

        protected float CurrentEnergy
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
            return this.m_CurrentEnergy / this.r_MaxEnergyCapacity * 100;
        }
    }
}

