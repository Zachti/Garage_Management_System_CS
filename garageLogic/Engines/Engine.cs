namespace Garage {
    internal abstract class Engine(float i_MaxCapacity) {
        protected float MaxCapacity { get; } = i_MaxCapacity;
        protected float CurrentCapacity { get; set; }

        public float GetMaxCapacity() => MaxCapacity;

        public float GetCurrentCapacity() => CurrentCapacity;

        protected float getMaxCapacityPossible() => MaxCapacity - CurrentCapacity;
    }
}