namespace Garage {
    internal class TruckInputTransformer : CarInputTransformer {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Truck;
        private const float k_MaxFuelAmount = 120f;

        protected override sealed Engine getEngineData(eEngineType i_EngineType) =>
            i_EngineType switch {
                eEngineType.Fuel => new FuelEngine(k_MaxFuelAmount, eFuelType.Solar),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };

        protected override sealed List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Truck))
            ).ToList();
    
        public override sealed VehicleData Transform(eSupportVehicles i_VehicleType) 
        {
            VehicleData vehicleData = base.Transform(i_VehicleType);
            bool isCarryingDangerousMaterials = IsTruckCarryingDangerousMaterials();
            float cargoVolume = getCargoVolume();

            return vehicleData with { i_IsCarryingDangerousMaterials = isCarryingDangerousMaterials, i_CargoVolume = cargoVolume };
        }

        private bool IsTruckCarryingDangerousMaterials()
        {
            Console.WriteLine("Is the truck carrying dangerous materials? (Y/N)");
            string input = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
            if (input != "y" && input != "n")
            {
                throw new ArgumentException("Invalid input, please enter Y or N");
            }
            return input == "y";
        }
    
        private float getCargoVolume()
        {
            Console.WriteLine("Please enter the truck's cargo volume:");
            return Utilities.GetNumber<float>();
    }
    }
}