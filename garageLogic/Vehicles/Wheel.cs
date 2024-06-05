namespace Garage
{
    internal class Wheel(string i_Manufacturer, float i_Pressure, float i_MaxPressure)
    {
        private string Manufacturer { get; set; } = i_Manufacturer;
        private float  Pressure { get; set; } = i_Pressure;
        private float MaxPressure { get; set; } = i_MaxPressure;

        public void TireInflation(float i_PressureToAdd)
        {
            if (isInflateImpossible(i_PressureToAdd))
            {    
                Exception ex = new Exception("Cannot inflate tire, pressure will exceed maximum pressure");
                throw new ValueOutOfRangeException(ex, 0, MaxPressure);
            }
            Pressure += i_PressureToAdd;
        }
    
        private bool isInflateImpossible(float i_PressureToAdd) => Pressure + i_PressureToAdd > MaxPressure;

        public void InflateToMax() => Pressure = MaxPressure;
    }
}