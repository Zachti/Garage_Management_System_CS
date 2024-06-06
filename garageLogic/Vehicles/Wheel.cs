namespace Garage {

    internal record CreateWheelInput(string i_Manufacturer, float i_Pressure, float i_MaxPressure);
    
    internal class Wheel(CreateWheelInput i_Dto)
    {
        private string Manufacturer { get;} = i_Dto.i_Manufacturer;
        private float Pressure { get; set; } = i_Dto.i_Pressure;
        private float MaxPressure { get;} = i_Dto.i_MaxPressure;

        public void TireInflation(float i_PressureToAdd)
        {
            if (isInflateImpossible(i_PressureToAdd))
            {    
                Exception ex = new Exception("Cannot inflate tire, pressure will exceed maximum pressure");
                throw new ValueOutOfRangeException(ex, 0, MaxPressure - Pressure);
            }
            Pressure += i_PressureToAdd;
        }
    
        private bool isInflateImpossible(float i_PressureToAdd) => Pressure + i_PressureToAdd > MaxPressure;

        public void InflateToMax() => Pressure = MaxPressure;

        public override sealed string ToString()
        {
            return string.Format(
                @"Wheel manufacturer: {0}
                Air pressure: {1}
                ",
                Manufacturer,
                Pressure);
        }
    }
}