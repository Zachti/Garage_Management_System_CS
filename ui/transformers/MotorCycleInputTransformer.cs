namespace Garage {
    
    internal class MotorcycleInputTransformer: VehicleInputTransformer {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Motorcycle;
        private const float k_MaxFuelAmount = 5.5f;
        private const float k_MaxBatteryTime = 2.5f;
        protected override sealed Engine getEngineData(eEngineType i_EngineType) {
            getCurrentEngineEnergy(out float currentEnergy);
            return i_EngineType switch {
                eEngineType.Fuel =>  new FuelEngine(k_MaxFuelAmount, currentEnergy, eFuelType.Octan98),
                eEngineType.Electric => new ElectricEngine(k_MaxBatteryTime, currentEnergy),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };
        }

        protected override sealed List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Motorcycle))
            ).ToList();
    
        public override sealed VehicleData Transform(eSupportVehicles i_VehicleType) {
            BasicVehicleData basicVehicleData = getBasicVehicleData(i_VehicleType);

            eMotorLicenseType motorLicenseType = getMotorLicenseType();
            int engineVolume = getEngineVolume();
            return new VehicleData(basicVehicleData, null, null, motorLicenseType, engineVolume, null, null);
        }

        private eMotorLicenseType getMotorLicenseType() =>
            Utilities.EnumMenuToEnumChoice<eMotorLicenseType>("Please enter the motorcycle's license type:");

        private int getEngineVolume() {
            Console.WriteLine("Please enter the motorcycle's engine volume:");
            return Utilities.GetNumber<int>();
        }
    }
}