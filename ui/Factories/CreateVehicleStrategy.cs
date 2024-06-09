namespace Garage {
            
    internal class CreateVehicleStrategy {
        public static Vehicle CreateVehicle(eSupportVehicles i_VehicleType, string i_LicensePlate, Engine i_Engine) 
        {
            CreateVehicleInput createVehicleInput = new CreateVehicleInput(i_LicensePlate, i_Engine);
            return i_VehicleType switch 
            {
                eSupportVehicles.Motorcycle or eSupportVehicles.ElectricMotorcycle  => new MotorCycle(createVehicleInput),
                eSupportVehicles.Car or eSupportVehicles.ElectricCar => new Car(createVehicleInput),
                eSupportVehicles.Truck => new Truck(createVehicleInput),
                _ => throw new ValueOutOfRangeException((float)i_VehicleType, (float)eSupportVehicles.Motorcycle, (float)eSupportVehicles.Truck)
            };
        }
    }
}