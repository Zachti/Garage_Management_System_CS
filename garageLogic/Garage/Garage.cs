namespace Garage {
    
    internal class Garage {
        private Dictionary<string, GarageEntry> GarageEntries {get;} = [];

        public bool TryToMoveVehicleToRepair(string i_LicensePlate) {
            bool isExist = isVehicleExist(i_LicensePlate);
            if ( isExist ) 
            {
                ChangeCarStatus(i_LicensePlate, eCarStatus.InRepair);
            }
            return isExist;
        }
    
        public void AddVehicle(AddVehicleInput i_AddVehicleInput) {
            BasicVehicleData basicVehicleData = i_AddVehicleInput.i_VehicleData.i_BasicVehicleData;
            VehicleData extendedVehicleData = i_AddVehicleInput.i_VehicleData;
            Vehicle vehicle = VehicleFactory.CreateVehicle(i_AddVehicleInput.i_SelectedVehicleType, i_AddVehicleInput.i_LicensePlate, basicVehicleData.i_Model, basicVehicleData.i_Wheels,basicVehicleData.i_Engine, extendedVehicleData.i_MotorLicenseType, extendedVehicleData.i_EngineVolume, extendedVehicleData.i_CarColor, extendedVehicleData.i_NumberOfDoors, extendedVehicleData.i_IsCarryingDangerousMaterials, extendedVehicleData.i_CargoVolume);
            GarageEntries.Add(i_AddVehicleInput.i_LicensePlate, new GarageEntry(new CreateGarageEntryInput(vehicle, basicVehicleData.i_Owner)));
        }
    
        public List<string> GetAllLicensePlatesRegistered(VehicleFilter? i_Filter) 
        {
            return GarageEntries
                .Where(entry => i_Filter == null || entry.Value.Status == i_Filter.i_CarStatus)
                .Select(entry => entry.Key)
                .ToList();
        }
    
        public void ChangeCarStatus(string i_LicensePlate, eCarStatus i_NewStatus) {
            if (!isVehicleExist(i_LicensePlate)) 
            {
                throwNotExistException(i_LicensePlate);
            }
            GarageEntry entry = GarageEntries[i_LicensePlate];
            entry.CheckEqualStatus(i_NewStatus);
            entry.Status = i_NewStatus;
        }

        public void InflateWheelsToMax(string i_LicensePlate) {
            if (!isVehicleExist(i_LicensePlate)) 
            {
                throwNotExistException(i_LicensePlate);
            }
            GarageEntries[i_LicensePlate].Vehicle.InflateWheelsToMax();
        }

        public void SupplyEnergy(string i_LicensePlate, float i_AmountToAdd, eFuelType? i_FuelType) {
            if (!isVehicleExist(i_LicensePlate)) 
            {
                throwNotExistException(i_LicensePlate);
            }
            Vehicle vehicle = GarageEntries[i_LicensePlate].Vehicle;
            validateEngineTypeAndOperationMatch(vehicle, i_FuelType);
            vehicle.SupplyEnergy(i_FuelType, i_AmountToAdd);
        }   
        
        public string GetVehicleInfoByLicensePlate(string i_LicensePlate) {
            if (!isVehicleExist(i_LicensePlate)) 
            {
                throwNotExistException(i_LicensePlate);
            }
            return GarageEntries[i_LicensePlate].ToString();
        }

        private bool isVehicleExist(string i_LicensePlate) => GarageEntries.ContainsKey(i_LicensePlate);

        private void throwNotExistException(string i_LicensePlate) => throw new ArgumentException($"Vehicle with Llicense Plate: {i_LicensePlate} , does not exist in the garage");
    
        private void validateEngineTypeAndOperationMatch(Vehicle i_Vehicle, eFuelType? i_FuelType) {
            if (!i_Vehicle.IsElectricVehicle() && i_FuelType == null) 
            {
                throw new ArgumentException("You can't recharge fuel engine!");
            }
            if (i_Vehicle.IsElectricVehicle() && i_FuelType != null) 
            {
                throw new ArgumentException("You can't fuel battery!");
            }

        }
    }
}