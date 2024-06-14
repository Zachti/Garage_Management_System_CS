namespace Garage {
    
    internal sealed class TruckFactory : CarFactory {
        protected override sealed eWheelsNumber WheelsNumber => eWheelsNumber.Truck;
        protected override sealed float MaxEnergy => 120f;
        protected override sealed eWheelsMaxPressure WheelsMaxPressure => eWheelsMaxPressure.Truck;


        protected override sealed Engine getEngineData() => new FuelEngine(MaxEnergy, eFuelType.Soler);

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
    
        protected override sealed UpdateVehicleInput getUpdateVehicleInput()
        {
            UpdateVehicleInput updateVehicleInput = base.getUpdateVehicleInput();
            bool isCarryingDangerousMaterials = IsTruckCarryingDangerousMaterials();
            float CargoVolume = getCargoVolume();
            return updateVehicleInput with {i_IsCarryingDangerousMaterials = isCarryingDangerousMaterials, i_CargoVolume = CargoVolume};
        }
    }
}