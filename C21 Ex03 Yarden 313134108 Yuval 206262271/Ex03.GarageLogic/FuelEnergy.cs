using System;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : EnergyManager
   {
       private eFuelType m_FuelType;

       public FuelEnergy()
       {
       }

       public FuelEnergy(eFuelType i_FuelType, float i_MaxFuel) :
           base(i_MaxFuel)
       {
           this.m_FuelType = i_FuelType;
       }

       public float CurrentFuel
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

       public float MaxFuel
       {
           get
           {
               return MaxEnergyCapacity;
           }

           set
           {
               MaxEnergyCapacity = value;
           }
       }

       public eFuelType FuelType
       {
           get
           {
               return this.m_FuelType;
           }

           set
           {
               this.m_FuelType = value;
           }
       }

       public void AddFuel(eFuelType i_FuelType, float i_ToFill )
       {
           if (i_FuelType != m_FuelType)
           {
               throw new ArgumentException(string.Format(@"Mismatch in fuel type. Type needed is {0}", m_FuelType));
           }

           if (CurrentFuel + i_ToFill > MaxFuel || i_ToFill < 0)
           {
               throw new ValueOutOfRangeException(0, MaxFuel - CurrentFuel);
           }

           CurrentFuel += i_ToFill;
       }

       public enum eFuelType 
       {
           Soler = 1,
           Octan95,
           Octan96,
           Octan98
        }

       public override string ToString()
       {
           return string.Format(
               @"Fuel tank capacity - {0} liters
Current fuel - {1} liters ({2}%)
Fuel type - {3}
",
               MaxFuel,
               CurrentFuel,
               GetEnergyPercentage(),
               FuelType);
        }
   }
}
