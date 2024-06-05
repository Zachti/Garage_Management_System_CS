using System.Text;

namespace Garage {

    internal class UIManager(Garage i_Garage) {
        private Garage Garage { get; } = i_Garage;

        public void Start() {
            printMainMenu();
            eMainMenuOptions userChoice = (eMainMenuOptions)Utilities.GetSingleDigit();
            executeChoice(userChoice);

        }

        private static void printMainMenu() =>
           Utilities.EnumToMenu<eMainMenuOptions>("Please choose which action to make by inserting a chioce number below: ");
    
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
                Utilities.EnumToMenu<eSupportVehicles>("Please enter the Vehicle you want to add from the supported options:");
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
    
        private VehicleInputTransformer getVehicleInputTransformer(out eSupportVehicles o_VehicleType) {
             o_VehicleType = (eSupportVehicles)Utilities.GetSingleDigit();

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
            o_LicensePlate = Utilities.GetNumberAsString(7, 8);
        }
    
        private AddVehicleInput getAddVehicleInput(VehicleInputTransformer i_Transformer, eSupportVehicles i_VehicleType) {
            VehicleData vehicleData = i_Transformer.Transform(i_VehicleType);
            getLicensePlate(out string licensePlate);
            return new AddVehicleInput(vehicleData, i_VehicleType, licensePlate);
        }
    
        private void handlePrintLicensePlatesOrderByFilter() {
            try {

                VehicleFilter? filter = getVehicleFilter();
                List<string> vehicles = Garage.GetAllLicensePlatesRegistered(filter);
                StringBuilder output = new StringBuilder();
                foreach (string licensePlate in vehicles)
                {
                    output.AppendLine(licensePlate);
                }

                 Console.WriteLine(output.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Start();
            }
        }

        private VehicleFilter? getVehicleFilter() {
            int choice = EnumMenuToIntChoiceWithValidation<eCarStatus>("Please enter the vehicle status you want to filter by or 0 if you don't want any filter:", (int)eCarStatus.InRepair - 1, (int)eCarStatus.Paid);
            return choice == 0 ? null : new VehicleFilter((eCarStatus)choice);
        }

        private void validateNumberInRange(int i_Number, int i_Min, int i_Max) {
            if (i_Number < i_Min || i_Number > i_Max) {
                throw new ValueOutOfRangeException(new Exception("Invalid input, please try again"), i_Min, i_Max);
            }
        }
   
        private void handleUpdateVechileState() {
            try {
                getLicensePlate(out string licensePlate);
                int choice = EnumMenuToIntChoiceWithValidation<eCarStatus>("Please enter the new vehicle status:", (int)eCarStatus.InRepair ,(int)eCarStatus.Paid);
                Garage.ChangeCarStatus(licensePlate, (eCarStatus)choice);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Start();
            }
        }
    
        private int EnumMenuToIntChoiceWithValidation<T>(string i_Message, int i_Min, int i_Max) where T : Enum {
            Utilities.EnumMenuToEnumChoice<T>(i_Message);
            int choice = Utilities.GetSingleDigit();
            validateNumberInRange(choice, i_Min, i_Max);
            return choice;
        }
    
        private void handleInflateAllWheelsToMax() {
            try {
                getLicensePlate(out string licensePlate);
                Garage.InflateWheelsToMax(licensePlate);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Start();
            }
        }
    }
}