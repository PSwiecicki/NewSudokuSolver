using SudokuSolver.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.VMConsole
{
    public class SudokuOperations
    {
        public static Sudoku SudokuInstance { get; set; }
        public static int[,] Data { get; set; }

        public static void ShowData()
        {
            Console.Clear();
            string result = "┌───┬───┬───┐\n";
            for(int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                    result += "├───┼───┼───┤\n";
                for(int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                        result += "│";
                    if (Data[i, j] == 0)
                        result += ".";
                    else
                        result += Data[i, j];
                }
                result += "│\n";
            }
            result += "└───┴───┴───┘";
            Console.WriteLine(result);
        }

        public static void ShowData(int indexI, int indexJ)
        {
            Console.Clear();
            string result = "┌───┬───┬───┐\n";
            for (int i = 0; i < 9; i++)
            {
                if (i != 0 && i % 3 == 0)
                    result += "├───┼───┼───┤\n";
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                        result += "│";
                    if (indexI == i && indexJ == j)
                        result += "v";
                    else if (Data[i, j] == 0)
                        result += ".";
                    else
                        result += Data[i, j];
                }
                result += "│\n";
            }
            result += "└───┴───┴───┘";
            Console.WriteLine(result);
        }

        public static void GetData()
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    ShowData(i, j);
                    var dataGetter = Console.ReadLine();
                    if(int.TryParse(dataGetter, out int num))
                    {
                        Data[i, j] = (1 <= num && num <= 9) ? num : 0;
                        if (!IsDataValid(i, j))
                        {
                            Data[i, j] = 0;
                            j--;
                        }
                    }
                }
            }
        }

        private static bool IsDataValid(int indexI, int indexJ)
        {
            if (Data[indexI, indexJ] != 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i == indexI)
                        continue;
                    if (Data[i, indexJ] == Data[indexI, indexJ])
                        return false;
                }
                for (int j = 0; j < 9; j++)
                {
                    if (j == indexJ)
                        continue;
                    if (Data[indexI, j] == Data[indexI, indexJ])
                        return false;
                }
                int squareIndex = indexI / 3 * 3 + indexJ / 3;
                int squareFirstFieldIndexI = squareIndex / 3 * 3;
                int squareFirstFieldIndexJ = (squareIndex % 3) * 3;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int checkingI = squareFirstFieldIndexI + i;
                        int checkingJ = squareFirstFieldIndexJ + j;
                        if (checkingI == indexI && checkingJ == indexJ)
                            continue;
                        if (Data[checkingI, checkingJ] == Data[indexI, indexJ])
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
