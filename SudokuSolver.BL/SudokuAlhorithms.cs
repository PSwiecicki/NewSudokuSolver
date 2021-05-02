using System;
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
                        var detectedSquare = sudoku.Squares.Where(s => s.Fields.Contains(fieldsWithValue[0])).First();
                        bool areAllValueInSquare = true;
                        foreach (var field in fieldsWithValue)
                        {
                            if (detectedSquare != field.ContainersWithThatField[2]) // In ContainersWithThatField: 0 - row, 1 - column, 2 - square
                            {
                                areAllValueInSquare = false;
                                break;
                            }
                        }
                        if (areAllValueInSquare)
                        {
                            foreach (var field in detectedSquare.Fields.Where(x => !x.IsSet).ToList())
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
                foreach (var value in container.ValueToSet.ToList())
                {
                    var fieldsWithValue = container.Fields.Where(x => !x.IsSet && x.PossibleValues.Contains(value)).ToList();
                    if (fieldsWithValue.Count == 2 || fieldsWithValue.Count == 3)
                    {
                        var detectedRow = sudoku.Rows.Where(r => r.Fields.Contains(fieldsWithValue[0])).First();
                        var detectedColumn = sudoku.Columns.Where(c => c.Fields.Contains(fieldsWithValue[0])).First();
                        bool areAllValueInRow = true;
                        bool areAllValueInColumn = true;
                        foreach (var field in fieldsWithValue)
                        {
                            if (detectedRow != field.ContainersWithThatField[0]) // In ContainersWithThatField: 0 - row, 1 - column, 2 - square
                                areAllValueInRow = false;
                            if (detectedColumn != field.ContainersWithThatField[1]) // In ContainersWithThatField: 0 - row, 1 - column, 2 - square
                                areAllValueInColumn = false;
                            if (!(areAllValueInRow || areAllValueInColumn))
                                break;

                        }
                        if (areAllValueInRow)
                        {
                            foreach (var field in detectedRow.Fields.Where(x => !x.IsSet).ToList())
                            {
                                if (!fieldsWithValue.Contains(field))
                                    field.RemovePossibility(value);
                            }
                        }
                        if (areAllValueInColumn)
                        {
                            foreach (var field in detectedColumn.Fields.Where(x => !x.IsSet).ToList())
                            {
                                if (!fieldsWithValue.Contains(field))
                                    field.RemovePossibility(value);
                            }
                        }
                    }
                }
            }
        }

        public static void SamePossibilitiesInFewFields(this Sudoku sudoku)
        {
            foreach (var container in sudoku.Containers.Where(c => !c.IsDone).ToList())
            {
                foreach (var fieldToCheck in container.Fields.Where(f => !f.IsSet).ToList())
                {
                    if (!fieldToCheck.IsSet)
                    {
                        var detectedValues = fieldToCheck.PossibleValues;
                        List<Field> compatibleFields = new();
                        foreach (var possibleField in container.Fields.Where(f => !f.IsSet))
                        {
                            if (ContainsAllItems(detectedValues, possibleField.PossibleValues))
                                compatibleFields.Add(possibleField);
                        }
                        if (compatibleFields.Count == detectedValues.Count)
                        {
                            foreach (var field in container.Fields.Where(f => !f.IsSet && !compatibleFields.Contains(f)).ToList())
                            {
                                field.RemovePossibility(detectedValues);
                            }
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
                        sudoku.InsertData(newSudoku);
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