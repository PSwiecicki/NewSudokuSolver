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
                }                result += "│\n";
            }
            result += "└───┴───┴───┘";
            UserInterface.Messages.Enqueue(result);
        }

        public static void ShowData(int indexI, int indexJ)
        {
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
            UserInterface.Messages.Enqueue(result);
        }

        public static void GetData()
        {
            ShowData(0,0);
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    UserInterface.Messages.Enqueue("Insert value");
                    UserInterface.ShowMessages();
                    var dataGetter = Console.ReadLine();
                    if(int.TryParse(dataGetter, out int num))
                    {
                        Data[i, j] = (1 <= num && num <= 9) ? num : 0;
                        if (!IsDataValid(i, j))
                        {
                            Data[i, j] = 0;
                            ShowData(i, j);
                            UserInterface.Messages.Enqueue($"{num} is incorrect now.");
                            j--;
                        }
                        else if (0 > num || num > 9)
                        {
                            ShowData(i, j);
                            UserInterface.Messages.Enqueue($"{num} is wrong value.");
                            j--;
                        }
                        else
                        {
                            ShowNextData(i, j);
                        }
                    }
                    else if(dataGetter.Length != 0)
                    {
                        ShowData(i, j);
                        UserInterface.Messages.Enqueue($"{dataGetter} is wrong value.");
                        j--;
                    }
                    else
                    {

                        ShowNextData(i, j);
                    }
                }
            }
        }

        private static void ShowNextData(int i, int j)
        {
            int nextJ = j + 1;
            int nextI = i;
            if (nextJ == 9)
            {
                nextJ = 0;
                nextI++;
            }
            if (nextI != 9)
                ShowData(nextI, nextJ);
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
