namespace Garage {

    internal class CarFactory : VehicleFactory
    {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Car;
        protected override float MaxEnergy => 45f;
        protected override eWheelsMaxPressure WheelsMaxPressure => eWheelsMaxPressure.Car;

       
        protected override Engine getEngineData() => new FuelEngine(MaxEnergy, eFuelType.Octan95);

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
