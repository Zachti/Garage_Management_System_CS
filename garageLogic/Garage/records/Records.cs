namespace Garage {

    internal record VehicleFilter(eCarStatus i_CarStatus);

    internal record AddVehicleInput(Vehicle i_Vehicle, Owner i_Owner, string i_LicensePlate);
}