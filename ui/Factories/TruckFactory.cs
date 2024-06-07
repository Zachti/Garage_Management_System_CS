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
    
        public override sealed void CreateGarageEntry(eSupportVehicles i_VehicleType, Garage i_Garage, string i_LicensePlate) 
        {
            Engine engine = getEngineData(getEngineType(i_VehicleType));
            Vehicle Truck = CreateVehicleStrategy.CreateVehicle(i_VehicleType, i_LicensePlate, engine);
            CommonVehicleData commonVehicleData = getTruckData(out float currentEnergy, out eCarColors carColor, out eCarNumberOfDoors numberOfDoors, out bool isCarryingDangerousMaterials, out float cargoVolume);
            Truck.UpdateVehicleData(currentEnergy, commonVehicleData.i_Wheels, commonVehicleData.i_Model, carColor, numberOfDoors, isCarryingDangerousMaterials, cargoVolume);
            i_Garage.AddVehicle(new AddVehicleInput(Truck, commonVehicleData.i_Owner, i_LicensePlate));
        }

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
    
        private CommonVehicleData getTruckData(out float o_CurrentEnergy, out eCarColors o_CarColor, out eCarNumberOfDoors o_NumberOfDoors, out bool o_IsCarryingDangerousMaterials, out float o_CargoVolume) 
        {
            CommonVehicleData commonVehicleData = getCarData(out o_CurrentEnergy, out o_CarColor, out o_NumberOfDoors);
            o_IsCarryingDangerousMaterials = IsTruckCarryingDangerousMaterials();
            o_CargoVolume = getCargoVolume();
            return commonVehicleData;
        }
    }
}