using SudokuSolver.BL;
using System;

namespace SudokuSolver.VMConsole
{
    public static class UserInterface
    {
        private static bool Quit = false;

        public static void CommandLoop()
        {
            while(!Quit)
            {
                Console.WriteLine("\nWhat would you like to do?");
                var command = Console.ReadLine().ToLower();
                ComandRoute(command);
            }
        }

        private static void ComandRoute(string command)
        {

            if(command.StartsWith("help"))
                HelpCommand();
            else if(command.StartsWith("new"))
                NewCommand();
            else if (command.StartsWith("insert"))
                InsertCommand();
            else if (command.StartsWith("change"))
                ChangeCommand(command);
            else if(command.StartsWith("solve"))
                SolveCommand();
            else if(command.StartsWith("quit"))
                Quit = true;
            else
                Console.WriteLine($"{command} wasn't recognized, please try again or write \"help\" to see availble commands.");
            
        }

        private static void NewCommand()
        {
            if (SudokuOperations.SudokuInstance != null)
            {
                Console.WriteLine("You already have created sudoku. Do you want to erase it and create a new one?");
                if (YesNoLoop())
                {
                    CreateNewSudoku();
                }
                else
                {
                    Console.WriteLine("New sudoku wasn't created.");
                }
            }
            else
            {
                CreateNewSudoku();
            }
        }

        private static void CreateNewSudoku()
        {
            SudokuOperations.SudokuInstance = new Sudoku();
            Console.WriteLine("Your sudoku has been created.");
        }

        private static bool YesNoLoop()
        {
            bool? result = null;
            while(!result.HasValue)
            {
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes")
                    result = true;
                else if (answer == "no")
                    result = false;
                else
                {
                    Console.WriteLine($"Can't recognize \"{answer}\" command. Try again with \"yes\" or \"no\".");
                }
            }
            return result.Value;
        }
        private static void InsertCommand()
        {
            SudokuOperations.GetData();
        }


        private static void ChangeCommand(string command)
        {
            Console.WriteLine("Change command");
        }

        private static void SolveCommand()
        {
            SudokuOperations.SudokuInstance.Solve();
            SudokuOperations.ShowData();
        }


        private static void HelpCommand()
        {
            Console.WriteLine("Help command");
        }
    }
}