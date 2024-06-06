namespace Garage
{
    internal class CarInputTransformer : VehicleInputTransformer
    {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Car;
        private const float k_MaxFuelAmount = 45f;
        private const float k_MaxBatteryTime = 3.5f;

        protected override Engine getEngineData(eEngineType i_EngineType) {
            float maxEnergy = getMaxEnergByType(k_MaxFuelAmount, k_MaxBatteryTime, i_EngineType);
            getCurrentEngineEnergy( out float currentEnergy, maxEnergy);
            return i_EngineType switch
            {
                eEngineType.Fuel => new FuelEngine(k_MaxFuelAmount, currentEnergy, eFuelType.Octan95),
                eEngineType.Electric => new ElectricEngine(k_MaxBatteryTime, currentEnergy),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };
        }

        protected override List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Car))
            ).ToList();

        public override VehicleData Transform(eSupportVehicles i_VehicleType) 
        {
            
            BasicVehicleData basicVehicleData = getBasicVehicleData(i_VehicleType);
            eCarColors carColor = getCarColor();
            eCarNumberOfDoors numberOfDoors = getNumberOfDoors();

            return new VehicleData(basicVehicleData, carColor, numberOfDoors, null, null, null, null);

        }

        private eCarColors getCarColor() => Utilities.EnumMenuToEnumChoice<eCarColors>("Please enter the car color from the options below:");
    
        private eCarNumberOfDoors getNumberOfDoors() =>
            Utilities.EnumMenuToEnumChoice<eCarNumberOfDoors>("Please enter the number of doors from the options below:");
    }
}
