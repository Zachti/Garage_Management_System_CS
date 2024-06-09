namespace Garage
{
    
    internal record CreateVehicleInput(string i_LicensePlate, Engine i_Engine);

    internal record UpdateVehicleInput(float i_EngineEnergy, List<Wheel> i_Wheels, string i_Model, eCarColors? i_Color = null, eCarNumberOfDoors? i_NumberOfDoors = null, bool? i_IsCarryingDangerousMaterials = null, float? i_CargoVolume = null, eMotorLicenseType? i_LicenseType = null, int? i_EngineVolume = null);
}