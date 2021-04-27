using SudokuSolver.BL;
using System;
using System.Collections.Generic;

namespace SudokuSolver.VMConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SudokuSolver!");
            
            UserInterface.CommandLoop();

            Console.WriteLine("Thanks for using SudokuSolver and have a nice day!");
            Console.Read();



        }

        static void InsertData(Sudoku sudoku)
        {
            int[,] dataForSudoku = new int[9, 9];
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Console.Clear();
                    Console.WriteLine("Podaj wartość pola lub 0 jeżeli nie nie ma informacji");
                    Console.WriteLine($"Pole ({i}, {j}): ");
                    string response = Console.ReadLine();
                    if(!int.TryParse(response, out int result))
                    {
                        result = 0;
                    }
                    dataForSudoku[i, j] = result;
                }
            }
            sudoku.InsertData(dataForSudoku);
        }
    }
}
