using System.Text;

namespace Garage {

    internal class UIManager(Garage i_Garage) {
        private Garage Garage { get; } = i_Garage;
        private bool IsUserWantToExit { get; set; }

        public void Start() {
            printWelcomeMessage();
            while (!IsUserWantToExit) {
                try {
                    eMainMenuOptions userChoice = displayMainMenuAndGetChoice();
                    executeChoice(userChoice);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static eMainMenuOptions displayMainMenuAndGetChoice() =>
           Utilities.EnumMenuToEnumChoice<eMainMenuOptions>("Please choose which action to make by inserting a chioce number below: ");
    
        private void executeChoice(eMainMenuOptions i_UserChoice)
        {
            switch (i_UserChoice)
            {
                case eMainMenuOptions.Add_vehicle:
                    handleAddVehicle();
                    break;
                case eMainMenuOptions.Show_all_license_plates_in_the_system_filtered_by_status:
                    handlePrintLicensePlatesOrderByFilter();
                    break;
                case eMainMenuOptions.Update_vechile_status:
                    handleUpdateVechileState();
                    break;
                case eMainMenuOptions.Inflate_all_wheels_to_max:
                    handleInflateAllWheelsToMax();
                    break;
                case eMainMenuOptions.Refuel_vehicle:
                    handleRefuelVehicle();
                    break;
                case eMainMenuOptions.Charge_vehicle:
                    handleChargeVehicle();
                    break;
                case eMainMenuOptions.Display_full_vehicle_details:
                    handleDisplayFullVehicleDetails();
                    break;
                case eMainMenuOptions.Exit:
                    handleGarageExit();
                    break;
                default:
                    throw new ValueOutOfRangeException((float)i_UserChoice, (float)eMainMenuOptions.Add_vehicle, (float)eMainMenuOptions.Exit);
            }
        }
    
        private void handleAddVehicle() {
            getLicensePlate(out string licensePlate);
            if (!Garage.TryToMoveVehicleToRepair(licensePlate)) 
            {
                Utilities.EnumToMenu<eSupportVehicles>("Please enter the Vehicle you want to add from the supported options:");
                VehicleFactory factory = getFactory(out eSupportVehicles vehicleType);
                factory.CreateGarageEntry(vehicleType, Garage, licensePlate);
            }
        }
    
        private void handlePrintLicensePlatesOrderByFilter() {
                VehicleFilter? filter = getVehicleFilterOrNull();
                List<string> licensePlates = Garage.GetAllLicensePlatesRegistered(filter);
                StringBuilder output = new StringBuilder();
                if (licensePlates.Count > 0)
                {
                    foreach (string licensePlate in licensePlates)
                    {
                        output.AppendLine(licensePlate);
                    }
                }
                else
                {
                    output.AppendLine("No vehicles found.");
                }
                 Console.WriteLine(output.ToString());
        }

        private void handleUpdateVechileState() {
                getLicensePlate(out string licensePlate);
                int choice = Utilities.EnumMenuToIntChoiceWithValidation<eCarStatus>("Please enter the new vehicle status:", (int)eCarStatus.InRepair ,(int)eCarStatus.Paid);
                Garage.ChangeCarStatus(licensePlate, (eCarStatus)choice);
        }
    
        private void handleInflateAllWheelsToMax() {
                getLicensePlate(out string licensePlate);
                Garage.InflateWheelsToMax(licensePlate);
        }
    
        private void handleRefuelVehicle() {
                getLicensePlate(out string licensePlate);
                getAmountToAdd(out float amountToAdd);
                eFuelType fuelType = getFuelType();
                Garage.SupplyEnergy(licensePlate, amountToAdd, fuelType);
        }

        private void handleChargeVehicle() {
                getLicensePlate(out string licensePlate);
                getAmountToAdd(out float amountToAdd);
                Garage.SupplyEnergy(licensePlate, amountToAdd, null);
        }
    
        private void handleDisplayFullVehicleDetails() {
                getLicensePlate(out string licensePlate);
                Console.WriteLine(Garage.GetVehicleInfoByLicensePlate(licensePlate));
        }
    
        private void handleGarageExit() {
            printExitMessage();
            IsUserWantToExit = true;
        }
    
        private VehicleFactory getFactory(out eSupportVehicles io_VehicleType) {
             io_VehicleType = (eSupportVehicles)Utilities.GetSingleDigit();
             return FactoryStrategy.CreateFactory(io_VehicleType);
        }
    
        private void getLicensePlate(out string o_LicensePlate) {
            Console.WriteLine("Please enter the vehicle's license plate number:");
            o_LicensePlate = Utilities.GetNumberAsString(7, 8, "license plate number must contain between 7 and 8 digits.");
        }
    
        private VehicleFilter? getVehicleFilterOrNull() {
            int choice = Utilities.EnumMenuToIntChoiceWithValidation<eCarStatus>("Please enter the vehicle status you want to filter by or 0 if you don't want any filter:", (int)eCarStatus.InRepair - 1, (int)eCarStatus.Paid);
            return choice == 0 ? null : new VehicleFilter((eCarStatus)choice);
        }

        private eFuelType getFuelType() =>
            (eFuelType)Utilities.EnumMenuToIntChoiceWithValidation<eFuelType>("Please enter the fuel type you want to add:", (int)eFuelType.Octan95, (int)eFuelType.Solar);
    
        private static void getAmountToAdd(out float o_AmountToAdd) {
            Console.WriteLine("Please enter the amount of fuel you want to add:");
            o_AmountToAdd = Utilities.GetNumber<float>();
        }
    
        private void printWelcomeMessage() {

            StringBuilder opening = new StringBuilder(@"
██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗    ████████╗ ██████╗ 
██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝    ╚══██╔══╝██╔═══██╗
██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗         ██║   ██║   ██║
██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝         ██║   ██║   ██║
╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗       ██║   ╚██████╔╝
 ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝       ╚═╝    ╚═════╝ 
████████╗██╗  ██╗███████╗     ██████╗  █████╗ ██████╗  █████╗  ██████╗ ███████╗     
╚══██╔══╝██║  ██║██╔════╝    ██╔════╝ ██╔══██╗██╔══██╗██╔══██╗██╔════╝ ██╔════╝     
   ██║   ███████║█████╗      ██║  ███╗███████║██████╔╝███████║██║  ███╗█████╗       
   ██║   ██╔══██║██╔══╝      ██║   ██║██╔══██║██╔══██╗██╔══██║██║   ██║██╔══╝       
   ██║   ██║  ██║███████╗    ╚██████╔╝██║  ██║██║  ██║██║  ██║╚██████╔╝███████╗     
   ╚═╝   ╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝     
███╗   ███╗ █████╗ ███╗   ██╗ █████╗  ██████╗ ███████╗██████╗ ██╗                   
████╗ ████║██╔══██╗████╗  ██║██╔══██╗██╔════╝ ██╔════╝██╔══██╗██║                   
██╔████╔██║███████║██╔██╗ ██║███████║██║  ███╗█████╗  ██████╔╝██║                   
██║╚██╔╝██║██╔══██║██║╚██╗██║██╔══██║██║   ██║██╔══╝  ██╔══██╗╚═╝                   
██║ ╚═╝ ██║██║  ██║██║ ╚████║██║  ██║╚██████╔╝███████╗██║  ██║██╗                   
╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝                   
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
            ").AppendLine(@"
 .--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--. 
/ .. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \
\ \/\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ \/ /
 \/ /`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'\/ / 
 / /\                                                                                / /\ 
/ /\ \                                                                              / /\ \
\ \/ /        ▄████  ▄▄▄       ██▀███   ▄▄▄        ▄████ ▓█████                     \ \/ /
 \/ /        ██▒ ▀█▒▒████▄    ▓██ ▒ ██▒▒████▄     ██▒ ▀█▒▓█   ▀                      \/ / 
 / /\       ▒██░▄▄▄░▒██  ▀█▄  ▓██ ░▄█ ▒▒██  ▀█▄  ▒██░▄▄▄░▒███                        / /\ 
/ /\ \      ░▓█  ██▓░██▄▄▄▄██ ▒██▀▀█▄  ░██▄▄▄▄██ ░▓█  ██▓▒▓█  ▄                     / /\ \
\ \/ /      ░▒▓███▀▒ ▓█   ▓██▒░██▓ ▒██▒ ▓█   ▓██▒░▒▓███▀▒░▒████▒                    \ \/ /
 \/ /        ░▒   ▒  ▒▒   ▓▒█░░ ▒▓ ░▒▓░ ▒▒   ▓▒█░ ░▒   ▒ ░░ ▒░ ░                     \/ / 
 / /\         ░   ░   ▒   ▒▒ ░  ░▒ ░ ▒░  ▒   ▒▒ ░  ░   ░  ░ ░  ░                     / /\ 
/ /\ \      ░ ░   ░   ░   ▒     ░░   ░   ░   ▒   ░ ░   ░    ░                       / /\ \
\ \/ /            ░       ░  ░   ░           ░  ░      ░    ░  ░                    \ \/ /
 \/ /                                                                                \/ / 
 / /\        ███▄ ▄███▓ ▄▄▄       ███▄    █  ▄▄▄        ▄████ ▓█████  ██▀███         / /\ 
/ /\ \      ▓██▒▀█▀ ██▒▒████▄     ██ ▀█   █ ▒████▄     ██▒ ▀█▒▓█   ▀ ▓██ ▒ ██▒      / /\ \
\ \/ /      ▓██    ▓██░▒██  ▀█▄  ▓██  ▀█ ██▒▒██  ▀█▄  ▒██░▄▄▄░▒███   ▓██ ░▄█ ▒      \ \/ /
 \/ /       ▒██    ▒██ ░██▄▄▄▄██ ▓██▒  ▐▌██▒░██▄▄▄▄██ ░▓█  ██▓▒▓█  ▄ ▒██▀▀█▄         \/ / 
 / /\       ▒██▒   ░██▒ ▓█   ▓██▒▒██░   ▓██░ ▓█   ▓██▒░▒▓███▀▒░▒████▒░██▓ ▒██▒       / /\ 
/ /\ \      ░ ▒░   ░  ░ ▒▒   ▓▒█░░ ▒░   ▒ ▒  ▒▒   ▓▒█░ ░▒   ▒ ░░ ▒░ ░░ ▒▓ ░▒▓░      / /\ \
\ \/ /      ░  ░      ░  ▒   ▒▒ ░░ ░░   ░ ▒░  ▒   ▒▒ ░  ░   ░  ░ ░  ░  ░▒ ░ ▒░      \ \/ /
 \/ /       ░      ░     ░   ▒      ░   ░ ░   ░   ▒   ░ ░   ░    ░     ░░   ░        \/ / 
 / /\              ░         ░  ░         ░       ░  ░      ░    ░  ░   ░            / /\ 
/ /\ \                                                                              / /\ \
\ \/ /                                                                              \ \/ /
 \/ /                                                                                \/ / 
 / /\.--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--..--./ /\ 
/ /\ \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \.. \/\ \
\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `'\ `' /
 `--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--'`--' 
                            
            ");
            Console.WriteLine(opening.ToString());
        }
    
        private void printExitMessage() {
            StringBuilder exitMessage = new StringBuilder(@"
   ▄████████  ▄██████▄   ▄██████▄   ▄██████▄    ████████▄  ███   █▄    ▄████████▄                                   
  ███    ███ ███    ███ ███    ███ ███   ▀███   ███    ███ ███   ██▄   ███    ███                             
  ███    █▀  ███    ███ ███    ███ ███    ███   ███    ███ ███▄▄▄███   ███    █▀                              
 ▄███        ███    ███ ███    ███ ███    ███  ▄███▄▄▄██▀  ▀▀▀▀▀▀███  ▄███▄▄▄                                 
▀▀███ ████▄  ███    ███ ███    ███ ███    ███ ▀▀███▀▀▀██▄  ▄██   ███ ▀▀███▀▀▀                                 
  ███    ███ ███    ███ ███    ███ ███    ███   ███    ██▄ ███   ███   ███    █▄                              
  ███    ███ ███    ███ ███    ███ ███   ▄███   ███    ███ ███   ███   ███    ███                             
  ████████▀   ▀██████▀   ▀██████▀  ████████▀  ▄█████████▀   ▀█████▀    ██████████                             
                                                                                                              
 ▄█     █▄     ▄████████         ▄█    █▄     ▄██████▄     ▄███████▄    ▄████████          ███      ▄██████▄  
███     ███   ███    ███        ███    ███   ███    ███   ███    ███   ███    ███      ▀█████████▄ ███    ███ 
███     ███   ███    █▀         ███    ███   ███    ███   ███    ███   ███    █▀          ▀███▀▀██ ███    ███ 
███     ███  ▄███▄▄▄           ▄███▄▄▄▄███▄▄ ███    ███   ███    ███  ▄███▄▄▄              ███   ▀ ███    ███ 
███     ███ ▀▀███▀▀▀          ▀▀███▀▀▀▀███▀  ███    ███ ▀█████████▀  ▀▀███▀▀▀              ███     ███    ███ 
███     ███   ███    █▄         ███    ███   ███    ███   ███          ███    █▄           ███     ███    ███ 
███ ▄█▄ ███   ███    ███        ███    ███   ███    ███   ███          ███    ███          ███     ███    ███ 
 ▀███▀███▀    ██████████        ███    █▀     ▀██████▀   ▄████▀        ██████████         ▄████▀    ▀██████▀  
                                                                                                              
   ▄████████    ▄████████    ▄████████      ▄██   ▄    ▄██████▄  ███    █▄                                    
  ███    ███   ███    ███   ███    ███      ███   ██▄ ███    ███ ███    ███                                   
  ███    █▀    ███    █▀    ███    █▀       ███▄▄▄███ ███    ███ ███    ███                                   
  ███         ▄███▄▄▄      ▄███▄▄▄          ▀▀▀▀▀▀███ ███    ███ ███    ███                                   
▀███████████ ▀▀███▀▀▀     ▀▀███▀▀▀          ▄██   ███ ███    ███ ███    ███                                   
         ███   ███    █▄    ███    █▄       ███   ███ ███    ███ ███    ███                                   
   ▄█    ███   ███    ███   ███    ███      ███   ███ ███    ███ ███    ███                                   
 ▄████████▀    ██████████   ██████████       ▀█████▀   ▀██████▀  ████████▀                                    
                                                                                                              
   ▄████████    ▄██████▄     ▄████████  ▄█  ███▄▄▄▄           ▄████████  ▄██████▄   ▄██████▄  ███▄▄▄▄         
  ███    ███   ███    ███   ███    ███ ███  ███▀▀▀██▄        ███    ███ ███    ███ ███    ███ ███▀▀▀██▄       
  ███    ███   ███    █▀    ███    ███ ███▌ ███   ███        ███    █▀  ███    ███ ███    ███ ███   ███       
  ███    ███  ▄███          ███    ███ ███▌ ███   ███        ███        ███    ███ ███    ███ ███   ███       
▀███████████ ▀▀███ ████▄  ▀███████████ ███▌ ███   ███      ▀███████████ ███    ███ ███    ███ ███   ███       
  ███    ███   ███    ███   ███    ███ ███  ███   ███               ███ ███    ███ ███    ███ ███   ███       
  ███    ███   ███    ███   ███    ███ ███  ███   ███         ▄█    ███ ███    ███ ███    ███ ███   ███       
  ███    █▀    ████████▀    ███    █▀  █▀    ▀█   █▀        ▄████████▀   ▀██████▀   ▀██████▀   ▀█   █▀        
   
   "); 
            Console.WriteLine(exitMessage.ToString());                                                                                                          
        }
    }
}