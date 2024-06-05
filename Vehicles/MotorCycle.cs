namespace Garage {
    internal record CreateMotorCycleInput(CreateVehicleInput i_CreateVehicleInput, eMotorLicenseType i_LicenseType, int i_EngineVolume);
    
    internal class MotorCycle: Vehicle {
        private eMotorLicenseType LicenseType { get; }
        private int EngineVolume { get; }

        public MotorCycle(CreateMotorCycleInput i_Dto)
            : base(i_Dto.i_CreateVehicleInput) {
            LicenseType = i_Dto.i_LicenseType;
            EngineVolume = i_Dto.i_EngineVolume;
        }
    }
}