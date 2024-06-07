namespace Garage {
        
    internal sealed class MotorCycle(CreateVehicleInput i_CreateVehicleInput) : Vehicle(i_CreateVehicleInput) {
        private eMotorLicenseType LicenseType { get; set; }
        private int EngineVolume { get; set; }

        public override sealed string ToString() {
            return string.Format(
            @"{0}
Motorcycle's license type: {1}
Motorcycle's engine cpacity: {2}",
            base.ToString().TrimStart(),
            LicenseType.ToString().TrimStart(),
            EngineVolume );
        }

        public override void UpdateVehicleData(float i_EngineEnergy, List<Wheel> i_Wheels, string i_Model, eCarColors? i_Color = null, eCarNumberOfDoors? i_NumberOfDoors = null, bool? i_IsCarryingDangerousMaterials = null, float? i_CargoVolume = null, eMotorLicenseType? i_LicenseType = null, int? i_EngineVolume = null) {
            base.UpdateVehicleData(i_EngineEnergy, i_Wheels, i_Model, null, null, null, null, null, null);
            LicenseType = (eMotorLicenseType)i_LicenseType!;
            EngineVolume = (int)i_EngineVolume!;
        }
    }
}