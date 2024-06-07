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

        private eCarColors getCarColor() => Utilities.EnumMenuToEnumChoice<eCarColors>("Please enter the car color from the options below:");
    
        private eCarNumberOfDoors getNumberOfDoors() =>
            Utilities.EnumMenuToEnumChoice<eCarNumberOfDoors>("Please enter the number of doors from the options below:");
        
        protected override UpdateVehicleInput getUpdateVehicleInput() 
        {
            CommonVehicleData commonVehicleData = getCommonVehicleData();
            getCurrentEngineEnergy(out float currentEnergy , MaxEnergy);
            eCarColors carColor = getCarColor();
            eCarNumberOfDoors numberOfDoors = getNumberOfDoors();
            return new UpdateVehicleInput(currentEnergy, commonVehicleData.i_Wheels, commonVehicleData.i_Model, carColor, numberOfDoors);
        }
    }
}
