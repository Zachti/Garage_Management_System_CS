namespace Garage {
    internal record CreateCarInput(CreateVehicleInput i_CreateVehicleInput, eCarColors i_Color, int i_NumberOfDoors);
    internal class Car : Vehicle {
        private eCarColors Color { get; }   
        private int NumberOfDoors { get; }

        public Car(CreateCarInput i_Dto)
            : base(i_Dto.i_CreateVehicleInput) {
            Color = i_Dto.i_Color;
            NumberOfDoors = i_Dto.i_NumberOfDoors;
        }

    }
}