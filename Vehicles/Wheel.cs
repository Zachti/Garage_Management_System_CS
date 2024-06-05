namespace Garage
{
    internal class Wheel
    {
        private string Manufacturer { get; set; } = string.Empty;
        private float  Pressure { get; set; } = 0;
        private float MaxPressure { get; set; } = 0;

        public void TireInflation(float i_PressureToAdd)
        {
            if (isInflateImpossible(i_PressureToAdd))
            {    
                Exception ex = new Exception("Cannot inflate tire, pressure will exceed maximum pressure");
                throw new OutOfRangeException(ex, 0, MaxPressure);
            }
            Pressure += i_PressureToAdd;
        }
    
        private bool isInflateImpossible(float i_PressureToAdd) => Pressure + i_PressureToAdd > MaxPressure;
    }
}