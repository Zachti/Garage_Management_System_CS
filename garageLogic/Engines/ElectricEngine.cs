namespace Garage {
   
    internal class ElectricEngine(float i_MaxCapacity) : Engine(i_MaxCapacity) 
    {
        public override void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType) {
            if (isRechargeImpossible(i_AmountToAdd)) {
                Exception ex = new Exception("Cannot recharge more than the maximum possible capacity");
                throw new ValueOutOfRangeException(ex, 0, getMaxCapacityPossible());
            }

            CurrentCapacity += i_AmountToAdd;
        }

        private bool isRechargeImpossible(float i_Hours) => i_Hours + GetCurrentCapacity() > GetMaxCapacity();

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