namespace Garage {
        
        internal class TransformerFactory {
            public static VehicleInputTransformer CreateTransformer(eSupportVehicles i_VehicleType) =>
                i_VehicleType switch {
                    eSupportVehicles.Motorcycle or eSupportVehicles.ElectricMotorcycle => new MotorcycleInputTransformer(),
                    eSupportVehicles.Car or eSupportVehicles.ElectricCar => new CarInputTransformer(),
                    eSupportVehicles.Truck => new TruckInputTransformer(),
                _ => throw new ValueOutOfRangeException((float)i_VehicleType, (float)eSupportVehicles.Motorcycle, (float)eSupportVehicles.Truck)
                };
        }
}