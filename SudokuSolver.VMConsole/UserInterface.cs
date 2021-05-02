using SudokuSolver.BL;
using System;
using System.Collections.Generic;

namespace SudokuSolver.VMConsole
{
    public static class UserInterface
    {
        private static bool Quit = false;
        private static Queue<string> messages = new ();

        public static Queue<string> Messages { get => messages; set => messages = value; }

        public static void CommandLoop()
        {
            while(!Quit)
            {
                Messages.Enqueue("What would you like to do?");
                ShowMessages();
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
                Messages.Enqueue($"{command} wasn't recognized, please try again or write \"help\" to see availble commands.");    
        }

        private static void NewCommand()
        {
            ShowMessages();
            if (SudokuOperations.SudokuInstance != null)
            {
                Messages.Enqueue("You already have created sudoku. Do you want to erase it and create a new one?");
                if (YesNoLoop())
                {
                    CreateNewSudoku();
                }
                else
                {
                    Messages.Enqueue("New sudoku wasn't created.");
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
            SudokuOperations.Data = new int[9, 9];
            Messages.Enqueue("Your sudoku has been created.");
        }

        private static bool YesNoLoop()
        {
            bool? result = null;
            while(!result.HasValue)
            {
                ShowMessages();
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes")
                    result = true;
                else if (answer == "no")
                    result = false;
                else
                {
                    Messages.Enqueue($"Can't recognize \"{answer}\" command. Try again with \"yes\" or \"no\".");
                    Messages.Enqueue("Do you want to erase your sudoku and create a new one?");
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


        public static void ShowMessages()
        {
            Console.Clear();
            while (Messages.Count != 0)
                Console.WriteLine(Messages.Dequeue());
        }


    }
}