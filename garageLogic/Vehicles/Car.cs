namespace Garage {
    
    internal record CreateCarInput(CreateVehicleInput i_CreateVehicleInput, eCarColors i_Color, int i_NumberOfDoors);
    
    internal class Car(CreateCarInput i_Dto) : Vehicle(i_Dto.i_CreateVehicleInput) {
        protected eCarColors Color { get; } = i_Dto.i_Color;
        protected int NumberOfDoors { get; } = i_Dto.i_NumberOfDoors;

        public override string ToString() {
            return string.Format(
                @"{0}
                Car's Color: {1}
                Car's door quantity: {2}
                ",
                VehicleDetails(),
                Color,
                NumberOfDoors);
        }
    }
}