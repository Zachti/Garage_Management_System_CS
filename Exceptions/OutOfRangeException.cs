namespace Garage {
    public class OutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        internal OutOfRangeException(Exception i_InnerException, float i_MaxValue, float i_MinValue) :
            base(($"Value was not in the range of {i_MinValue} - {i_MaxValue}"), i_InnerException)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}