namespace Garage {
    
    internal record CreateTruckInput(CreateCarInput i_CreateCarInput, bool i_IsCarryingDangerousMaterials, float i_CargoVolume);
    
    internal class Truck(CreateTruckInput i_Dto) : Car(i_Dto.i_CreateCarInput) {
        private bool IsCarryingDangerousMaterials { get; } = i_Dto.i_IsCarryingDangerousMaterials;
        private float CargoVolume { get; } = i_Dto.i_CargoVolume;

        public override sealed string ToString() {
            return string.Format(
                @"{0}
                Truck's carrying dangerous materials: {1}
                Truck's cargo volume: {2}
                ",
                base.ToString().Replace("Car", "Truck").TrimStart(),
                IsCarryingDangerousMaterials,
                CargoVolume);
        }
    }
}