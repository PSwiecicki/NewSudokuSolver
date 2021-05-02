using SudokuSolver.BL;
using System;
using System.Collections.Generic;

namespace SudokuSolver.VMConsole
{
    class Program
    {
        static void Main()
        {
            UserInterface.Messages.Enqueue("Welcome to SudokuSolver!");
            
            UserInterface.CommandLoop();

            Console.WriteLine("Thanks for using SudokuSolver and have a nice day!");
            Console.Read();
        }
    }
}
