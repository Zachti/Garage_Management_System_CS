namespace Garage {
        
        internal class FactoryStrategy {
            public static VehicleFactory CreateFactory(eSupportVehicles i_VehicleType) =>
                i_VehicleType switch {
                    eSupportVehicles.Motorcycle or eSupportVehicles.ElectricMotorcycle => new MotorCycleFactory(),
                    eSupportVehicles.Car or eSupportVehicles.ElectricCar => new CarFactory(),
                    eSupportVehicles.Truck => new TruckFactory(),
                _ => throw new ValueOutOfRangeException((float)i_VehicleType, (float)eSupportVehicles.Motorcycle, (float)eSupportVehicles.Truck)
                };
        }
}