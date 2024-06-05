namespace Garage {

    internal record VehicleFilter(eCarStatus i_CarStatus);

    internal record AddVehicleInput(VehicleData i_VehicleData, eSupportVehicles i_SelectedVehicleType, string i_LicensePlate);
    
    internal record BasicVehicleData(Engine i_Engine, List<Wheel> i_Wheels, Owner i_Owner, string i_Model);

    internal record VehicleData(BasicVehicleData i_BasicVehicleData, eCarColors? i_CarColor, eCarNumberOfDoors? i_NumberOfDoors, eMotorLicenseType? i_MotorLicenseType, int? i_EngineVolume, bool? i_IsCarryingDangerousMaterials, float? i_CargoVolume);

}