namespace Garage {
    internal abstract class Engine(float i_MaxCapacity) {
        protected float MaxCapacity { get; } = i_MaxCapacity;
        protected float CurrentCapacity { get; set; }

        public float GetMaxCapacity() => MaxCapacity;

        public float GetCurrentCapacity() => CurrentCapacity;

        protected float getMaxCapacityPossible() => MaxCapacity - CurrentCapacity;

        public abstract void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType);

        public float getLeftEnergyPercentage() => CurrentCapacity / MaxCapacity * 100;
    }
}