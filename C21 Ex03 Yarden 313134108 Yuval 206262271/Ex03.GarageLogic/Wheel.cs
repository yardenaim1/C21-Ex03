namespace Ex03.GarageLogic
{
    public sealed class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            this.m_ManufacturerName = i_ManufacturerName;
            this.m_CurrentAirPressure = i_CurrentAirPressure;
            this.r_MaxAirPressure = i_MaxAirPressure;
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

        public void FillAirPressureToMax()
        {
            this.m_CurrentAirPressure = this.r_MaxAirPressure;
        }

        public override string ToString()
        {
            return string.Format(
                @"Wheels Manufacturer - {0}
Wheels PSI - {1} out of {2}
",
                this.m_ManufacturerName,
                this.m_CurrentAirPressure,
                this.r_MaxAirPressure);
        }
    }
}
