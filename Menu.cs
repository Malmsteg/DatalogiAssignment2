using System;
using System.Collections.Generic;

namespace DatalogiAssignment2
{
    public class Menu
    {        
        private readonly List<string> _menuOptions;
        private readonly bool _canExitProgram;
        private int _choice;
        public Menu(List<string> menuOptions, bool canExitProgram = false){
            _menuOptions = menuOptions;
            _canExitProgram = canExitProgram;
        }

        public int Choice {
            get { return _choice; }
        }

        public void CreateMenu(){
            PrintMenuOptions();
            CheckInput();
        }

        private void PrintMenuOptions(){
            for (int i = 0; i < _menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuOptions[i]}");
            }
            if (_canExitProgram){
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

        private void CheckInput(){
            int choice;
            bool isNumeric;

            do
            {
                isNumeric = int.TryParse(Console.ReadLine(), out choice);
                if (!isNumeric || choice < 1 || choice > _menuOptions.Count){
                    Error("Please enter a valid number.");
                }
            } while (!isNumeric || choice < 1 || choice > _menuOptions.Count);

            _choice = choice;
        }

        private static void Error(string errorMsg){
            Console.WriteLine(errorMsg);
            Console.Write("> ");
        }
    }
}