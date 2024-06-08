namespace Garage {
        
        internal class FactoryStrategy {
            public static VehicleFactory CreateFactory(eSupportVehicles i_VehicleType) =>
                i_VehicleType switch {
                    eSupportVehicles.Motorcycle => new MotorCycleFactory(),
                    eSupportVehicles.ElectricMotorcycle => new ElectricMotorCycleFactory(),
                    eSupportVehicles.Car => new CarFactory(),
                    eSupportVehicles.ElectricCar => new ElectricCarFactory(),
                    eSupportVehicles.Truck => new TruckFactory(),
                    _ => throw new ValueOutOfRangeException((float)i_VehicleType, (float)eSupportVehicles.Motorcycle, (float)eSupportVehicles.Truck)
                };
        }
}