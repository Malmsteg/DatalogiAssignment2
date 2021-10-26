using System;
using System.Collections.Generic;

namespace DatalogiAssignment2
{
    /// <summary>
    /// Class <c>Menu</c> models a menu.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Instance variable <c>_menuOptions</c> represents the menus options.
        /// </summary>
        private readonly List<string> _menuOptions;

        /// <summary>
        /// Instance variable <c>_canExitProgram</c> represents if the menu
        /// should contain an exit option.
        /// </summary>
        private readonly bool _canExitProgram;

        /// <summary>
        /// Instance variable <c>_choice</c> represents the users menu choice.
        /// </summary>
        private int _choice;

        /// <summary>
        /// This constructor initializes the new Menu with the options from 
        /// <paramref name="menuOptions"/> and an optional parameter 
        /// <paramref name="canExitProgram"/> that checks if you want an exit 
        /// option.
        /// </summary>
        /// <param name="menuOptions"><c>menuOptions</c> is the menu options.
        /// </param>
        /// <param name="canExitProgram"><c>canExitProgram</c> is an optional
        /// parameter that represents if the menu should contain an exit
        /// option. </param>
        public Menu(List<string> menuOptions, bool canExitProgram = false)
        {
            _menuOptions = menuOptions;
            _canExitProgram = canExitProgram;
        }

        /// <value>Property <c>Choice</c> represents the users choice.</value>
        public int Choice
        {
            get { return _choice; }
        }

        /// <summary>
        /// This method creates the menu.
        /// </summary>
        public void CreateMenu()
        {
            PrintMenuOptions();
            CheckInput();
        }

        /// <summary>
        /// This method prints all the menu options.
        /// </summary>
        private void PrintMenuOptions()
        {
            for (int i = 0; i < _menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuOptions[i]}");
            }
            if (_canExitProgram)
            {
                Console.WriteLine($"{_menuOptions.Count + 1}. Exit");
                Console.WriteLine(
                    $"Enter a menu selection between 1 - " +
                    $"{_menuOptions.Count + 1}");
            }
            else
            {
                Console.WriteLine(
                    $"Enter a menu selection between 1 - {_menuOptions.Count}");
            }
            Console.Write("> ");
        }

        /// <summary>
        /// This method checks if the users input is valid.
        /// </summary>
        private void CheckInput()
        {
            int choice;
            bool isNumeric;

            do
            {
                isNumeric = int.TryParse(Console.ReadLine(), out choice);
                if (!isNumeric || choice < 1 || choice > _menuOptions.Count)
                {
                    Error("Please enter a valid number.");
                }
            } while (!isNumeric || choice < 1 || choice > _menuOptions.Count);

            _choice = choice;
        }

        /// <summary>
        /// This method prints an error message.
        /// </summary>
        /// <param name="errorMsg"><c>errorMsg</c> is the message that should be
        /// printed. </param>
        private static void Error(string errorMsg)
        {
            Console.WriteLine(errorMsg);
            Console.Write("> ");
        }
    }
}