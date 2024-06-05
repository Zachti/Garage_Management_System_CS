namespace Garage {
    
    internal record CreateGarageEntryInput (Vehicle i_Vehicle, Owner i_Owner);
    
    internal class GarageEntry (CreateGarageEntryInput i_Dto) {
        public Vehicle Vehicle { get; set; } = i_Dto.i_Vehicle;
        public Owner Owner { get; set; } = i_Dto.i_Owner;
        public eCarStatus Status { get; set; } = eCarStatus.InRepair;
    }
}