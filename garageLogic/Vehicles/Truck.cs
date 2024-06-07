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

        public override void UpdateVehicleData(float i_EngineEnergy, List<Wheel> i_Wheels, string i_Model, eCarColors? i_Color = null, eCarNumberOfDoors? i_NumberOfDoors = null, bool? i_IsCarryingDangerousMaterials = null, float? i_CargoVolume = null, eMotorLicenseType? i_LicenseType = null, int? i_EngineVolume = null) {
            base.UpdateVehicleData(i_EngineEnergy, i_Wheels, i_Model, (eCarColors)i_Color!, (eCarNumberOfDoors)i_NumberOfDoors!, null, null, null, null);
            IsCarryingDangerousMaterials = (bool)i_IsCarryingDangerousMaterials!;
            CargoVolume = (float)i_CargoVolume!;
        }
    }
}