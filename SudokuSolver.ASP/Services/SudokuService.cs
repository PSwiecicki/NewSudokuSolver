using SudokuSolver.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuSolver.ASP.Services
{
    public static class SudokuService
    {
        static Sudoku sudoku { get; }

        static SudokuService()
        {
            //int[,] table = new int[,]
            //{
            //    { 4, 0, 5, 7, 0, 0, 0, 0, 0 },
            //    { 9, 2, 0, 0, 0, 0, 0, 0, 0 },
            //    { 0, 0, 0, 0, 0, 0, 1, 5, 8 },
            //    { 0, 0, 0, 0, 0, 0, 0, 6, 9 },
            //    { 0, 8, 0, 0, 0, 6, 7, 0, 0 },
            //    { 0, 9, 0, 0, 0, 0, 0, 0, 1 },
            //    { 6, 0, 0, 0, 9, 0, 0, 0, 3 },
            //    { 0, 0, 0, 0, 0, 7, 6, 0, 0 },
            //    { 5, 0, 0, 1, 0, 0, 0, 0, 2 }
            //};

            sudoku = new Sudoku();
            //sudoku.InsertData(table);
        }

        public static List<List<int>> Solve()
        {
            sudoku.Solve();
            List<List<int>> result = new List<List<int>>();
            foreach(var row in sudoku.Rows)
            {
                result.Add(new List<int>());
                foreach(var field in row.Fields)
                {
                    result[result.Count - 1].Add(field.Value.Value);
                }
            }
            return result;
        }
        public static bool InsertaData(int[,] data)
        {
            var result = true;
            sudoku.InsertData(data);
            for(var row = 0; row < sudoku.Rows.Count; row++)
            {
                for (var column = 0; column < sudoku.Rows[row].Fields.Count; column++)
                {
                    result &= data[row, column] == sudoku.Rows[row].Fields[column].Value;
                }
            }
            return result;
        }
    }
}
