namespace Garage
{
    internal record CreateVehicleInput(string i_Model, string i_LicensePlate, List<Wheel> i_Wheels, Engine i_Engine);

    internal abstract class Vehicle (CreateVehicleInput i_Dto)
    {
        private string Model { get;} = i_Dto.i_Model;
        private string LicensePlate { get;} = i_Dto.i_LicensePlate;
        private float i_LeftEnergyPercentage { get; set; }
        private List<Wheel> Wheels { get; set; } =  i_Dto.i_Wheels;
        private Engine Engine { get;} = i_Dto.i_Engine;
    }
    
}