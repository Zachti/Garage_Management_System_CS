namespace Garage
{
    internal class CarFactory : VehicleFactory
    {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Car;
        private const float k_MaxFuelAmount = 45f;
        private const float k_MaxBatteryTime = 3.5f;
        protected override float MaxEnergy {get; set; }
       
        protected override Engine getEngineData(eEngineType i_EngineType) {
            MaxEnergy = getMaxEnergByType(k_MaxFuelAmount, k_MaxBatteryTime, i_EngineType);
            return i_EngineType switch
            {
                eEngineType.Fuel => new FuelEngine(k_MaxFuelAmount, eFuelType.Octan95),
                eEngineType.Electric => new ElectricEngine(k_MaxBatteryTime),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };
        }

        protected override List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Car))
            ).ToList();

        public override void CreateGarageEntry(eSupportVehicles i_VehicleType, Garage i_Garage, string i_LicensePlate) 
        {
            Engine engine = getEngineData(getEngineType(i_VehicleType));
            Vehicle car = CreateVehicleStrategy.CreateVehicle(i_VehicleType, i_LicensePlate, engine);
            CommonVehicleData commonVehicleData = getCarData(out float currentEnergy, out eCarColors carColor, out eCarNumberOfDoors numberOfDoors);
            car.UpdateVehicleData(currentEnergy, commonVehicleData.i_Wheels, commonVehicleData.i_Model, carColor, numberOfDoors);
            i_Garage.AddVehicle(new AddVehicleInput(car, commonVehicleData.i_Owner, i_LicensePlate));
        }

        private eCarColors getCarColor() => Utilities.EnumMenuToEnumChoice<eCarColors>("Please enter the car color from the options below:");
    
        private eCarNumberOfDoors getNumberOfDoors() =>
            Utilities.EnumMenuToEnumChoice<eCarNumberOfDoors>("Please enter the number of doors from the options below:");
        
        protected CommonVehicleData getCarData(out float o_CurrentEnergy, out eCarColors o_CarColor, out eCarNumberOfDoors o_NumberOfDoors) 
        {
            getCurrentEngineEnergy(out o_CurrentEnergy , MaxEnergy);
            o_CarColor = getCarColor();
            o_NumberOfDoors = getNumberOfDoors();
            return getCommonVehicleData();
        }

    }
}
