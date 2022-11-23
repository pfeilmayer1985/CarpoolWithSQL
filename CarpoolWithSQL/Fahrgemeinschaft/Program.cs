using System;
using System.Text;

namespace Fahrgemeinschaft
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainScreen();
        }

        /// <summary>
        /// The MainScreen method is the main menu of the carpool app, where the user chooses it's class (driver/passenger)
        /// </summary>
        public static void MainScreen()
        {
            bool userClassBool = true;
            do
            {
                //Clearing console and showing the main menu in a loop. User can choose tthe drivers or passengers menu, list all the carpools or exit
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                CwL("╔═════════════════════════════════╗\n" +
                    "║        Welcome to Narnia        ║\n" +
                    "╚═════════════════════════════════╝");
                Console.ResetColor();
                CwL("\n( 1 )\tDrivers" +
                    "\n( 2 )\tPassengers" +
                    "\n( 3 )\tSee existing carpools");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 4 )\tExit");
                Console.ResetColor();
                int userClass;
                CarpoolC carpools = new CarpoolC();
                //if user chooses a non-existing menu item stays in loop until he enters a existing value
                bool pressedRightKey = false;
                do
                {
                    ChoseOptionAbvTxt();
                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);
                    if (!int.TryParse(userInput, out userClass))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Would you like to try again, this time with your brain switched on before typing?");
                        Console.ResetColor();
                        pressedRightKey = true;
                    }
                    else
                    {
                        pressedRightKey = false;
                        continue;
                    }
                } while (pressedRightKey);

                switch (userClass)
                {
                    case 1:
                        DriverMenu();
                        userClassBool = true;
                        continue;
                    case 2:
                        PassengerMenu();
                        userClassBool = true;
                        continue;
                    case 3:
                        carpools.ListAllCarpools();
                        userClassBool = true;
                        continue;
                    case 4:
                        Console.WriteLine("You choose to leave. Have a great one!");
                        userClassBool = false;
                        break;
                    default:
                        userClassBool = true;
                        continue;
                }
            } while (userClassBool);
        }

        /// <summary>
        /// This is the main menu for the Drivers class, managing account, taking a passenger (making a carpool), kick passenger, see the passengers, drivers and carpool lists
        /// </summary>
        public static void DriverMenu()
        {
            bool userDriverBool = true;
            do
            {
                UDrivers driversClass = new UDrivers();
                UPassengers passengersClass = new UPassengers();
                CarpoolC carpoolsClass = new CarpoolC();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                CwL("╔═════════════════════════════════╗\n" +
                    "║           Drivers Menu          ║\n" +
                    "╚═════════════════════════════════╝");
                Console.ResetColor();
                CwL("\n( 1 )\tManage your Driver Account" +
                    "\n( 2 )\tTake a passenger (you must be registered)" +
                    "\n( 3 )\tKick a passenger from your carpool" +
                    "\n( 4 )\tSearch for a passenger by departure city and destination city" +
                    "\n( 5 )\tSee the entire list of Passengers to find a match" +
                    "\n( 6 )\tAre you registered? See the list of Drivers" +
                    "\n( 7 )\tSee existing carpools");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the main menu");
                Console.ResetColor();
                //if user chooses a non-existing menu item stays in loop until he enters a existing value
                int driverMenu;
                bool pressedRightKey = false;
                do
                {
                    ChoseOptionAbvTxt();
                    //string userInput = Console.ReadLine();
                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);
                    if (!int.TryParse(userInput, out driverMenu))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Would you like to try again, this time with your brain switched on before typing?");
                        Console.ResetColor();
                        pressedRightKey = true;
                    }
                    else
                    {
                        pressedRightKey = false;
                        continue;
                    }
                } while (pressedRightKey);

                switch (driverMenu)
                {
                    case 1:
                        DriverAccountMenu();
                        userDriverBool = true;
                        continue;
                    case 2:
                        carpoolsClass.OfferCarpoolToPassenger();
                        userDriverBool = true;
                        continue;
                    case 3:
                        carpoolsClass.RemovePassengerFromCarpool();
                        userDriverBool = true;
                        continue;
                    case 4:
                        carpoolsClass.SearchPassengerStartDestination();
                        userDriverBool = true;
                        continue;
                    case 5:
                        passengersClass.ListAllPassengers();
                        userDriverBool = true;
                        continue;
                    case 6:
                        driversClass.ListAllDrivers();
                        userDriverBool = true;
                        continue;
                    case 7:
                        carpoolsClass.ListAllCarpools();
                        userDriverBool = true;
                        continue;
                    case 9:
                        userDriverBool = false;
                        break;
                    default:
                        userDriverBool = true;
                        continue;
                }
            } while (userDriverBool);
        }

        /// <summary>
        /// This is the account management for the drivers class, register, edit, delete account
        /// </summary>
        public static void DriverAccountMenu()
        {
            bool userDriverBool = true;
            do
            {
                UDrivers driversClass = new UDrivers();
                UPassengers passengersClass = new UPassengers();
                CarpoolC carpoolsClass = new CarpoolC();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                CwL("╔═════════════════════════════════╗\n" +
                    "║    Driver Account Management    ║\n" +
                    "╚═════════════════════════════════╝");
                Console.ResetColor();
                CwL("\n( 1 )\tRegister as a new driver for the Carpool" +
                    "\n( 2 )\tSee your existing account details" +
                    "\n( 3 )\tEdit your existing account" +
                    "\n( 4 )\tDelete your driver account completely");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the drivers menu");
                Console.ResetColor();
                //if user chooses a non-existing menu item stays in loop until he enters a existing value
                int driverMenu;
                bool pressedRightKey = false;
                do
                {
                    ChoseOptionAbvTxt();
                    //string userInput = Console.ReadLine();
                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);
                    if (!int.TryParse(userInput, out driverMenu))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Would you like to try again, this time with your brain switched on before typing?");
                        Console.ResetColor();
                        pressedRightKey = true;
                    }
                    else
                    {
                        pressedRightKey = false;
                        continue;
                    }
                } while (pressedRightKey);

                switch (driverMenu)
                {
                    case 1:
                        driversClass.AddDriver();
                        userDriverBool = true;
                        continue;
                    case 2:
                        driversClass.SeeDriver();
                        continue;
                    case 3:
                        driversClass.ManageDriverAccount();
                        userDriverBool = true;
                        continue;
                    case 4:
                        driversClass.RemoveDriverAccount();
                        userDriverBool = true;
                        continue;
                    case 9:
                        userDriverBool = false;
                        break;
                    default:
                        userDriverBool = true;
                        continue;
                }
            } while (userDriverBool);
        }

        /// <summary>
        /// This is the main menu for the Passengers class, managing account, joining a carpool, cancelling a carpool, see the passengers, drivers and carpool lists 
        /// </summary>
        public static void PassengerMenu()
        {
            bool userPassengerBool = true;
            do
            {
                UPassengers passengersClass = new UPassengers();
                UDrivers driversClass = new UDrivers();
                CarpoolC carpoolsClass = new CarpoolC();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                CwL("╔═════════════════════════════════╗\n" +
                    "║         Passengers Menu         ║\n" +
                    "╚═════════════════════════════════╝");
                Console.ResetColor();
                CwL("\n( 1 )\tManage your Passenger Account" +
                    "\n( 2 )\tTake a ride (you must be registered)" +
                    "\n( 3 )\tCancel a ride - remove yourself from a carpool" +
                    "\n( 4 )\tSearch for a driver/carpool by departure city and destination city" +
                    "\n( 5 )\tSee the entire list of Drivers to find a match" +
                    "\n( 6 )\tAre you registered? See the list of Passengers" +
                    "\n( 7 )\tSee existing carpools");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the main menu");
                Console.ResetColor();
                //if user chooses a non-existing menu item stays in loop until he enters a existing value
                int passengerMenu;
                bool pressedRightKey = false;
                do
                {
                    ChoseOptionAbvTxt();
                    //string userInput = Console.ReadLine();
                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);
                    if (!int.TryParse(userInput, out passengerMenu))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Would you like to try again, this time with your brain switched on before typing?");
                        Console.ResetColor();
                        pressedRightKey = true;
                    }
                    else
                    {
                        pressedRightKey = false;
                        continue;
                    }
                } while (pressedRightKey);

                switch (passengerMenu)
                {
                    case 1:
                        PassengerAccountMenu();
                        userPassengerBool = true;
                        continue;
                    case 2:
                        carpoolsClass.AddPassengerToCarpool();
                        userPassengerBool = true;
                        continue;
                    case 3:
                        carpoolsClass.RemovePassengerFromCarpool();
                        userPassengerBool = true;
                        continue;
                    case 4:
                        carpoolsClass.SearchCarpoolStartDestination();
                        userPassengerBool = true;
                        continue;
                    case 5:
                        driversClass.ListAllDrivers();
                        userPassengerBool = true;
                        continue;
                    case 6:
                        passengersClass.ListAllPassengers();
                        userPassengerBool = true;
                        continue;
                    case 7:
                        carpoolsClass.ListAllCarpools();
                        userPassengerBool = true;
                        continue;
                    case 9:
                        userPassengerBool = false;
                        break;
                    default:

                        userPassengerBool = true;
                        continue;
                }
            } while (userPassengerBool);
        }

        /// <summary>
        /// This is the account management for the passengers class, register, edit, delete account
        /// </summary>
        public static void PassengerAccountMenu()
        {
            bool userPassengerBool = true;
            do
            {
                UPassengers passengersClass = new UPassengers();
                UDrivers driversClass = new UDrivers();
                CarpoolC carpoolsClass = new CarpoolC();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                CwL("╔═════════════════════════════════╗\n" +
                    "║  Passenger Account Management   ║\n" +
                    "╚═════════════════════════════════╝");
                Console.ResetColor();
                CwL("\n( 1 )\tRegister as a new passenger for the Carpool" +
                    "\n( 2 )\tSee your existing account details" +
                    "\n( 3 )\tEdit your existing account" +
                    "\n( 4 )\tDelete your passenger account completely");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n( 9 )\tBack to the passengers menu");
                Console.ResetColor();
                //if user chooses a non-existing menu item stays in loop until he enters a existing value
                int passengerMenu;
                bool pressedRightKey = false;
                do
                {
                    ChoseOptionAbvTxt();
                    ConsoleKeyInfo userInputKey = Console.ReadKey();
                    string userInput = Convert.ToString(userInputKey.KeyChar);
                    if (!int.TryParse(userInput, out passengerMenu))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Would you like to try again, this time with your brain switched on before typing?");
                        Console.ResetColor();
                        pressedRightKey = true;
                    }
                    else
                    {
                        pressedRightKey = false;
                        continue;
                    }
                } while (pressedRightKey);
                switch (passengerMenu)
                {
                    case 1:
                        passengersClass.AddPassenger();
                        userPassengerBool = true;
                        continue;
                    case 2:
                        passengersClass.SeePassenger();
                        userPassengerBool = true;
                        continue;
                    case 3:
                        passengersClass.ManagePassengerAccount();
                        userPassengerBool = true;
                        continue;
                    case 4:
                        carpoolsClass.RemovePassengerAccount();
                        userPassengerBool = true;
                        continue;
                    case 9:
                        userPassengerBool = false;
                        break;
                    default:
                        userPassengerBool = true;
                        continue;
                }
            } while (userPassengerBool);
        }

        /// <summary>
        /// Quick ConsoleWriteLine
        /// </summary>
        public static void CwL(params string[] words)
        {
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }

        /// <summary>
        /// Standard text for choose an option
        /// </summary>
        public static void ChoseOptionAbvTxt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔═════════════════════════════════╗");
            Console.Write("║ Choose one of the options above ║");
            Console.Write("\n╚═════════════════════════════════╝");
            Console.ResetColor();
        }


        /// <summary>
        /// Standard text for choose an option
        /// </summary>
        public static void PressEnterTxt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔═══════════════════════════════════════════════╗");
            Console.Write("║ Press <Enter> to return to the previous menu. ║");
            Console.Write("\n╚═══════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static string readLineWithCancel()
        {
            string result = null;
            StringBuilder buffer = new StringBuilder();
            //The key is read passing true for the intercept argument to prevent
            //any characters from displaying when the Escape key is pressed.
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                if (info.Key == ConsoleKey.Backspace)
                {
                    if (buffer.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        buffer.Length--;
                    }
                    info = Console.ReadKey(true);
                    continue;
                }
                else
                {
                    Console.Write(info.KeyChar);
                    buffer.Append(info.KeyChar);
                    info = Console.ReadKey(true);
                }
            }
            if (info.Key == ConsoleKey.Enter)
            {
                result = buffer.ToString();
            }
            return result;
        }
    }
}
