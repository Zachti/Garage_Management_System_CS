namespace Garage {
        
    internal class Car(CreateVehicleInput i_CreateVehicleInput) : Vehicle(i_CreateVehicleInput) {
        private eCarColors Color { get; set; }
        private eCarNumberOfDoors NumberOfDoors { get; set; }

        public override string ToString() {
            return string.Format(
                @"{0}
Car's Color: {1}
Car's door quantity: {2}",
                base.ToString().TrimStart(),
                Color,
                NumberOfDoors);
        }

        public override void  UpdateVehicleData(UpdateVehicleInput i_UpdateVehicleInput) {
            base.UpdateVehicleData(i_UpdateVehicleInput);
            Color = (eCarColors)i_UpdateVehicleInput.i_Color!;
            NumberOfDoors = (eCarNumberOfDoors)i_UpdateVehicleInput.i_NumberOfDoors!;
        }
    }
}