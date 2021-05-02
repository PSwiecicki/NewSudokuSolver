﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public static class SudokuAlhorithms
    {
        //If only one field have possible special value
        public static void UniquePossibilityInContainer(this Sudoku sudoku)
        {
            foreach (var container in sudoku.Containers.Where(c => !c.IsDone))
            {
                foreach (var value in container.ValueToSet.ToList())
                {
                    var fieldsWithValue = container.Fields.Where(x => !x.IsSet && x.PossibleValues.Contains(value)).ToList();
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
                foreach (var value in container.ValueToSet.ToList())
                {
                    var fieldsWithValue = container.Fields.Where(x => !x.IsSet && x.PossibleValues.Contains(value)).ToList();
                    if(fieldsWithValue.Count == 2 || fieldsWithValue.Count == 3)
                    {
                        var firstFieldSquare = sudoku.Squares.Where(s => s.Fields.Contains(fieldsWithValue[0])).ToList()[0];
                        bool areAllValueInSquare = true;
                        foreach (var field in fieldsWithValue)
                        {
                            if (firstFieldSquare != field.ContainersWithThatField[2])
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
                                    field.RemovePossibility(value);
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
                    var fieldsWithValue = container.Fields.Where(x => !x.IsSet && x.PossibleValues.Contains(value)).ToList();
                    if (fieldsWithValue.Count == 2 || fieldsWithValue.Count == 3)
                    {
                        var firstFieldRow = sudoku.Rows.Where(r => r.Fields.Contains(fieldsWithValue[0])).ToList()[0];
                        var firstFieldColumn = sudoku.Columns.Where(c => c.Fields.Contains(fieldsWithValue[0])).ToList()[0];
                        bool areAllValueInRow = true;
                        bool areAllValueInColumn = true;
                        foreach (var field in fieldsWithValue)
                        {
                            if (firstFieldRow != field.ContainersWithThatField[0])
                                areAllValueInRow = false;
                            if (firstFieldColumn != field.ContainersWithThatField[1])
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

        public static void SamePossibilitiesInFewFields(this Sudoku sudoku)
        {
            foreach (var container in sudoku.Containers.Where(c => !c.IsDone))
            {
                foreach (var fieldToCheck in container.Fields.Where(f => !f.IsSet))
                {
                    var fielfToCheckValues = fieldToCheck.PossibleValues;
                    List<Field> compatibleFields = new();
                    foreach (var possibleField in container.Fields.Where(f => !f.IsSet))
                    {
                        if (ContainsAllItems(fieldToCheck.PossibleValues, possibleField.PossibleValues))
                            compatibleFields.Add(possibleField);
                    }
                    if (compatibleFields.Count == fieldToCheck.PossibleValues.Count)
                    {
                        foreach (var field in container.Fields.Where(f => !f.IsSet && !compatibleFields.Contains(f)))
                        {
                            if(!fieldToCheck.IsSet)
                                field.RemovePossibility(field.PossibleValues.Intersect(fieldToCheck.PossibleValues).ToList());
                        }
                    }
                }
            }
        }

        public static void ValueSubstitution(this Sudoku sudoku)
        {
            int fieldIndexRow = -1, fieldIndexColumn = -1;
            for (int i = 2; i < 9; i ++)
            {
                foreach (var row in sudoku.Rows)
                {
                    foreach (var field in row.Fields.Where(f => !f.IsSet))
                    {
                        if (field.PossibleValues.Count == i)
                        {
                            fieldIndexRow = sudoku.Rows.IndexOf(field.ContainersWithThatField[0]);
                            fieldIndexColumn = sudoku.Columns.IndexOf(field.ContainersWithThatField[1]);
                            break;
                        }
                    }
                    if (fieldIndexRow != -1)
                        break;
                }
                if (fieldIndexRow != -1)
                    break;
            }
            if(fieldIndexRow != -1)
                foreach(var value in sudoku.Rows[fieldIndexRow].Fields[fieldIndexColumn].PossibleValues.ToList())
                {
                    var newSudoku = sudoku.Clone();
                    newSudoku.Rows[fieldIndexRow].InsertValue(fieldIndexColumn, value);
                    newSudoku.Solve();
                    if (newSudoku.IsValid && newSudoku.IsDone)
                    {
                        newSudoku.InsertDataTo(sudoku);
                        break;
                    }
                }
        }

        private static bool ContainsAllItems<T>(List<T> first, List<T>second)
        {
            foreach (var item in second)
                if (!first.Contains(item))
                    return false;
            return true;
        }
    }
}