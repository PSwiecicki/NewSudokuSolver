﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public static class SudokuAlhorithms
    {
        public static void OnePossibleValueInContainer(this Sudoku sudoku)
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

        //If I have 2 or 3 possibilities with one value in one row or column and all this possibilities are in one square, then that square need to have this value in that row and we can remove other possibilitis
        public static void SamePossibilities(this Sudoku sudoku)
        {
            //For columns and rows
            foreach(var container in sudoku.Rows.Concat(sudoku.Columns))
            {
                foreach (var value in container.ValueToSet)
                {
                    var fieldsWithValue = container.Fields.Where(x => x.PossibleValues.Contains(value)).ToList();
                    if(fieldsWithValue.Count == 2 || fieldsWithValue.Count == 3)
                    {
                        var firstFieldSquare = sudoku.Squares.Where(s => s.Fields.Contains(fieldsWithValue[0])).ToList()[0];
                        bool areAllValueInSquare = true;
                        foreach (var field in fieldsWithValue)
                        {
                            if (!firstFieldSquare.Fields.Contains(field))
                            {
                                areAllValueInSquare = false;
                                break;
                            }
                        }
                        if (areAllValueInSquare)
                        {
                            foreach (var field in firstFieldSquare.Fields.Where(x => !x.IsSet))
                            {
                                if (!fieldsWithValue.Contains(field))
                                    field.PossibleValues.Remove(value);
                            }
                        }
                    }
                }
            }
            //For squares
            foreach (var container in sudoku.Squares)
            {
                foreach (var value in container.ValueToSet)
                {
                    var fieldsWithValue = container.Fields.Where(x => x.PossibleValues.Contains(value)).ToList();
                    if (fieldsWithValue.Count == 2 || fieldsWithValue.Count == 3)
                    {
                        var firstFieldRow = sudoku.Rows.Where(r => r.Fields.Contains(fieldsWithValue[0])).ToList()[0];
                        var firstFieldColumn = sudoku.Columns.Where(c => c.Fields.Contains(fieldsWithValue[0])).ToList()[0];
                        bool areAllValueInRow = true;
                        bool areAllValueInColumn = true;
                        foreach (var field in fieldsWithValue)
                        {
                            if (!firstFieldRow.Fields.Contains(field))
                                areAllValueInRow = false;
                            if (!firstFieldColumn.Fields.Contains(field))
                                areAllValueInColumn = false;
                            if (!(areAllValueInRow || areAllValueInColumn))
                                break;

                        }
                        if (areAllValueInRow)
                        {
                            foreach (var field in firstFieldRow.Fields.Where(x => !x.IsSet))
                            {
                                if (!fieldsWithValue.Contains(field))
                                    field.PossibleValues.Remove(value);
                            }
                        }
                        if (areAllValueInColumn)
                        {
                            foreach (var field in firstFieldColumn.Fields.Where(x => !x.IsSet))
                            {
                                if (!fieldsWithValue.Contains(field))
                                    field.PossibleValues.Remove(value);
                            }
                        }
                    }
                }
            }

        }
            
    }
}