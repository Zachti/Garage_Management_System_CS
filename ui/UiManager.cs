using System.Text;

namespace Garage {

    internal class UIManager {
        private Garage Garage { get; }
        private Menu Menu { get; }

        public UIManager(Garage i_Garage) {
            Garage = i_Garage;
            Menu =
            [
                new Menu.Item(new AddVehicleCommand(this)),
                new Menu.Item(new ShowAllLicensePlatesOrderByFilterCommand(this)),
                new Menu.Item(new UpdateVechileStateCommand(this)),
                new Menu.Item(new InflateAllWheelsToMaxCommand(this)),
                new Menu.Item(new RefuelVehicleCommand(this)),
                new Menu.Item(new ChargeVehicleCommand(this)),
                new Menu.Item(new DisplayFullVehicleDetailsCommand(this))
            ];
        }

        public void Start() => Menu.Start();
    
        private void handleAddVehicle() 
        {
            getLicensePlate(out string licensePlate);
            if (!Garage.TryToMoveVehicleToRepair(licensePlate)) 
            {
                VehicleFactory vehicleFactory = getFactory(out eSupportVehicles vehicleType);
                vehicleFactory.CreateGarageEntry(vehicleType, Garage, licensePlate);
            }
        }
    
        private void handlePrintLicensePlatesOrderByFilter() 
        {
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

        private void handleUpdateVechileState() 
        {
            getLicensePlate(out string licensePlate);
            int choice = Utilities.EnumMenuToIntChoiceWithValidation<eCarStatus>("Please enter the new vehicle status:", (int)eCarStatus.InRepair ,(int)eCarStatus.Paid);
            Garage.ChangeCarStatus(licensePlate, (eCarStatus)choice);
        }
    
        private void handleInflateAllWheelsToMax() 
        {
            getLicensePlate(out string licensePlate);
            Garage.InflateWheelsToMax(licensePlate);
        }
    
        private void handleRefuelVehicle() 
        {
            getLicensePlate(out string licensePlate);
            getAmountToAdd(out float amountToAdd);
            eFuelType fuelType = getFuelType();
            Garage.SupplyEnergy(licensePlate, amountToAdd, fuelType);
        }

        private void handleChargeVehicle() 
        {
            getLicensePlate(out string licensePlate);
            getAmountToAdd(out float amountToAdd);
            Garage.SupplyEnergy(licensePlate, amountToAdd, null);
        }
    
        private void handleDisplayFullVehicleDetails() 
        {
            getLicensePlate(out string licensePlate);
            StringBuilder vehicleInfo = new StringBuilder(Garage.GetVehicleInfoByLicensePlate(licensePlate)).AppendLine();
            Console.WriteLine(vehicleInfo.ToString());
        }
    
        private VehicleFactory getFactory(out eSupportVehicles io_VehicleType) 
        {
            io_VehicleType = Utilities.EnumMenuToEnumChoice<eSupportVehicles>("Please enter the Vehicle you want to add from the supported options:");
            return FactoryStrategy.CreateFactory(io_VehicleType);
        }
    
        private void getLicensePlate(out string o_LicensePlate) 
        {
            Console.WriteLine("Please enter the vehicle's license plate number:");
            o_LicensePlate = Utilities.GetNumberAsString(7, 8, "license plate number must contain between 7 and 8 digits.");
        }
    
        private VehicleFilter? getVehicleFilterOrNull() 
        {
            int choice = Utilities.EnumMenuToIntChoiceWithValidation<eCarStatus>("Please enter the vehicle status you want to filter by or 0 if you don't want any filter:", (int)eCarStatus.InRepair - 1, (int)eCarStatus.Paid);
            return choice == 0 ? null : new VehicleFilter((eCarStatus)choice);
        }

        private eFuelType getFuelType() =>
            (eFuelType)Utilities.EnumMenuToIntChoiceWithValidation<eFuelType>("Please enter the fuel type you want to add:", (int)eFuelType.Octan95, (int)eFuelType.Soler);
    
        private static void getAmountToAdd(out float o_AmountToAdd) 
        {
            Console.WriteLine("Please enter the amount of fuel you want to add:");
            o_AmountToAdd = Utilities.GetNumber<float>();
        }
    
        private class AddVehicleCommand(UIManager i_Client) : ICommand {
            public void Execute() => i_Client.handleAddVehicle(); 
        }
    
        private class ShowAllLicensePlatesOrderByFilterCommand(UIManager i_Client) : ICommand {
            public void Execute() => i_Client.handlePrintLicensePlatesOrderByFilter(); 
        }

        private class UpdateVechileStateCommand(UIManager i_Client) : ICommand {
            public void Execute() => i_Client.handleUpdateVechileState(); 
        }

        private class InflateAllWheelsToMaxCommand(UIManager i_Client) : ICommand {
            public void Execute() => i_Client.handleInflateAllWheelsToMax(); 
        }

        private class RefuelVehicleCommand(UIManager i_Client) : ICommand {
            public void Execute() => i_Client.handleRefuelVehicle(); 
        }

        private class ChargeVehicleCommand(UIManager i_Client) : ICommand {
            public void Execute() => i_Client.handleChargeVehicle(); 
        }

        private class DisplayFullVehicleDetailsCommand(UIManager i_Client) : ICommand {
            private UIManager Client { get; } = i_Client;

            public void Execute() => Client.handleDisplayFullVehicleDetails(); 
        }
    }
}