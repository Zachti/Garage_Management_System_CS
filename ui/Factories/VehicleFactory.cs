namespace Garage {

    internal record CommonVehicleData(List<Wheel> i_Wheels, string i_Model);

    internal abstract class VehicleFactory {
        protected abstract eWheelsNumber WheelsNumber { get; }
        protected abstract float MaxEnergy { get; }
        
        protected abstract Engine getEngineData();

        protected abstract List<Wheel> getWheelData(float[] i_WheelsPressure, string i_Manufacturer);

        private static Owner getOwnerDetails()
        {
            string customerName, customerPhoneNumber;

            Console.WriteLine("Please enter your name");
            customerName = Utilities.GetAlphabeticString();
            Console.WriteLine("Please enter your phone number, note that a valid phone number is 9 or 10 digits long");
            customerPhoneNumber = Utilities.GetNumberAsString(9, 10, "phone number must be 9 or 10 digits long");
            return new Owner(customerName, customerPhoneNumber);
        }
    
        public virtual void CreateGarageEntry(eSupportVehicles i_VehicleType, Garage i_Garage, string i_LicensePlate) {
            Engine engine = getEngineData();
            Vehicle vehicle = CreateVehicleStrategy.CreateVehicle(i_VehicleType, i_LicensePlate, engine);
            Owner owner = getOwnerDetails();
            UpdateVehicleInput updateVehicleInput = getUpdateVehicleInput();
            vehicle.UpdateVehicleData(updateVehicleInput);
            i_Garage.AddVehicle(new AddVehicleInput(vehicle, owner, i_LicensePlate));
        }
        
        protected CommonVehicleData getCommonVehicleData() {
            List<Wheel> wheels = getWheelData(getWheelsPressure(WheelsNumber), getManufacturer());
            string model = getModel();
            return new CommonVehicleData(wheels, model);
        }

        private float[] getWheelsPressure(eWheelsNumber i_WheelsNumber) {
            Console.WriteLine("Please enter the wheels pressure, separated by a comma or one pressure for all wheels: ");
            return Utilities.WheelsPressureToArray((int)i_WheelsNumber);
        }
    
        private string getManufacturer() {
            Console.WriteLine("Please enter the wheels manufacturer");
            return Utilities.GetAlphabeticString();
        }
    
        private string getModel() {
            Console.WriteLine("Please enter the vehicle model");
            return Utilities.GetAlphabeticString();
        }
    
        protected void getCurrentEngineEnergy(out float o_CurrentEnergy, float i_MaxEnergy) {
            Console.WriteLine("Please enter the current engine energy amount (fuel/battery):");
            o_CurrentEnergy = Utilities.GetNumber<float>();
            Utilities.ValidateNumberInRange(o_CurrentEnergy, 0, i_MaxEnergy, "Invalid current engine energy amount");
        }
    
        protected float getMaxEnergByType(float i_MaxFuel, float i_MaxBattery, eEngineType i_EngineType) =>
            i_EngineType switch {
                eEngineType.Fuel => i_MaxFuel,
                eEngineType.Electric => i_MaxBattery,
                _ => throw new ArgumentException("Invalid engine type", nameof(i_EngineType))
            };
        
        protected abstract UpdateVehicleInput getUpdateVehicleInput(); 
    }
}   