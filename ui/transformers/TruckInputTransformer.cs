namespace Garage {
    internal class TruckInputTransformer : CarInputTransformer {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Truck;

        protected override Engine getEngineData(eEngineType i_EngineType) =>
            i_EngineType switch {
                eEngineType.Fuel => new FuelEngine(120f, eFuelType.Solar),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };

        protected override List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Truck))
            ).ToList();
    
        public override VehicleData Transform(eSupportVehicles i_VehicleType) 
        {

            BasicVehicleData basicVehicleData = getBasicVehicleData(i_VehicleType);
            eCarColors carColor = getCarColor();
            eCarNumberOfDoors numberOfDoors = getNumberOfDoors();
            bool isCarryingDangerousMaterials = IsTruckCarryingDangerousMaterials();
            float cargoVolume = getCargoVolume();
            return new VehicleData(basicVehicleData, carColor, numberOfDoors, null, null, isCarryingDangerousMaterials, cargoVolume);

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
            return UIManager.GetNumber<float>();
    }
    }
}