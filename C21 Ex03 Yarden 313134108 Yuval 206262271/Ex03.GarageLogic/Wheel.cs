namespace Ex03.GarageLogic
{
    public sealed class Wheel
    {
        private float m_MaxAirPressure = 0;
        private string m_ManufacturerName = string.Empty;
        private float m_CurrentAirPressure = 0;

        public Wheel()
        {
        }

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            this.m_ManufacturerName = i_ManufacturerName;
            this.m_MaxAirPressure = i_MaxAirPressure;
            this.m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public Wheel DeepClone(Wheel i_ToClone)
        {
            return new Wheel(i_ToClone.ManufacturerName, i_ToClone.m_CurrentAirPressure, i_ToClone.m_MaxAirPressure);
        }

        public float MaxAirPressure
        {
            get
            {
                return this.m_MaxAirPressure;
            }

            set
            {
                this.m_MaxAirPressure = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public void FillAirPressure(float i_AirToFill)
        {
            if(this.m_CurrentAirPressure + i_AirToFill > this.m_MaxAirPressure)
            {
                //todo :throw exeption
            }

            CurrentAirPressure += i_AirToFill;
        }

        public void FillAirPressureToMax()
        {
            this.m_CurrentAirPressure = this.m_MaxAirPressure;
        }

        public override string ToString()
        {
            return string.Format(
                @"Wheels Manufacturer - {0}
Wheels PSI - {1} out of {2}
",
                this.m_ManufacturerName,
                this.m_CurrentAirPressure,
                this.m_MaxAirPressure);
        }
    }
}
