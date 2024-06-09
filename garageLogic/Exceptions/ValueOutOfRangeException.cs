namespace Garage {
    
    internal class ValueOutOfRangeException : Exception {
        
    internal ValueOutOfRangeException(float i_Value, float i_Min, float i_Max, string? additionalMessage = null)
        : base($"{additionalMessage ?? string.Empty}\nValue '{i_Value}' is out of range. Allowed range: [{i_Min}, {i_Max}].\n") {}
    }
}