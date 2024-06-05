using System.Dynamic;

namespace Garage {
    
    internal abstract class VehicleInputTransformer {
        protected abstract eWheelsNumber WheelsNumber { get; }
        
        protected abstract Engine getEngineData(eEngineType i_EngineType);

        protected abstract List<Wheel> getWheelData(float[] i_WheelsPressure, string i_Manufacturer);

        private static Owner getOwnerDetails()
        {
            string customerName, customerPhoneNumber;

            Console.WriteLine("Please enter your name");
            customerName = UIManager.GetAlphabeticString();
            Console.WriteLine("Please enter your phone number, note that a valid phone number is 9 or 10 digits long");
            customerPhoneNumber = UIManager.GetNumberAsString(9, 10);
            return new Owner(customerName, customerPhoneNumber);
        }
    
        public abstract VehicleData Transform(eSupportVehicles i_VehicleType);
        
        protected BasicVehicleData getBasicVehicleData(eSupportVehicles i_VehicleType) {
            Engine engine = getEngineData(getEngineType(i_VehicleType));
            List<Wheel> wheels = getWheelData(getWheelsPressure(WheelsNumber), getManufacturer());
            Owner owner = getOwnerDetails();
            string model = getModel();
            return new BasicVehicleData(engine, wheels, owner, model);
        }

        private eEngineType getEngineType(eSupportVehicles i_VehicleType) {
            return i_VehicleType switch {
                eSupportVehicles.ElectricCar or eSupportVehicles.ElectricMotorcycle => eEngineType.Fuel,
                eSupportVehicles.Car or eSupportVehicles.Motorcycle or eSupportVehicles.Truck => eEngineType.Fuel,
                _ => throw new ArgumentException("Invalid vehicle type", nameof(i_VehicleType))
            };
        }

        private float[] getWheelsPressure(eWheelsNumber i_WheelsNumber) {
            Console.WriteLine("Please enter the wheels pressure, separated by a comma");
            return UIManager.GetFloatArray((int)i_WheelsNumber);
        }
    
        private string getManufacturer() {
            Console.WriteLine("Please enter the wheels manufacturer");
            return UIManager.GetAlphabeticString();
        }
    
        private string getModel() {
            Console.WriteLine("Please enter the vehicle model");
            return UIManager.GetAlphabeticString();
        }
    }
}