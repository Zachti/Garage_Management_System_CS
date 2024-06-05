namespace Garage {
        
        internal class TransformerFactory {
            public static VehicleInputTransformer CreateTransformer(eVehicleTransformerTypes i_VehicleTransformer) =>
                i_VehicleTransformer switch {
                    eVehicleTransformerTypes.Motorcycle => new MotorcycleInputTransformer(),
                    eVehicleTransformerTypes.Car => new CarInputTransformer(),
                    eVehicleTransformerTypes.Truck => new TruckInputTransformer(),
                    _ => throw new ArgumentException("Invalid vehicle type", nameof(i_VehicleTransformer))
                };
        }
}