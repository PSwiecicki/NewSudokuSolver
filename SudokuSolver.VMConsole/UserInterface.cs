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
            var commandParts = command.Split();

            if (commandParts.Length >= 2)
            {
                if (commandParts[1] == "row")
                {
                    ChangeRow();
                }
                else if (commandParts[1] == "column")
                {
                    ChangeColumn();
                }
                else if (commandParts[1] == "field")
                {
                    ChangeField();
                }
                else
                {
                    messages.Enqueue($"You should use \"field\", \"column\" or \"row\" instead of {commandParts[1]}.");
                }
            }
            else
            {
                messages.Enqueue("You need to specify what do you want to change - row, column or field.");
            }
        }

        private static void ChangeRow()
        {
            messages.Enqueue("Which row do you want change?");
            ShowMessages();
            var valueGetter = Console.ReadLine();
            if (int.TryParse(valueGetter, out int row))
            {
                if (1 <= row && row <= 9)
                    SudokuOperations.ChangeRow(row-1);
                else
                    messages.Enqueue("Wrong value. You should chose value from 1 to 9.");
            }
            else
                messages.Enqueue("That wasn't value.");
        }

        private static void ChangeColumn()
        {
            messages.Enqueue("Which column do you want change?");
            ShowMessages();
            var valueGetter = Console.ReadLine();
            if (int.TryParse(valueGetter, out int column))
            {
                if (1 <= column && column <= 9)
                    SudokuOperations.ChangeColumn(column-1);
                else
                    messages.Enqueue("Wrong value. You should chose value from 1 to 9.");
            }
            else
                messages.Enqueue("That wasn't value.");
        }

        private static void ChangeField()
        {
            messages.Enqueue("Which field do you want change?");
            messages.Enqueue("Insert row index.");
            ShowMessages();
            var valueGetter = Console.ReadLine();
            if (int.TryParse(valueGetter, out int row))
            {
                if (1 <= row && row <= 9)
                {
                    messages.Enqueue("Insert column index.");
                    ShowMessages();
                    valueGetter = Console.ReadLine();
                    if (int.TryParse(valueGetter, out int column))
                    {
                        if (1 <= column && column <= 9)
                            SudokuOperations.ChangeField(row - 1, column - 1);
                        else
                            messages.Enqueue("Wrong value. You should chose value from 1 to 9.");
                    }
                    else
                        messages.Enqueue("That wasn't value.");
                }
                else
                    messages.Enqueue("Wrong value. You should chose value from 1 to 9.");
            }
            else
                messages.Enqueue("That wasn't value.");
        }

        private static void SolveCommand()
        {
            SudokuOperations.Solve();
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