namespace Garage {
   
    internal class ElectricEngine(float i_MaxCapacity) : Engine(i_MaxCapacity) 
    {
        public override sealed string ToString()
            {
                return string.Format(
                    @"Battery running time left : {0} 
                    Max battery running time : {1}",
                    CurrentCapacity,
                    MaxCapacity);
            }
    }
}