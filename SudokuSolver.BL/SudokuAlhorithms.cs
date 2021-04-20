using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public static class SudokuAlhorithms
    {
        public static void OneValueInContainer(this Sudoku sudoku)
        {
            foreach (var container in sudoku.Containers)
            {
                foreach (var value in container.ValueToSet)
                {
                    var fieldsWithValue = container.Fields.Where(x => x.PossibleValues.Contains(value)).ToList();
                    if (fieldsWithValue.Count == 1)
                    {
                        fieldsWithValue[0].SetValue(value);
                    }
                }
            }
        }
    }
}
