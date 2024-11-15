namespace Garage {
    
    internal class MotorCycleFactory: VehicleFactory {
        protected override sealed eWheelsNumber WheelsNumber => eWheelsNumber.Motorcycle;
        protected override float MaxEnergy => 5.5f;
        protected override sealed eWheelsMaxPressure WheelsMaxPressure => eWheelsMaxPressure.Motorcycle;


        protected override Engine getEngineData() => new FuelEngine(MaxEnergy, eFuelType.Octan98);

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