namespace Garage {
    
    internal class VehicleFactory {
        public static Vehicle CreateVehicle(eSupportVehicles i_VehicleType ,string i_LicensePlate, string i_Model, List<Wheel> i_Wheels, eMotorLicenseType? i_LicenseType, int? i_EngineVolume, eCarColors? i_Color, eCarNumberOfDoors? i_NumberOfDoors, bool? i_IsCarryingDangerousMaterials, float? i_CargoVolume) {
            return i_VehicleType switch 
            {
                eSupportVehicles.Motorcycle => new MotorCycle(new CreateMotorCycleInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, new FuelEngine(5.5f, eFuelType.Octan98)), (eMotorLicenseType)i_LicenseType!, (int)i_EngineVolume!)),
                eSupportVehicles.ElectricMotorcycle => new MotorCycle(new CreateMotorCycleInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, new ElectricEngine(2.5f)), (eMotorLicenseType)i_LicenseType!, (int)i_EngineVolume!)),
                eSupportVehicles.Car => new Car(new CreateCarInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, new FuelEngine(45f, eFuelType.Octan95)), (eCarColors)i_Color!, (eCarNumberOfDoors)i_NumberOfDoors!)),
                eSupportVehicles.ElectricCar => new Car(new CreateCarInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, new ElectricEngine(3.5f)), (eCarColors)i_Color!, (eCarNumberOfDoors)i_NumberOfDoors!)),
                eSupportVehicles.Truck => new Truck(new CreateTruckInput(new CreateCarInput(new CreateVehicleInput(i_Model, i_LicensePlate, i_Wheels, new FuelEngine(120f,eFuelType.Solar)), (eCarColors)i_Color!, (eCarNumberOfDoors)i_NumberOfDoors!), (bool)i_IsCarryingDangerousMaterials!, (float)i_CargoVolume!)),
                _ => throw new NotSupportedException("Unsupported vehicle type")
            };
        }
    }
}

