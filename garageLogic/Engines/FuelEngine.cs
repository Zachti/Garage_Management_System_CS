namespace Garage {
    
    internal class FuelEngine(float i_MaxCapacity, float i_CurrentCapacity, eFuelType i_FuelType) : Engine(i_MaxCapacity, i_CurrentCapacity) {
        private eFuelType FuelType { get;} = i_FuelType;

        public override sealed void SupplyEnergy(float i_AmountToAdd, eFuelType? i_FuelType) {
            if (isFuelTypeMismatch((eFuelType)i_FuelType!)) {
                throw new ArgumentException("Fuel type mismatch");
            }
            base.SupplyEnergy(i_AmountToAdd, i_FuelType);
        }

        private bool isFuelTypeMismatch(eFuelType i_FuelType) => i_FuelType != FuelType;
    
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