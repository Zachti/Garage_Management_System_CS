using System.Text;

namespace Garage
{
    internal record CreateVehicleInput(string i_LicensePlate, Engine i_Engine);

    internal record UpdateVehicleInput(float i_EngineEnergy, List<Wheel> i_Wheels, string i_Model, eCarColors? i_Color = null, eCarNumberOfDoors? i_NumberOfDoors = null, bool? i_IsCarryingDangerousMaterials = null, float? i_CargoVolume = null, eMotorLicenseType? i_LicenseType = null, int? i_EngineVolume = null);

    internal abstract class Vehicle(CreateVehicleInput i_CreateVehicleInput) 
    {
        private string Model { get; set; } = string.Empty;
        private string LicensePlate { get; } = i_CreateVehicleInput.i_LicensePlate;
        private List<Wheel> Wheels { get; } = [];
        private Engine Engine { get; } = i_CreateVehicleInput.i_Engine;

        public void InflateWheelsToMax()
        {
            foreach (Wheel wheel in Wheels)
            {
                wheel.InflateToMax();
            }
        }
    
        public void SupplyEnergy(eFuelType? i_FuelType, float i_AmountToAdd) => Engine.SupplyEnergy(i_AmountToAdd, i_FuelType);

        public override string ToString() {
            StringBuilder wheelsInfo = new StringBuilder();
            foreach (int index in Enumerable.Range(0, Wheels.Count))
            {
                wheelsInfo.AppendLine($"Wheel No. {index + 1}: {Wheels[index].ToString()}").AppendLine();
            }
           return string.Format(
                @"Vehicel license plate: {0}
Vehicel model name: {1}
Wheels information: 
{2}
{3}",
                LicensePlate,
                Model,
                wheelsInfo.ToString(),
                Engine.ToString());
        }
    
        public bool IsElectricVehicle() => Engine is ElectricEngine;

        public virtual void UpdateVehicleData(UpdateVehicleInput i_UpdateVehicleInput) {
            Engine.CurrentCapacity = i_UpdateVehicleInput.i_EngineEnergy;
            Wheels.AddRange(i_UpdateVehicleInput.i_Wheels);
            Model = i_UpdateVehicleInput.i_Model;
        }
    }
}