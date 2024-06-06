namespace Garage {
    
    internal record CreateGarageEntryInput (Vehicle i_Vehicle, Owner i_Owner);
    
    internal class GarageEntry (CreateGarageEntryInput i_Dto) {
        public Vehicle Vehicle { get; } = i_Dto.i_Vehicle;
        public Owner Owner { get; } = i_Dto.i_Owner;
        public eCarStatus Status { get; set; } = eCarStatus.InRepair;

        public void CheckEqualStatus(eCarStatus i_NewStatus)
        {
            if (Status == i_NewStatus)
            {
                throw new ArgumentException(
                    string.Format(
                    "The vehicle is already in '{0}' status",
                    Status));
            }
        }
    
        public override sealed string ToString()
        {
            return string.Format(
            @"{0}
            Vehicle status: {1}
            {2}
            ",
            Owner.ToString().TrimStart(),
            Status,
            Vehicle.ToString().TrimStart()
            );
        }
    }
}