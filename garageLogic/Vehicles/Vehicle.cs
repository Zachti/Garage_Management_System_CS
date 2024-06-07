using System.Text;

namespace Garage
{
    internal record CreateVehicleInput(string i_LicensePlate, Engine i_Engine);

    internal abstract class Vehicle(CreateVehicleInput i_Dto) 
    {
        private string Model { get; set; } = string.Empty;
        private string LicensePlate { get; } = i_Dto.i_LicensePlate;
        private List<Wheel> Wheels { get; set; } = [];
        private Engine Engine { get; } = i_Dto.i_Engine;

        public void InflateWheelsToMax()
        {
            foreach (Wheel wheel in Wheels)
            {
                wheel.InflateToMax();
            }
        }
    
        public void SupplyEnergy(eFuelType? i_FuelType, float i_AmountToAdd) {
            Engine.SupplyEnergy(i_AmountToAdd, i_FuelType);
        }

        public override string ToString() {
            StringBuilder wheelsInfo = new StringBuilder();
            foreach (int index in Enumerable.Range(0, Wheels.Count))
            {
                wheelsInfo.AppendLine($"Wheel No. {index + 1}: {Wheels[index].ToString()}");
            }
           return string.Format(
                @"Vehicel license plate: {0}
Vehicel model name: {1}
Wheels information: 
{2}
{3}",
                LicensePlate,
                Model,
                wheelsInfo.ToString().TrimStart(),
                Engine.ToString().TrimStart());
        }
    
        public bool IsElectricVehicle() => Engine is ElectricEngine;

        public virtual void UpdateVehicleData(float i_EngineEnergy, List<Wheel> i_Wheels, string i_Model, eCarColors? i_Color = null, eCarNumberOfDoors? i_NumberOfDoors = null, bool? i_IsCarryingDangerousMaterials = null, float? i_CargoVolume = null, eMotorLicenseType? i_LicenseType = null, int? i_EngineVolume = null) {
            Engine.CurrentCapacity = i_EngineEnergy;
            Wheels = i_Wheels;
            Model = i_Model;
        }
    }
}