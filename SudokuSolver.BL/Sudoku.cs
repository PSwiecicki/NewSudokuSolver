using System;
using System.Linq;
using System.Collections.Generic;

namespace SudokuSolver.BL
{
    public class Sudoku
    {
        private List<FieldsContainer> rows;
        private List<FieldsContainer> columns;
        private List<FieldsContainer> squares;

        public bool IsDone 
        {
            get
            {
                bool result = true;
                foreach (var row in rows)
                    result &= row.IsDone;
                return result;
            }
        }
        

        public Sudoku()
        {
            rows = new List<FieldsContainer>();
            columns = new List<FieldsContainer>();
            squares = new List<FieldsContainer>();

            for(int i = 0; i<9;i++)
            {
                rows.Add(new FieldsContainer());
                columns.Add(new FieldsContainer());
                squares.Add(new FieldsContainer());
            }

            
            //Adding new fields to rows, columns and squares
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Field pole = new Field();

                    int squareIndex = (i / 3) * 3 + j / 3;

                    rows[i].Fields.Add(pole);
                    columns[j].Fields.Add(pole);
                    squares[squareIndex].Fields.Add(pole);
                }
            }
        }

        public void InsertData(int[,] dataTable)
        {
            if(dataTable.Length == 81)
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        rows[i].Fields[j].Value = dataTable[i, j];
                    }
                }
        }

        //Algorithms

        //If one field can be set to special value, this method set that value.
        private void OneValueInContainer()
        {
            foreach (var container in rows.Concat(squares).Concat(columns).Where(x => !x.IsDone))
            {
                OneValueInContainer(container);
            }
        }

        public void OneValueInContainer(FieldsContainer fieldsContainer)
        {
            foreach (var value in fieldsContainer.ValueToSet)
            {
                var fieldsWithValue = fieldsContainer.Fields.Where(x => x.PossibleValues.Contains(value)).ToList();
                if (fieldsWithValue.Count == 1)
                {
                    fieldsWithValue[0].Value = value;
                }
            }
        }
        
        //If one of possibilities is in 2 or 3 fields of one row and that fields are in the same square, then other fields in that square can't have that possibiliti value.
        //Same with column

        private void SamePossibilities()
        {
            foreach(var container in rows.Concat(columns).Where(x => !x.IsDone))
            {
                SamePossibilitiesInRow(container);
            }
            foreach(var container in squares.Where(x => !x.IsDone))
            {
                SamePossibilitiesInSquare(container);
            }
        }

        public void SamePossibilitiesInRow(FieldsContainer fieldsContainer)
        {
            foreach (var value in fieldsContainer.ValueToSet)
            {
                var fieldsWithValue = fieldsContainer.Fields.Where(x => x.PossibleValues.Contains(value)).ToList();
                if (fieldsWithValue.Count == 2 || fieldsWithValue.Count == 3)
                {
                    int firstFieldSquareIndex = -1;
                    foreach(var square in squares)
                    {
                        if (square.Fields.Contains(fieldsWithValue[0]))
                        {
                            firstFieldSquareIndex = squares.IndexOf(square);
                            break;
                        }
                    }
                    bool areAllValueInSquare = true;
                    foreach(var field in fieldsWithValue)
                    {
                        if(!squares[firstFieldSquareIndex].Fields.Contains(field))
                        {
                            areAllValueInSquare = false;
                            break;
                        }
                    }
                    if(areAllValueInSquare)
                    {
                        foreach(var field in squares[firstFieldSquareIndex].Fields.Where(x => !x.IsSet))
                        {
                            if (!fieldsWithValue.Contains(field))
                                field.PossibleValues.Remove(value);
                        }
                    }
                }
            }
        }

        public void SamePossibilitiesInSquare(FieldsContainer fieldsContainer)
        {
            foreach (var value in fieldsContainer.ValueToSet)
            {
                var fieldsWithValue = fieldsContainer.Fields.Where(x => x.PossibleValues.Contains(value)).ToList();
                if (fieldsWithValue.Count == 2 || fieldsWithValue.Count == 3)
                {
                    int firstFieldRowIndex = -1;
                    int firstFieldColumnIndex = -1;
                    foreach (var row in rows)
                    {
                        if (row.Fields.Contains(fieldsWithValue[0]))
                        {
                            firstFieldRowIndex = rows.IndexOf(row);
                            break;
                        }
                    }
                    foreach (var column in columns)
                    {
                        if (column.Fields.Contains(fieldsWithValue[0]))
                        {
                            firstFieldColumnIndex = columns.IndexOf(column);
                            break;
                        }
                    }
                    bool areAllValueInRow = true;
                    bool areAllValueInColumn = true;
                    foreach (var field in fieldsWithValue)
                    {
                        if (!rows[firstFieldRowIndex].Fields.Contains(field))
                            areAllValueInRow = false;
                        if (!columns[firstFieldColumnIndex].Fields.Contains(field))
                            areAllValueInColumn = false;
                    }
                    if (areAllValueInRow)
                    {
                        foreach (var field in rows[firstFieldRowIndex].Fields.Where(x => !x.IsSet))
                        {
                            if (!fieldsWithValue.Contains(field))
                                field.PossibleValues.Remove(value);
                        }
                    }
                    if (areAllValueInColumn)
                    {
                        foreach (var field in columns[firstFieldColumnIndex].Fields.Where(x => !x.IsSet))
                        {
                            if (!fieldsWithValue.Contains(field))
                                field.PossibleValues.Remove(value);
                        }
                    }
                }
            }
        }

        public void ClearAllPossibilities()
        {
            foreach (var container in rows.Concat(columns).Concat(squares).Where(x => !x.IsDone))
            {
                container.ClearPossibilities();
            }
        }

        
        public override string ToString()
        {
            string result = "+-+-+-+-+-+-+-+-+-+\n";
            foreach(var row in rows)
            {
                result += row.ToString() + "\n+-+-+-+-+-+-+-+-+-+\n";
            }
            return result;
        }
    }
}
