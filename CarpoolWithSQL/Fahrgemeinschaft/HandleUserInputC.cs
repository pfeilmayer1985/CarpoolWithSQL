using System;
using System.Text.RegularExpressions;


namespace Fahrgemeinschaft

{
    /// <summary>
    /// handeling user input class. checking the user input and setting the borders for characters allowed
    /// </summary>
    public class HandleUserInputC
    {
        public HandleUserInputC()
        { }

        /// <summary>
        /// Handeling the stings/text input from user, limiting or not the use of special characters
        /// </summary>
        public string HandleUserTextInput(bool checkSpecialCharacter = false)
        {
            string userInput = "";
            bool pressedRightKey = true;
            do
            {
                string inputToBeChecked = Program.readLineWithCancel();
                if (inputToBeChecked == null)
                {
                    Program.MainScreen();
                    continue;
                }

                if (checkSpecialCharacter && !Regex.IsMatch(inputToBeChecked, @"^[a-zA-Z0-9_ -]*$"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nYou need help, mate! Are you for real?\nHow about you take your pills and try once again: ");
                    Console.ResetColor();
                    continue;
                }
                if (string.IsNullOrWhiteSpace(inputToBeChecked))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nYou need help, mate! How about you take your pills and try once again: ");
                    Console.ResetColor();
                }
                else
                {
                    pressedRightKey = false;
                    userInput = inputToBeChecked.TrimEnd().TrimStart();
                }
            } while (pressedRightKey);
            Console.Write("\n");
            return userInput;
        }

        /// <summary>
        /// Handeling the numbers/ints input from user
        /// </summary>
        public int HandleUserNumbersInput()
        {
            int userInput = 0;
            bool itIsntANumber = true;
            do
            {
                string inputToBeChecked = Program.readLineWithCancel();
                if (inputToBeChecked == null)
                {
                    Program.MainScreen();
                    continue;
                }
                inputToBeChecked = inputToBeChecked.TrimEnd().TrimStart();
                if (inputToBeChecked.ToString() == "exit".ToLower())
                {
                    Program.MainScreen();
                }
                if (!int.TryParse(inputToBeChecked, out userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nYou need help, mate!\nHow about you take your pills and try once again: ");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    itIsntANumber = false;
                    userInput = Convert.ToInt32(inputToBeChecked.TrimEnd().TrimStart());
                }
                if (userInput < 1 || userInput > 9)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nThis a Carpool App, not social media. Try again: ");
                    Console.ResetColor();
                    itIsntANumber = true;
                }
            } while (itIsntANumber);
            return userInput;
        }
    }
}