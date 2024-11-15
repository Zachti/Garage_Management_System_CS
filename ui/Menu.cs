using System.Text;

namespace Garage {
internal class Menu : List<Menu.Item> {
    public bool IsUserWantToExit { get; set; } = false;
    internal class Item(Action command)
        {
            public Action Command { get; } = command;

            public virtual void Selected() {
            Command?.Invoke();
        }
    }

    public Menu(List<Action> i_MenuItems) {
        foreach (Action item in i_MenuItems) {
            Add(new Item(item));
        }
        Add(new Item(close));
    }
    
    public void Start() {
        printWelcomeMessage();
        while (!IsUserWantToExit) 
            {
                try {
                    eMainMenuOptions userChoice = showMenu();
                    executeChoice(userChoice);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
    }

    private void executeChoice(eMainMenuOptions choice) {
        int choiceIndex = (int)choice - 1;
        Utilities.ValidateNumberInRange(choiceIndex, 0, Count - 1, "Invalid choice number.");
        this[choiceIndex - 1].Selected();
}

    private eMainMenuOptions showMenu() => Utilities.EnumMenuToEnumChoice<eMainMenuOptions>("Please choose which action to make by inserting a chioce number below: ");

    private void printWelcomeMessage() 
        {

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
    
    private void close() {
        IsUserWantToExit = true;
        printExitMessage();
    }
    
    private void printExitMessage() 
        {
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