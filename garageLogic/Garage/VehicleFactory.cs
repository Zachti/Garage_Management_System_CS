namespace Garage {
    
    internal class VehicleFactory {
        public static Vehicle CreateVehicle(eSupportVehicles i_VehicleType ,string i_LicensePlate, string i_Model, List<Wheel> i_Wheels, Engine i_Engine, eMotorLicenseType? i_LicenseType, int? i_EngineVolume, eCarColors? i_Color, eCarNumberOfDoors? i_NumberOfDoors, bool? i_IsCarryingDangerousMaterials, float? i_CargoVolume) {
            return i_VehicleType switch 
            {
                eSupportVehicles.Motorcycle => new MotorCycle(new CreateMotorCycleInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, i_Engine), (eMotorLicenseType)i_LicenseType!, (int)i_EngineVolume!)),
                eSupportVehicles.ElectricMotorcycle => new MotorCycle(new CreateMotorCycleInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, i_Engine), (eMotorLicenseType)i_LicenseType!, (int)i_EngineVolume!)),
                eSupportVehicles.Car => new Car(new CreateCarInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, i_Engine), (eCarColors)i_Color!, (eCarNumberOfDoors)i_NumberOfDoors!)),
                eSupportVehicles.ElectricCar => new Car(new CreateCarInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, i_Engine), (eCarColors)i_Color!, (eCarNumberOfDoors)i_NumberOfDoors!)),
                eSupportVehicles.Truck => new Truck(new CreateTruckInput(new CreateCarInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, i_Engine), (eCarColors)i_Color!, (eCarNumberOfDoors)i_NumberOfDoors!), (bool)i_IsCarryingDangerousMaterials!, (float)i_CargoVolume!)),
                _ => throw new ValueOutOfRangeException((float)i_VehicleType, (float)eSupportVehicles.Motorcycle, (float)eSupportVehicles.Truck, "Unsupported vehicle type")
            };
        }
    }
}

