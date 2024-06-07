namespace Garage {
        
    internal class Car(CreateVehicleInput i_CreateVehicleInput) : Vehicle(i_CreateVehicleInput) {
        private eCarColors Color { get; set; }
        private eCarNumberOfDoors NumberOfDoors { get; set; }

        public override string ToString() {
            return string.Format(
                @"{0}
Car's Color: {1}
Car's door quantity: {2}",
                base.ToString().TrimStart(),
                Color,
                NumberOfDoors);
        }

        public override void UpdateVehicleData(float i_EngineEnergy, List<Wheel> i_Wheels, string i_Model, eCarColors? i_Color = null, eCarNumberOfDoors? i_NumberOfDoors = null, bool? i_IsCarryingDangerousMaterials = null, float? i_CargoVolume = null, eMotorLicenseType? i_LicenseType = null, int? i_EngineVolume = null) {
            base.UpdateVehicleData(i_EngineEnergy, i_Wheels, i_Model, null, null, null, null, null, null);
            Color = (eCarColors)i_Color!;
            NumberOfDoors = (eCarNumberOfDoors)i_NumberOfDoors!;
        }
    }
}