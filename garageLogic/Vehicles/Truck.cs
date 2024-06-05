namespace Garage {
    
    internal record CreateTruckInput(CreateCarInput i_CreateCarInput, bool i_IsCarryingDangerousMaterials, float i_CargoVolume);
    
    internal class Truck(CreateTruckInput i_Dto) : Car(i_Dto.i_CreateCarInput) {
        private bool IsCarryingDangerousMaterials { get; } = i_Dto.i_IsCarryingDangerousMaterials;
        private float CargoVolume { get; } = i_Dto.i_CargoVolume;

        public override string ToString() {
            return string.Format(
                @"{0}
                Truck's Color: {1}
                Truck's door quantity: {2}
                Truck's carrying dangerous materials: {3}
                Truck's cargo volume: {4}
                ",
                VehicleDetails(),
                Color,
                NumberOfDoors,
                IsCarryingDangerousMaterials,
                CargoVolume);
        }
    }
}