namespace Garage {
    internal class ElectricEngine : Engine {

        public ElectricEngine(float i_MaxCapacity)
            : base(i_MaxCapacity) {}

        public void Recharge(float i_Hours) {
            if (isRechargeImpossible(i_Hours)) {
                Exception ex = new Exception("Cannot recharge more than the maximum possible capacity");
                throw new OutOfRangeException(ex, 0, getMaxCapacityPossible());
            }

            CurrentCapacity += i_Hours;
        }

        private bool isRechargeImpossible(float i_Hours) => i_Hours + GetCurrentCapacity() > GetMaxCapacity();
    }
}