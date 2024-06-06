namespace Garage {
    
    internal abstract class Engine(float i_MaxCapacity) {
        protected float MaxCapacity { get; } = i_MaxCapacity;
        protected float CurrentCapacity { get; set; }

        private float getMaxCapacityPossible() => MaxCapacity - CurrentCapacity;

        public virtual void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType) {
            EnsureEnergySupplyIsValid(i_AmountToAdd);
            CurrentCapacity += i_AmountToAdd;
        }

        public float GetLeftEnergyPercentage() => CurrentCapacity / MaxCapacity * 100f;

        protected void EnsureEnergySupplyIsValid(float i_AmountToAdd) {
            if (isSupplyEnergyImpossible(i_AmountToAdd)) {
                string errorMessage = String.Format("Cannot supply energy more than the maximum possible capacity");
                throw new ValueOutOfRangeException(i_AmountToAdd, 0, getMaxCapacityPossible(), errorMessage);
            }
        }

        private bool isSupplyEnergyImpossible(float i_AmountToAdd) => i_AmountToAdd > getMaxCapacityPossible();

        public abstract override string ToString();
    }
}