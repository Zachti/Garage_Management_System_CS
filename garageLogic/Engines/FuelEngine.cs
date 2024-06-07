namespace Garage {
    
    internal class FuelEngine(float i_MaxCapacity, float i_CurrentCapacity, eFuelType i_FuelType) : Engine(i_MaxCapacity, i_CurrentCapacity) {
        private eFuelType FuelType { get; set; } = i_FuelType;

        public override sealed void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType) {
            if (isFuelTypeMismatch((eFuelType)i_FuelType!)) {
                throw new ArgumentException("Fuel type mismatch");
            }
            base.SupplyEnergy(i_AmountToAdd, i_FuelType);
            FuelType = (eFuelType)i_FuelType;
        }

        private bool isFuelTypeMismatch(eFuelType i_FuelType) {
            return (IsOctaneFuel(i_FuelType) && FuelType == eFuelType.Solar) ||
                (IsOctaneFuel(FuelType) && eFuelType.Solar == i_FuelType);
        } 

        private bool IsOctaneFuel(eFuelType i_FuelType)
        {
            List<eFuelType> fuelTypes = Enum.GetValues(typeof(eFuelType))
                                         .Cast<eFuelType>()
                                         .Where(ft => ft != eFuelType.Solar)
                                         .ToList();
            return fuelTypes.Contains(i_FuelType);
        }
    
        public override sealed string ToString() {
            return string.Format(
                @"Current amount of fuel : {0} 
Max amount of fuel : {1} 
Fuel type : {2}
Left energy percentage : {3} %",
                CurrentCapacity,
                MaxCapacity,
                FuelType,
                LeftEnergyPercentage);
        }
    }
}