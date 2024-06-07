namespace Garage {
    
    internal sealed class MotorCycleFactory: VehicleFactory {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Motorcycle;
        private const float k_MaxFuelAmount = 5.5f;
        private const float k_MaxBatteryTime = 2.5f;
        protected override float MaxEnergy {get; set; }

        protected override sealed Engine getEngineData(eEngineType i_EngineType) {
            MaxEnergy = getMaxEnergByType(k_MaxFuelAmount, k_MaxBatteryTime, i_EngineType);
            return i_EngineType switch {
                eEngineType.Fuel =>  new FuelEngine(k_MaxFuelAmount, eFuelType.Octan98),
                eEngineType.Electric => new ElectricEngine(k_MaxBatteryTime),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };
        }

        protected override sealed List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Motorcycle))
            ).ToList();

        private eMotorLicenseType getMotorLicenseType() =>
            Utilities.EnumMenuToEnumChoice<eMotorLicenseType>("Please enter the motorcycle's license type:");

        private int getEngineVolume() {
            Console.WriteLine("Please enter the motorcycle's engine volume:");
            return Utilities.GetNumber<int>();
        }
    
        protected override sealed UpdateVehicleInput getUpdateVehicleInput() 
        {
            CommonVehicleData commonVehicleData = getCommonVehicleData();
            getCurrentEngineEnergy(out float currentEnergy , MaxEnergy);
            eMotorLicenseType motorLicenseType = getMotorLicenseType();
            int engineVolume = getEngineVolume();
            return new UpdateVehicleInput(currentEnergy, commonVehicleData.i_Wheels, commonVehicleData.i_Model, null, null, null, null, motorLicenseType, engineVolume);
        }
    }
}