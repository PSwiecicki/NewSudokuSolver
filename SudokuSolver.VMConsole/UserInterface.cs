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
                changeCommand(command);
            else if(command.StartsWith("solve"))
                SolveCommand();
            else if(command.StartsWith("quit"))
                Quit = true;
            else
                Console.WriteLine($"{command} wasn't recognized, please try again or write help to see availble commands.");
            
        }

        private static void changeCommand(string command)
        {
            Console.WriteLine("Change command");
        }

        private static void InsertCommand()
        {
            Console.WriteLine("Insert command");
        }

        private static void SolveCommand()
        {
            Console.WriteLine("Solve command");
        }

        private static void NewCommand()
        {
            Console.WriteLine("New command");
        }

        private static void HelpCommand()
        {
            Console.WriteLine("Help command");
        }
    }
}