namespace Ex03.GarageLogic
{
    using System;

    public class ElectricEnergy : EnergyManager
    {
        public ElectricEnergy()
        {
        }

        public ElectricEnergy(float i_MaxBatteryHours ) :
            base(i_MaxBatteryHours)
        {
        }

        public float CurrentHoursLeft
        {
            get
            {
                return this.CurrentEnergy;
            }

            set
            {
                this.CurrentEnergy = value;
            }
        }

        public float MaxBatteryHours
        {
            get
            {
                return MaxEnergyCapacity;
            }
            set
            {
                this.m_MaxEnergyCapacity = value;
            }
        }

        public void Charge(float i_HoursToFill) 
        { 
            if (CurrentHoursLeft + i_HoursToFill > MaxBatteryHours || i_HoursToFill < 0)
            {
                throw new ValueOutOfRangeException(0, MaxBatteryHours - CurrentHoursLeft);
            }

            CurrentHoursLeft += i_HoursToFill;
        }

        public override string ToString()
        {
            return string.Format(
                @"Maximum battery time - {0} hours
Current charge - {1} hours ({2}%)
",
                MaxBatteryHours,
                CurrentHoursLeft,
                GetEnergyPercentage());
        }
    }
}
