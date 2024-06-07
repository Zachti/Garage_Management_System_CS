namespace Garage {
    internal sealed class TruckFactory : CarFactory {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Truck;
        protected override float MaxEnergy {get; set; } = 120f;

        protected override sealed Engine getEngineData(eEngineType i_EngineType) {
            return i_EngineType switch {
                eEngineType.Fuel => new FuelEngine(MaxEnergy, eFuelType.Solar),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };
        }

        protected override sealed List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Truck))
            ).ToList();

        private bool IsTruckCarryingDangerousMaterials()
        {
            Console.WriteLine("Is the truck carrying dangerous materials? (Y/N)");
            string input = Utilities.GetInputOrEmpty().Trim().ToLower();
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
    
        protected override UpdateVehicleInput getUpdateVehicleInput()
        {
            UpdateVehicleInput updateVehicleInput = base.getUpdateVehicleInput();
            bool isCarryingDangerousMaterials = IsTruckCarryingDangerousMaterials();
            float CargoVolume = getCargoVolume();
            return updateVehicleInput with {i_IsCarryingDangerousMaterials = isCarryingDangerousMaterials, i_CargoVolume = CargoVolume};
        }
    }
}