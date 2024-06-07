namespace Garage {
    
    internal class MotorCycleFactory: VehicleFactory {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Motorcycle;
        protected override float MaxEnergy => 5.5f;

        protected override Engine getEngineData() => new FuelEngine(MaxEnergy, eFuelType.Octan98);

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