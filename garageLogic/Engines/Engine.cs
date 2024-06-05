namespace Garage {
    
    internal abstract class Engine(float i_MaxCapacity) {
        protected float MaxCapacity { get; } = i_MaxCapacity;
        protected float CurrentCapacity { get; set; }

        protected float getMaxCapacityPossible() => MaxCapacity - CurrentCapacity;

        public virtual void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType) {
            EnsureEnergySupplyIsValid(i_AmountToAdd);
            CurrentCapacity += i_AmountToAdd;
        }

        public float getLeftEnergyPercentage() => CurrentCapacity / MaxCapacity * 100f;

        protected void EnsureEnergySupplyIsValid(float i_AmountToAdd) {
            if (isSupplyEnergyImpossible(i_AmountToAdd)) {
                Exception ex = new Exception("Cannot recharge more than the maximum possible capacity");
                throw new ValueOutOfRangeException(ex, 0, getMaxCapacityPossible());
            }
        }

        private bool isSupplyEnergyImpossible(float i_AmountToAdd) => i_AmountToAdd + CurrentCapacity > MaxCapacity;

        public abstract override string ToString();
    }
}