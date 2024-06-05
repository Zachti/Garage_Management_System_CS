namespace Garage {
    internal record CreateTruckInput(CreateCarInput i_CreateCarInput, bool i_IsCarryingDangerousMaterials, float i_CargoVolume);
    
    internal class Truck: Car {
        private bool IsCarryingDangerousMaterials { get; }
        private float CargoVolume { get; }

        public Truck(CreateTruckInput i_Dto)
            : base(i_Dto.i_CreateCarInput) {
            IsCarryingDangerousMaterials = i_Dto.i_IsCarryingDangerousMaterials;
            CargoVolume = i_Dto.i_CargoVolume;
        }
    }
}