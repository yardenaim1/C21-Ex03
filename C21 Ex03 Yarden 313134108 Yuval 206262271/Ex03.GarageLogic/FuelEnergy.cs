namespace Ex03.GarageLogic
{
   public sealed class FuelEnergy : EnergyManager
   {
       private eFuelType r_FuelType;

       public FuelEnergy()
       {
       }

       public FuelEnergy(eFuelType i_FuelType, float i_MaxFuel) :
           base(i_MaxFuel)
       {
           this.r_FuelType = i_FuelType;
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
               return this.r_FuelType;
           }
           set
           {
               this.r_FuelType = value;
           }
       }

       //public override void FillEnergy(float i_ToFill)
       //{
       //    AddFuel(i_ToFill);
       //}

       public void AddFuel(float i_ToFill, eFuelType i_FuelType)
       {
           if (i_FuelType != r_FuelType)
           {
               // todo:  throw new ArgumentException(string.Format(@"Mismatch in fuel type. Type needed is {0}", r_FuelType));
           }

           if (CurrentFuel + i_ToFill > MaxFuel || i_ToFill < 0)
           {
               // todo:  throw new ValueOutOfRangeException(0, MaxCapacity - CurrentCapacity, "Gas Engine");
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
