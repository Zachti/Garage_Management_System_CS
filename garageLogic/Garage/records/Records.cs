namespace Garage {

    internal record VehicleFilter(eCarStatus i_CarStatus);

    internal record AddVehicleInput(Vehicle i_Vehicle, Owner i_Owner, string i_LicensePlate);
    
    internal record CommonVehicleData(List<Wheel> i_Wheels, Owner i_Owner, string i_Model);

    internal record VehicleData(CommonVehicleData i_BasicVehicleData, eCarColors? i_CarColor, eCarNumberOfDoors? i_NumberOfDoors, eMotorLicenseType? i_MotorLicenseType, int? i_EngineVolume, bool? i_IsCarryingDangerousMaterials, float? i_CargoVolume);

}