namespace Garage {
    
    internal record VehicleFilter(eCarStatus i_CarStatus);

    internal record AddVehicleInput(Owner i_Owner, eSupportVehicles i_SelectedVehicleType, string i_LicensePlate, string i_Model, List<Wheel> i_Wheels, eMotorLicenseType? i_LicenseType, int? i_EngineVolume, eCarColors? i_Color, int? i_NumberOfDoors, bool? i_IsCarryingDangerousMaterials, float? i_CargoVolume);
    
    internal class Garage {
        private Dictionary<string, GarageEntry> GarageEntries {get; set; }= new Dictionary<string, GarageEntry>();
        
        public void AddVehicle(AddVehicleInput i_AddVehicleInput) {
            if ( isVehicleExists(i_AddVehicleInput.i_LicensePlate) ) 
            {
                changeCarStatusToInRepair(i_AddVehicleInput.i_LicensePlate);
            }
            else 
            {
                addNewVehicle(i_AddVehicleInput);
            }
        }

        private bool isVehicleExists(string i_LicensePlate) => GarageEntries.ContainsKey(i_LicensePlate);
    
        private void changeCarStatusToInRepair(string i_LicensePlate) => GarageEntries[i_LicensePlate].Status = eCarStatus.InRepair;

        private void addNewVehicle(AddVehicleInput i_AddVehicleInput) {
            Vehicle vehicle = VehicleFactory.CreateVehicle(i_AddVehicleInput.i_SelectedVehicleType, i_AddVehicleInput.i_Model, i_AddVehicleInput.i_LicensePlate, i_AddVehicleInput.i_Wheels, i_AddVehicleInput.i_LicenseType, i_AddVehicleInput.i_EngineVolume, i_AddVehicleInput.i_Color, i_AddVehicleInput.i_NumberOfDoors, i_AddVehicleInput.i_IsCarryingDangerousMaterials, i_AddVehicleInput.i_CargoVolume);
            GarageEntries.Add(i_AddVehicleInput.i_LicensePlate, new GarageEntry(new CreateGarageEntryInput(vehicle, i_AddVehicleInput.i_Owner)));
        }
    
        public List<string> GetAllLicensePlatesRegistered(VehicleFilter? i_Filter) 
        {
            return GarageEntries
                .Where(entry => entry.Value.Status == i_Filter?.i_CarStatus)
                .Select(entry => entry.Key)
                .ToList();
        }
    
        public void ChangeCarStatus(string i_LicensePlate, eCarStatus i_NewStatus) => GarageEntries[i_LicensePlate].Status = i_NewStatus;

        public void InflateWheelsToMax(string i_LicensePlate) => GarageEntries[i_LicensePlate].Vehicle.InflateWheelsToMax();

        public void SupplyMaxEnergy(string i_LicensePlate, float i_AmountToAdd, eFuelType? i_FuelType) => GarageEntries[i_LicensePlate].Vehicle.SupplyMaxEnergy(i_FuelType, i_AmountToAdd);
   
        public Vehicle GetVehicleByLicensePlate(string i_LicensePlate) => GarageEntries[i_LicensePlate].Vehicle;
    }
}