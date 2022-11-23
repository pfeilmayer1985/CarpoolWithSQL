using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fahrgemeinschaft
{
    public class UDrivers : UsersC
    {
        /// <summary>
        /// class properties
        /// </summary>
        public string CarTypeMake { get; set; }
        public int FreePlaces { get; set; }
        public List<UDrivers> DriversList { get; set; }
        string pathFileDrivers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\drivers.txt";
        string pathFileCarpools = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\carpools.txt";
        string pathFilePassengers = @"C:\010 Projects\006 Fahrgemeinschaft\Fahrgemeinschaft\passengers.txt";
        HandleUserInputC h = new HandleUserInputC();

        /// <summary>
        /// class constructor
        /// </summary>
        public UDrivers(string iD, string name, string startCity, string destination, string carTypeMake, int freePlaces)
        {
            ID = iD;
            Name = name;
            StartingCity = startCity;
            Destination = destination;
            CarTypeMake = carTypeMake;
            FreePlaces = freePlaces;
        }

        public UDrivers()
        {
            DriversList = new List<UDrivers>();
        }

        /// <summary>
        /// This method saves a driver account to the drivers.txt file
        /// </summary>
        public void AddDriver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║ You are now  registering as a driver ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            //if this file does not exist in the specified path
            if (!File.Exists(pathFileDrivers))
            {
                //the file will be created in the specified path
                File.Create(pathFileDrivers);
            }
            //if this file does not exist in the specified path
            if (!File.Exists(pathFileCarpools))
            {
                //the file will be created in the specified path
                File.Create(pathFileCarpools);
            }
            Console.Write("\nWhat's your first name?: ");
            string firstname = h.HandleUserTextInput(true);
            Console.Write("What's your last name?: ");
            string lastname = h.HandleUserTextInput(true);
            bool userInUse = false;
            string idDriver;
            idDriver = "DID#" + firstname.Substring(0, 3).ToUpper() + lastname.Substring(0, 3).ToUpper();
            do
            {
                bool ckeckInputDriverID = File.ReadLines(pathFileDrivers).Any(line => line.Contains(idDriver));
                if (ckeckInputDriverID || idDriver.Length != 10)
                {
                    //asking for the passenger ID and checking if the ID exists in the passengers list
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"This ID is allready in use or you used more/less characters than allowed." +
                        $"\nChoose another custom ID (except '{firstname.Substring(0, 3).ToUpper() + lastname.Substring(0, 3).ToUpper()}'!" +
                        $"Enter your ID now (6 characters long): ");
                    Console.ResetColor();
                    idDriver = "DID#" + Console.ReadLine().ToUpper();
                    userInUse = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Driver ID {idDriver} was generated and assigned to you based on your first and last name.");
                    Console.ResetColor();
                    userInUse = false;
                }

            } while (userInUse == true);
            Console.Write("What is the make of the car: ");
            string carMake = h.HandleUserTextInput();
            Console.Write("What is the model of the car: ");
            string carModel = h.HandleUserTextInput();
            Console.Write("How many places are free in the car (1 to 9): ");
            int freePlaces = h.HandleUserNumbersInput();
            Console.Write("Departure from City: ");
            string startCity = h.HandleUserTextInput(true);
            Console.Write("Destination City: ");
            string destination = h.HandleUserTextInput(true);
            File.AppendAllText(pathFileDrivers, ("\n" + idDriver + "," + freePlaces + "," + firstname + " " + lastname + "," + carMake + " " + carModel + "," + startCity + "," + destination));
            Console.WriteLine($"\nThe new user ID {idDriver} for {firstname + " " + lastname} was successfully added to the list.\nYou can now receive passengers.");
            bool pressedTheRightKey = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\nWould you like to register a passenger account with the same data as well (y/n)?");
                Console.ResetColor();
                ConsoleKeyInfo userInputKey = Console.ReadKey();
                string userInput = Convert.ToString(userInputKey.KeyChar);
                if (userInput == "y")
                {
                    bool userPassInUse = false;
                    string idPassenger;
                    idPassenger = "PID#" + firstname.Substring(0, 3).ToUpper() + lastname.Substring(0, 3).ToUpper();
                    do
                    {
                        bool ckeckInputPassengerID = File.ReadLines(pathFilePassengers).Any(line => line.Contains(idPassenger));
                        if (ckeckInputPassengerID || idPassenger.Length != 10)
                        {
                            //asking for the passenger ID and checking if the ID exists in the passengers list
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\n\nThis Passenger ID is allready in use or you used more/less characters than allowed." +
                                $"\nChoose another custom ID (except '{firstname.Substring(0, 3).ToUpper() + lastname.Substring(0, 3).ToUpper()}'!" +
                                $"Enter your ID now (6 characters long): ");
                            Console.ResetColor();
                            idPassenger = "PID#" + Console.ReadLine().ToUpper();
                            pressedTheRightKey = false;
                            userPassInUse = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n\nID {idPassenger} was assigned to your Passenger Account.");
                            Console.ResetColor();
                            pressedTheRightKey = false;
                            userPassInUse = false;
                        }

                    } while (userPassInUse == true);
                    File.AppendAllText(pathFilePassengers, ("\n" + idPassenger + "," + firstname + " " + lastname + "," + startCity + "," + destination));
                    Console.WriteLine($"\nThe new user ID {idPassenger} for {firstname + " " + lastname} was successfully added to the list.\nYou can now look for a carpool ride.");
                }
                else if (userInput == "n")
                {
                    pressedTheRightKey = false;
                }
                else
                {
                    continue;
                }
            } while (pressedTheRightKey);
            Program.PressEnterTxt();
        }

        /// <summary>
        /// This method shows a driver account details from the drivers.txt file, searching for driver by driver ID
        /// </summary>
        public void SeeDriver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║ See your driver acount details ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.ResetColor();
            //asking for the passenger ID
            Console.Write("\nEnter driver ID (DID#): ");
            string inputDriverID = Console.ReadLine().ToUpper();
            List<string> theDriversList = File.ReadAllLines(pathFileDrivers).ToList();
            var findDriverInDrivers = theDriversList.Where(e => e.Contains("DID#" + inputDriverID)).ToList();
            bool exists = false;
            foreach (var driver in theDriversList)
            {
                string[] strings = driver.Split(',');
                if (strings[0] == ("DID#" + inputDriverID))
                {
                    exists = true;
                }
            }

            if (exists)
            {
                List<string> theEditedUserDetails = new List<string>();
                foreach (var driver in theDriversList)
                {
                    string[] position = driver.Split(',');
                    if (position[0] == ("DID#" + inputDriverID))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"╔══════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine($"║ The following user informations are registered with your account ║");
                        Console.WriteLine($"╚══════════════════════════════════════════════════════════════════╝");
                        Console.ResetColor();
                        Console.WriteLine($"\nDriver ID: \t\t\t\t{position[0]}");
                        Console.WriteLine($"Driver's name: \t\t\t\t{position[2]}");
                        Console.WriteLine($"Registered vehicle: \t\t\t{position[3]}");
                        Console.WriteLine($"Free places available: \t\t\t{position[1]}");
                        Console.WriteLine($"Driving from location: \t\t\t{position[4]}");
                        Console.WriteLine($"Current registered destination: \t{position[5]}");
                        Program.PressEnterTxt();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The driver DID# does not exist, account content can't be retrieved.");
                Console.ResetColor();
                Program.PressEnterTxt();
            }
        }

        /// <summary>
        /// This main method manages a driver account details from the drivers.txt file, being able to "edit"/replace all drivers infos without ID and free places
        /// </summary>
        public void ManageDriverAccount()
        {
            bool userClassBool = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════╗");
            Console.WriteLine("║ Manage your driver acount ║");
            Console.WriteLine("╚═══════════════════════════╝");
            Console.ResetColor();
            //asking for the passenger ID
            Console.Write("\nEnter driver ID (DID#): ");
            string inputDriverID = Console.ReadLine().ToUpper();
            List<string> theDriversList = File.ReadAllLines(pathFileDrivers).ToList();
            var findDriverInDrivers = theDriversList.Where(e => e.Contains("DID#" + inputDriverID)).ToList();
            bool exists = false;
            foreach (var driver in theDriversList)
            {
                string[] strings = driver.Split(',');
                if (strings[0] == ("DID#" + inputDriverID))
                {
                    exists = true;
                }
            }

            if (exists)
            {
                List<string> theEditedUserDetails = new List<string>();
                foreach (var driver in theDriversList)
                {
                    string[] position = driver.Split(',');
                    if (position[0] == ("DID#" + inputDriverID))
                    {
                        do
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"╔═════════════════════════════════════════════════════╗");
                            Console.WriteLine($"║ The following user details are allowed to be edited ║");
                            Console.WriteLine($"╚═════════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"\nThe User ID and the free places can't be changed.\nIf you want to change these, delete this Driver Account and make a new account.");
                            Console.WriteLine($"\n( - )\tUser ID: {position[0]}");
                            Console.WriteLine($"( - )\tAvailable free places: {position[1]}");
                            Console.ResetColor();
                            Console.WriteLine($"( 1 )\tPassenger name: {position[2]}");
                            Console.WriteLine($"( 2 )\tCar make and model: {position[3]}");
                            Console.WriteLine($"( 3 )\tDriving from location: {position[4]}");
                            Console.WriteLine($"( 4 )\tCurrent destination: {position[5]}");
                            Console.WriteLine($"( 5 )\tBoth origin and destination");
                            Console.WriteLine($"( 6 )\tAll fields above (name, car, pickup & destination)");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"\n( 9 )\tDon't perform any changes, return to previous menu");
                            Console.ResetColor();
                            int userChoice;
                            bool pressedRightKey = false;
                            do
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("\nChoose one of the options above: ");
                                Console.ResetColor();
                                ConsoleKeyInfo userInputKey = Console.ReadKey();
                                string userInput = Convert.ToString(userInputKey.KeyChar);
                                if (!int.TryParse(userInput, out userChoice))
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

                            switch (userChoice)
                            {
                                case 1:
                                    EditDriverName(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 2:
                                    EditDriverCarMakeModel(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 3:
                                    EditDriverOrigin(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 4:
                                    EditDriverDestination(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 5:
                                    EditDriverOriginAndDestination(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 6:
                                    EditDriverAll(inputDriverID, theDriversList, position);
                                    userClassBool = false;
                                    continue;
                                case 9:
                                    userClassBool = false;
                                    break;
                                default:
                                    userClassBool = true;
                                    continue;
                            }
                        } while (userClassBool);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The driver DID# does not exist, account can't be edited.");
                Console.ResetColor();
                Program.PressEnterTxt();
            }
        }

        /// <summary>
        /// This is a sub method of the manage driver account method, where all fields are editable
        /// </summary>
        private void EditDriverAll(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit all driver data
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ You chose to change all the available driver information  ║");
            Console.WriteLine($"╚═══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            //Ask user for the new name
            Console.Write("\nWhat's your new first name?: ");
            string firstname = h.HandleUserTextInput(true);
            Console.Write("What's your new last name?: ");
            string lastname = h.HandleUserTextInput(true);
            string newUserName = firstname + " " + lastname;
            //Ask user for the new make and model
            Console.Write($"\nWhat make and model of a car are you driving now: ");
            string newMakeModel = h.HandleUserTextInput();
            //Ask user for the new city - start point
            Console.Write($"\nWhat's your new city, that you are driving from (origin), called: ");
            string newOrigin = h.HandleUserTextInput(true);
            //Ask user for the new city - destination
            Console.Write($"\nWhat's your new city, that you are driving to (destination), called: ");
            string newDestination = h.HandleUserTextInput(true);
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{newUserName},{newMakeModel},{newOrigin},{newDestination}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the driver's name from \" {position[2]} \" to \" {newUserName} \",");
            Console.WriteLine($"then the car make and model from \" {position[3]} \" to \" {newMakeModel} \",");
            Console.WriteLine($"and changed the city you are driving from, from \" {position[4]} \" to \" {newOrigin} \",");
            Console.WriteLine($"finally the city you are driving to, from \" {position[5]} \" to \" {newDestination} \" !");
            Console.ResetColor();
            Program.PressEnterTxt();
        }

        /// <summary>
        /// This is a sub method of the manage driver account method, where only origin and destination fields are editable
        /// </summary>
        private void EditDriverOriginAndDestination(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit driving origin and destination
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"╔═══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ You chose to change both cities : origin and the destination city ║");
            Console.WriteLine($"╚═══════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            //Ask user for the new city - start point
            Console.Write($"\nWhat's your new city, that you are driving from (origin), called: ");
            string newOrigin = h.HandleUserTextInput(true);
            //Ask user for the new city - destination
            Console.Write($"\nWhat's your new city, that you are driving to (destination), called: ");
            string newDestination = h.HandleUserTextInput(true);
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{position[3]},{newOrigin},{newDestination}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the city you are driving from, from \" {position[4]} \" to \" {newOrigin} \",");
            Console.WriteLine($"and the city you are driving to, from \" {position[5]} \" to \" {newDestination} \" !");
            Program.PressEnterTxt();
            Console.ResetColor();
        }

        /// <summary>
        /// This is a sub method of the manage driver account method, where only the destination field is editable
        /// </summary>
        private void EditDriverDestination(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit driving destination
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"╔══════════════════════════════════════════╗");
            Console.WriteLine($"║ You chose to change the destination city ║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.ResetColor();
            //Ask user for the new city - destination
            Console.Write($"\nWhat's your new city, that you are driving to, called: ");
            string newDestination = h.HandleUserTextInput(true);
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{position[3]},{position[4]},{newDestination}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the city you are driving to, from \" {position[5]} \" to \" {newDestination} \" !");
            Console.ResetColor();
            Program.PressEnterTxt();
        }

        /// <summary>
        /// This is a sub method of the manage driver account method, where only the origin field is editable
        /// </summary>
        private void EditDriverOrigin(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit driving from location
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"╔═══════════════════════════════════════════════════╗");
            Console.WriteLine($"║ You chose to change the city you are driving from ║");
            Console.WriteLine($"╚═══════════════════════════════════════════════════╝");
            Console.ResetColor();
            //Ask user for the new city - start point
            Console.Write($"\nWhat's your new city, that you are driving from, called: ");
            string newOrigin = h.HandleUserTextInput(true);
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{position[3]},{newOrigin},{position[5]}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the city you are driving from, from \" {position[4]} \" to \" {newOrigin} \" !");
            Console.ResetColor();
            Program.PressEnterTxt();
        }

        /// <summary>
        /// This is a sub method of the manage driver account method, where only the car/make field is editable
        /// </summary>
        private void EditDriverCarMakeModel(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit car make and model
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"╔════════════════════════════════════════════╗");
            Console.WriteLine($"║ You chose to change the car make and model ║");
            Console.WriteLine($"╚════════════════════════════════════════════╝");
            Console.ResetColor();
            //Ask user for the new make and model
            Console.Write($"\nWhat make and model of a car are you driving now: ");
            string newMakeModel = h.HandleUserTextInput(true);
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{position[2]},{newMakeModel},{position[4]},{position[5]}";
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the car make and model from \" {position[3]} \" to \" {newMakeModel} \" !");
            Console.ResetColor();
            Program.PressEnterTxt();
        }

        /// <summary>
        /// This is a sub method of the manage driver account method, where only the driver name field is editable
        /// </summary>
        private void EditDriverName(string inputDriverID, List<string> theDriversList, string[] position)
        {
            //edit driver name
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"╔═══════════════════════════════════════╗");
            Console.WriteLine($"║ You chose to change the driver's name ║");
            Console.WriteLine($"╚═══════════════════════════════════════╝");
            Console.ResetColor();
            //Ask user for the new name
            Console.Write("\nWhat's your new first name?: ");
            string firstname = h.HandleUserTextInput(true);
            Console.Write("What's your new last name?: ");
            string lastname = h.HandleUserTextInput(true);
            string newUserName = firstname + " " + lastname;
            //build a new string with all the drivers data
            string editedDriver = $"{position[0]},{position[1]},{newUserName},{position[3]},{position[4]},{position[5]}".TrimEnd();
            //select all other lines in the drivers.txt file add add them to a list
            List<string> addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
            //to the previously list you built with all other drivers - current, add the current edited driver
            addAllOtherEntriesBack.Add(editedDriver);
            //rewrite the drivers.txt file with the modified entry+all other entries
            File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack.Select(e => e.TrimEnd()));
            //show the new user info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have successfully changed the driver's name from \" {position[2]} \" to \" {newUserName} \" !");
            Console.ResetColor();
            Program.PressEnterTxt();
        }

        /// <summary>
        /// This method lists all the drivers saved in the drivers.txt
        /// </summary>
        public void ListAllDrivers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║ The following carpools are now available ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            string[] showDriversList = File.ReadAllLines(pathFileDrivers);
            int counterAvailable = 1;
            int counterUnavailable = 1;
            foreach (string driver in showDriversList)
            {
                string[] splittetDriverArray = driver.Split(',');
                if (Convert.ToInt32(splittetDriverArray[1]) > 0)
                {
                    Console.WriteLine("\n════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"{counterAvailable}.\t{splittetDriverArray[2]} has {splittetDriverArray[1]} free places available and is driving a {splittetDriverArray[3]} from {splittetDriverArray[4]} to {splittetDriverArray[5]}.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\tHis driver ID is: {splittetDriverArray[0]}");
                    Console.ResetColor();
                    counterAvailable++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ The following carpools are for the moment full and can't take any passengers ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            foreach (string driver in showDriversList)
            {
                string[] splittetDriverArray = driver.Split(',');
                if (Convert.ToInt32(splittetDriverArray[1]) == 0)
                {
                    Console.WriteLine($"\n{counterUnavailable}.\t{splittetDriverArray[2]} is driving a {splittetDriverArray[3]} from {splittetDriverArray[4]} to {splittetDriverArray[5]}. Unfortunately he does not have any free seats available.");
                    counterUnavailable++;
                }
            }
            Program.PressEnterTxt();
        }

        /// <summary>
        /// Main method to delete a drivers account. Taking input from user and calling the sub method
        /// </summary>
        public void RemoveDriverAccount()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Remove a driver acount from both drivers and carpool lists ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            //asking for the driver ID
            Console.Write("Enter driver ID (DID#): ");
            string inputDriverID = Console.ReadLine().ToUpper();
            SMRemoveDriverAccountByDriverID(inputDriverID);
        }

        /// <summary>
        /// Sub method to delete a drivers account, looking for a driver after driver ID taken from user input
        /// </summary>
        public void SMRemoveDriverAccountByDriverID(string inputDriverID)
        {
            //removing a driver from the drivers list
            List<string> theDriversList = File.ReadAllLines(pathFileDrivers).ToList();
            var findDriverInDrivers = theDriversList.FirstOrDefault(e => e.Contains("DID#" + inputDriverID));
            bool exists = false;
            bool existsInCarpool = false;
            foreach (var driver in theDriversList)
            {
                string[] strings = driver.Split(',');
                if (strings[0] == ("DID#" + inputDriverID))
                {
                    exists = true;
                }
            }
            if (exists)
            {
                var addAllOtherEntriesBack = theDriversList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
                File.WriteAllLines(pathFileDrivers, addAllOtherEntriesBack);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Driver DID# {inputDriverID} was removed from the registered drivers list.");
                Console.ResetColor();
                //removing a driver from the carpool list
                List<string> theCarpoolList = File.ReadAllLines(pathFileCarpools).ToList();
                foreach (var driver in theCarpoolList)
                {
                    string[] strings = driver.Split(',');
                    if (strings[0] == ("DID#" + inputDriverID))
                    {
                        existsInCarpool = true;
                    }
                }
                if (existsInCarpool)
                {
                    var findDriverInCarpools = theCarpoolList.FirstOrDefault(e => e.Contains("DID#" + inputDriverID));

                    string[] arrayWithSplittedCarpoolLines = findDriverInCarpools.Split(',');

                    var addAllOtherEntriesBackToCarpoolList = theCarpoolList.Where(e => !e.Contains("DID#" + inputDriverID)).ToList();
                    File.WriteAllLines(pathFileCarpools, addAllOtherEntriesBackToCarpoolList);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Driver DID# {inputDriverID} was also removed from the carpool list and since the carpool was dissolved all his passengers were removed as well.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("The driver DID# doesn't have any carpools created, the carpool list remains untouched.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The driver DID# does not exist, account can't be deleted.");
                Console.ResetColor();
            }
            Program.PressEnterTxt();
        }
    }
}
