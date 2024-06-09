namespace Garage {

    internal class CarFactory : VehicleFactory
    {
        protected override eWheelsNumber WheelsNumber => eWheelsNumber.Car;
        protected override float MaxEnergy => 45f;
       
        protected override Engine getEngineData() => new FuelEngine(MaxEnergy, eFuelType.Octan95);

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
