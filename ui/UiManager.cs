using System.Text;

namespace Garage {

    internal class UIManager(Garage i_Garage) {
        private Garage Garage { get; } = i_Garage;

        public void Start() {
            printMainMenu();
            eMainMenuOptions userChoice = (eMainMenuOptions)GetSingleDigit();
            executeChoice(userChoice);

        }

        public static string GetAlphabeticString() {
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || !input.All(ch => char.IsLetter(ch) || ch == ' ')) {
                throw new FormatException("Input must contain only alphabetic characters and spaces.");
            }

            if (!input.Any(char.IsLetter)) {
                throw new FormatException("Input must contain at least one alphabetic character.");
            }

            return input;
        }

        public static string GetNumberAsString(int i_MinNumOfDigits, int i_MaxNumOfDigits) 
        {
            string strNumber = Console.ReadLine();

            bool isUserQuit = strNumber == "Q";

            int strLength = strNumber.Length;

            if (!isUserQuit &&(strLength < i_MinNumOfDigits || strLength > i_MaxNumOfDigits))
            {
                throw new ValueOutOfRangeException(
                    new Exception("Invalid input, please try again"),
                    i_MinNumOfDigits,
                    i_MaxNumOfDigits
                );
            }

            if (!strNumber.All(char.IsDigit) && !isUserQuit)
            {
                throw new FormatException("Input must contain only numeric digits.");
            }

            return strNumber;
        }
    
        public static float[] GetFloatArray(int i_WheelsNumber) {
            string input = Console.ReadLine() ?? string.Empty;
            string[] splitInput = input.Split(',');

            if (splitInput.Length != i_WheelsNumber)
            {
                throw new FormatException($"Input must contain state for {i_WheelsNumber} wheels.");
            }

            if (!splitInput.All(str => float.TryParse(str, out _)))
            {
                throw new FormatException("Input must contain only numeric digits.");
            }

            return splitInput.Select(float.Parse).ToArray();
        }
        
        public static T GetNumber<T>() where T : IConvertible
        {
            string strFloatNum = Console.ReadLine() ?? string.Empty;

            return (T)Convert.ChangeType(strFloatNum, typeof(T));

        }
        
        private static void printMainMenu() =>
           EnumToMenu<eMainMenuOptions>("Please choose which action to make by inserting a chioce number below: ");
    
        public static int GetSingleDigit() 
        {

            string input = Console.ReadLine();

            if (input?.Length != 1)
            {
                throw new FormatException("Input must be a single digit.");
            }

            if (!char.IsDigit(input[0]))
            {
                throw new FormatException("Input must be a single digit.");
            }

            return int.Parse(input);
        }
    
        private void executeChoice(eMainMenuOptions i_UserChoice) =>
            i_UserChoice switch
            {
                eMainMenuOptions.AddVehicle => handleAddVehicle(),
                eMainMenuOptions.PrintLicensePlatesOrderByFilter => handlePrintLicensePlatesOrderByFilter(),
                eMainMenuOptions.UpdateVechileState => handleUpdateVechileState(),
                eMainMenuOptions.InflateAllWheelsToMax => handleInflateAllWheelsToMax(),
                eMainMenuOptions.RefuelVehicle => handleRefuelVehicle(),
                eMainMenuOptions.ChargeVehicle => handleChargeVehicle(),
                eMainMenuOptions.DisplayFullVehicleDetails => handleDisplayFullVehicleDetails(),
                eMainMenuOptions.ReturnToMainMenu => Start(),
                eMainMenuOptions.Exit => handleGarageExit(),
                _ => throw new ValueOutOfRangeException(new Exception("Invalid input, please try again"), (float)eMainMenuOptions.AddVehicle, (float)eMainMenuOptions.Exit)
            };
    
        private void handleAddVehicle() {
            try {
                getLicensePlate(out string licensePlate);
                EnumToMenu<eSupportVehicles>("Please enter the Vehicle you want to add from the supported options:");
                VehicleInputTransformer inputTransformer = getVehicleInputTransformer(out eSupportVehicles vehicleType);
                AddVehicleInput addVehicleInput = getAddVehicleInput(inputTransformer, vehicleType);
                Garage.AddVehicle(addVehicleInput);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Start();
            }
        }

        public static void EnumToMenu<TEnum>(string? i_OpenMessage) where TEnum : Enum
        {
            StringBuilder menu = new StringBuilder(i_OpenMessage ?? "");
            int index = 1;
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                menu.AppendLine($"{index}. {value}");
                index++;
            }
            Console.WriteLine(menu.ToString());
        }
    
        private VehicleInputTransformer getVehicleInputTransformer(out eSupportVehicles o_VehicleType) {
             o_VehicleType = (eSupportVehicles)GetSingleDigit();

            return o_VehicleType switch
            {
                eSupportVehicles.Motorcycle or eSupportVehicles.ElectricMotorcycle => TransformerFactory.CreateTransformer(eVehicleTransformerTypes.Motorcycle),
                eSupportVehicles.Car or eSupportVehicles.ElectricCar => TransformerFactory.CreateTransformer(eVehicleTransformerTypes.Car),
                eSupportVehicles.Truck => TransformerFactory.CreateTransformer(eVehicleTransformerTypes.Truck),
                _ => throw new ValueOutOfRangeException(new Exception("Invalid input, please try again"), (float)eSupportVehicles.Motorcycle, (float)eSupportVehicles.Truck)
            };
        }
    
        private void getLicensePlate(out string o_LicensePlate) {
            Console.WriteLine("Please enter the vehicle's license plate number:");
            o_LicensePlate = GetNumberAsString(7, 8);
        }
    
        private AddVehicleInput getAddVehicleInput(VehicleInputTransformer i_Transformer, eSupportVehicles i_VehicleType) {
            VehicleData vehicleData = i_Transformer.Transform(i_VehicleType);
            getLicensePlate(out string licensePlate);
            return new AddVehicleInput(vehicleData, i_VehicleType, licensePlate);
        }
    }
}