namespace Garage {
    
    public class ValueOutOfRangeException : Exception
    {
        
        internal ValueOutOfRangeException(Exception i_InnerException, float i_MaxValue, float i_MinValue) :
            base($"Value was not in the range of {i_MinValue} - {i_MaxValue}", i_InnerException) {}
    }
}