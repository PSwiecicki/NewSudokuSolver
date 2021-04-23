using SudokuSolver.BL;
using System;

namespace SudokuSolver.VMConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku actual = new();
            int[,] table = new int[,]
            {
                { 8, 0, 1, 0, 0, 0, 0, 4, 5},
                { 0, 0, 0, 0, 0, 0, 7, 0, 6},
                { 0, 5, 6, 0, 0, 0, 8, 0, 0},
                { 0, 9, 0, 7, 0, 0, 1, 0, 0},
                { 0, 0, 0, 0, 8, 0, 0, 0, 0},
                { 0, 0, 0, 2, 0, 0, 5, 3, 8},
                { 0, 0, 0, 0, 4, 0, 0, 8, 0},
                { 4, 2, 7, 0, 0, 0, 0, 1, 0},
                { 0, 0, 0, 0, 9, 0, 0, 0, 4}
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|7|1|9|2|6|3|4|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|4|9|8|5|1|7|2|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|5|6|4|7|3|8|9|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|9|8|7|3|4|1|6|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|3|2|6|8|5|4|7|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|6|4|2|1|9|5|3|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|1|5|3|4|2|6|8|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|2|7|5|6|8|9|1|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|8|3|1|9|7|2|5|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);
            actual.Solve();

            System.Console.WriteLine(expected);
            System.Console.WriteLine(actual.ToString());
            System.Console.WriteLine(actual.ToExtendedString());

        }
    }
}
