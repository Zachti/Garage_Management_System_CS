namespace Garage {
    
    internal class FuelEngine(float i_MaxCapacity, eFuelType i_FuelType) : Engine(i_MaxCapacity) {
        public eFuelType FuelType { get; } = i_FuelType;

        public override void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType) {
            EnsureEnergySupplyIsValid(i_AmountToAdd);
            if (isFuelTypeMismatch((eFuelType)i_FuelType!)) {
                throw new ArgumentException("Fuel type mismatch");
            }
            CurrentCapacity += i_AmountToAdd;
        }

        private bool isFuelTypeMismatch(eFuelType i_FuelType) {
            return (IsOctaneFuel(i_FuelType) && FuelType == eFuelType.Solar) ||
                (IsOctaneFuel(FuelType) && eFuelType.Solar == i_FuelType);
        } 

        private bool IsOctaneFuel(eFuelType i_FuelType)
        {
            return i_FuelType == eFuelType.Octan95 ||
                i_FuelType == eFuelType.Octan96 ||
                i_FuelType == eFuelType.Octan98;
        }
    
        public override string ToString() {
            return string.Format(
                @"Current amount of fuel : {0} 
                Max amount of fuel : {1} 
                Fuel type : {2}",
                CurrentCapacity,
                MaxCapacity,
                FuelType);
        }
    }
}