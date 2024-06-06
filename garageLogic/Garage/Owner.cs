namespace Garage {
    
    internal struct Owner(string i_Name, string i_PhoneNumber) {
        public string Name { get; } = i_Name;
        public string PhoneNumber { get; } = i_PhoneNumber;

        public override readonly string ToString() => string.Format(
        @"Owner name: {0}
        Owner phone: {1}",
        Name,
        PhoneNumber);
    }
}