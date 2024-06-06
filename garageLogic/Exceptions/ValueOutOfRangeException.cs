namespace Garage {
    
    public class ValueOutOfRangeException : Exception
    {
        
        internal ValueOutOfRangeException(Exception i_InnerException, float i_MinValue, float i_MaxValue) :
            base($"Value was not in the range of {i_MinValue} - {i_MaxValue}", i_InnerException) {}
    }
}