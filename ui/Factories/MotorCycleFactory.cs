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
    
        public override sealed void CreateGarageEntry(eSupportVehicles i_VehicleType,Garage i_Garage, string i_LicensePlate) {
            Engine engine = getEngineData(getEngineType(i_VehicleType));
            Vehicle motorcycle = CreateVehicleStrategy.CreateVehicle(i_VehicleType, i_LicensePlate, engine);
            CommonVehicleData commonVehicleData = getMotorCycleData(out float currentEnergy, out eMotorLicenseType motorLicenseType, out int engineVolume);
            motorcycle.UpdateVehicleData(currentEnergy, commonVehicleData.i_Wheels, commonVehicleData.i_Model, null, null, null, null, motorLicenseType, engineVolume);

            i_Garage.AddVehicle(new AddVehicleInput(motorcycle, commonVehicleData.i_Owner, i_LicensePlate));
        }

        private eMotorLicenseType getMotorLicenseType() =>
            Utilities.EnumMenuToEnumChoice<eMotorLicenseType>("Please enter the motorcycle's license type:");

        private int getEngineVolume() {
            Console.WriteLine("Please enter the motorcycle's engine volume:");
            return Utilities.GetNumber<int>();
        }
    
        private CommonVehicleData getMotorCycleData(out float o_CurrentEnergy, out eMotorLicenseType o_motorLicenseType, out int o_EngineVolume) 
        {
            getCurrentEngineEnergy(out o_CurrentEnergy, MaxEnergy);
            o_motorLicenseType = getMotorLicenseType();
            o_EngineVolume = getEngineVolume();
            return getCommonVehicleData();
        }
    }
}