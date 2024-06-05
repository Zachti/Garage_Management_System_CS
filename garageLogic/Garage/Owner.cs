namespace Garage {
    internal struct Owner(string i_Name, string i_PhoneNumber) {
        public string Name { get; set; } = i_Name;
        public string PhoneNumber { get; set; } = i_PhoneNumber;
    }
}