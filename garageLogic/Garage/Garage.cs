namespace Garage {
    
    internal class Garage {
        private Dictionary<string, GarageEntry> GarageEntries {get; set; } = [];

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
            BasicVehicleData basicVehicleData = i_AddVehicleInput.i_VehicleData.i_BasicVehicleData;
            VehicleData extendedVehicleData = i_AddVehicleInput.i_VehicleData;
            Vehicle vehicle = VehicleFactory.CreateVehicle(i_AddVehicleInput.i_SelectedVehicleType, basicVehicleData.i_Model, i_AddVehicleInput.i_LicensePlate, basicVehicleData.i_Wheels, extendedVehicleData.i_MotorLicenseType, extendedVehicleData.i_EngineVolume, extendedVehicleData.i_CarColor, extendedVehicleData.i_NumberOfDoors, extendedVehicleData.i_IsCarryingDangerousMaterials, extendedVehicleData.i_CargoVolume);
            GarageEntries.Add(i_AddVehicleInput.i_LicensePlate, new GarageEntry(new CreateGarageEntryInput(vehicle, basicVehicleData.i_Owner)));
        }
    
        public List<string> GetAllLicensePlatesRegistered(VehicleFilter? i_Filter) 
        {
            return GarageEntries
                .Where(entry => i_Filter == null || entry.Value.Status == i_Filter.i_CarStatus)
                .Select(entry => entry.Key)
                .ToList();
        }
    
        public void ChangeCarStatus(string i_LicensePlate, eCarStatus i_NewStatus) => GarageEntries[i_LicensePlate].Status = i_NewStatus;

        public void InflateWheelsToMax(string i_LicensePlate) => GarageEntries[i_LicensePlate].Vehicle.InflateWheelsToMax();

        public void SupplyEnergy(string i_LicensePlate, float i_AmountToAdd, eFuelType? i_FuelType) => GarageEntries[i_LicensePlate].Vehicle.SupplyEnergy(i_FuelType, i_AmountToAdd);
   
        public string GetVehicleInfoByLicensePlate(string i_LicensePlate)
        {
            GarageEntry entry = GarageEntries[i_LicensePlate];
            return string.Format("{0}\nVehicle repairing state: {1}\n{2}", entry.Owner.ToString(), entry.Status, entry.Vehicle.ToString());
        } 
    }
}