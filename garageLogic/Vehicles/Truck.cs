namespace Garage {
        
    internal sealed class Truck(CreateVehicleInput i_CreateCarInput) : Car(i_CreateCarInput) {
        private bool IsCarryingDangerousMaterials { get; set; }
        private float CargoVolume { get; set; }

        public override sealed string ToString() {
            return string.Format(
                @"{0}
Truck's carrying dangerous materials: {1}
Truck's cargo volume: {2}",
                base.ToString().Replace("Car", "Truck").TrimStart(),
                IsCarryingDangerousMaterials,
                CargoVolume);
        }

        public override void UpdateVehicleData(UpdateVehicleInput i_UpdateVehicleInput) {
            base.UpdateVehicleData(i_UpdateVehicleInput);
            IsCarryingDangerousMaterials = (bool)i_UpdateVehicleInput.i_IsCarryingDangerousMaterials!;
            CargoVolume = (float)i_UpdateVehicleInput.i_CargoVolume!;
        }
    }
}