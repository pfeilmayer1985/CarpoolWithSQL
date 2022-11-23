using System;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;


namespace TecAlliance.Carpool.Business

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
                string inputToBeChecked = null;
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
                    inputToBeChecked = buffer.ToString();
                }

                if (inputToBeChecked == null)
                {
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
                string inputToBeChecked = null;
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
                    inputToBeChecked = buffer.ToString();
                }

                if (inputToBeChecked == null)
                {
                    continue;
                }

                inputToBeChecked = inputToBeChecked.TrimEnd().TrimStart();

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
