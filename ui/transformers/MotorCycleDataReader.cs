namespace Garage {
    
    internal class MotorcycleInputTransformer: VehicleInputTransformer {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Motorcycle;
        protected override Engine getEngineData(eEngineType i_EngineType) =>
            i_EngineType switch {
                eEngineType.Fuel =>  new FuelEngine(5.5f, eFuelType.Octan98),
                eEngineType.Electric => new ElectricEngine(2.5f),
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };

        protected override List<Wheel> getWheelData(float[] i_Wheels, string i_Manufacturer) =>
            i_Wheels.Select(wheelPressure =>
                new Wheel(new CreateWheelInput(i_Manufacturer, wheelPressure, (float)eWheelsMaxPressure.Motorcycle))
            ).ToList();
    
        public override VehicleData Transform(eSupportVehicles i_VehicleType) {
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