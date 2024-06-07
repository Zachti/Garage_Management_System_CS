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

        public override void UpdateVehicleData(UpdateVehicleInput i_UpdateVehicleInput) {
            base.UpdateVehicleData(i_UpdateVehicleInput);
            LicenseType = (eMotorLicenseType)i_UpdateVehicleInput.i_LicenseType!;
            EngineVolume = (int)i_UpdateVehicleInput.i_EngineVolume!;
        }
    }
}