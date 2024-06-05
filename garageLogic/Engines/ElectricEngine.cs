namespace Garage {
   
    internal class ElectricEngine(float i_MaxCapacity) : Engine(i_MaxCapacity) 
    {
        public override void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType) {
            EnsureEnergySupplyIsValid(i_AmountToAdd);
            CurrentCapacity += i_AmountToAdd;
        }

        public override string ToString()
            {
                return string.Format(
                    @"Battery running time left : {0} 
                    Max battery running time : {1}",
                    CurrentCapacity,
                    MaxCapacity);
            }
    }
}