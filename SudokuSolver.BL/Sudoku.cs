using System;
using System.Linq;
using System.Collections.Generic;

namespace SudokuSolver.BL
{
    public class Sudoku
    {
        private readonly List<FieldsContainer> rows;
        private readonly List<FieldsContainer> columns;
        private readonly List<FieldsContainer> squares;

        public List<FieldsContainer> Rows { 
            get
            { 
                return rows;
            } 
        }
        public List<FieldsContainer> Columns {
            get
            {
                return columns;
            } 
        }
        public List<FieldsContainer> Squares {
            get
            {
                return squares;
            }
        }

        public IEnumerable<FieldsContainer> Containers {
            get
            {
                return rows.Concat(columns).Concat(squares);
            }
        }

        public bool IsDone 
        {
            get
            {
                foreach (var row in rows)
                    if(!row.IsDone)
                        return false;
                return true;
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var row in rows)
                    if (!row.IsValid)
                        return false;
                return true && error;
            }
        }

        private bool error;
        public Sudoku()
        {
            rows = new List<FieldsContainer>();
            columns = new List<FieldsContainer>();
            squares = new List<FieldsContainer>();
            error = true;

            for(int i = 0; i<9;i++)
            {
                rows.Add(new FieldsContainer());
                columns.Add(new FieldsContainer());
                squares.Add(new FieldsContainer());
            }

            
            //Adding new fields to rows, columns and squares
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    Field field = new ();

                    int squareIndex = (rowIndex / 3) * 3 + columnIndex / 3;

                    field.AddContainer(rows[rowIndex]).AddContainer(columns[columnIndex]).AddContainer(squares[squareIndex]);
                }
            }
        }

        private Sudoku (Sudoku toCopy) : this()
        {
            for (int rowIndex = 0; rowIndex < rows.Count && error; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns.Count && error; columnIndex++)
                {
                    
                    var currentFieldToCopy = toCopy.Rows[rowIndex].Fields[columnIndex];
                    var currentField = Rows[rowIndex].Fields[columnIndex];

                    if (currentFieldToCopy.IsSet)
                        error = currentField.SetValue(currentFieldToCopy.Value);
                    else
                    {
                        var itemsToRemove = currentField.PossibleValues.Except(currentFieldToCopy.PossibleValues).ToList();
                        currentField.RemovePossibility(itemsToRemove);
                    }
                }
            }
        }

        public Sudoku Clone()
        {
            return new Sudoku(this);
        }

        public void InsertData(int[,] dataTable)
        {
            if(dataTable.Length == 81)
                for(int rowIndex = 0; rowIndex < 9; rowIndex++)
                {
                    for(int columnIndex = 0; columnIndex < 9; columnIndex++)
                    {
                        Rows[rowIndex].InsertValue(columnIndex, dataTable[rowIndex, columnIndex]);
                    }
                }
        }

        public void InsertData(Sudoku sudoku)
        { 
            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    Rows[rowIndex].InsertValue(columnIndex, sudoku.Rows[rowIndex].Fields[columnIndex].Value);
                }
            }
        }

        public void Solve()
        {
            Sudoku sudokuBefore;
            do
            {
                do
                {
                    do
                    {
                        sudokuBefore = this.Clone();
                        this.UniquePossibilityInContainer();
                        if (IsDone || !IsValid || sudokuBefore.IsDone || !sudokuBefore.IsValid)
                            return;
                    }
                    while (!this.Equals(sudokuBefore));
                    this.SamePossibilities();
                    if (IsDone || !IsValid)
                        return;
                }
                while (!this.Equals(sudokuBefore));
                this.SamePossibilitiesInFewFields();
                if (IsDone || !IsValid)
                    return;
            }
            while (!this.Equals(sudokuBefore));
            this.ValueSubstitution();
        }

        public override string ToString()
        {
            string result = "+-+-+-+-+-+-+-+-+-+\n";
            foreach (var row in rows)
            {
                result += row.ToString() +
                    "\n+-+-+-+-+-+-+-+-+-+\n";
            }
            return result;
        }

        public string ToExtendedString()
        {
            string result = "+-+-+-+-+-+-+-+-+-+\n";
            foreach (var row in rows)
            {
                result += row.ToExtendedString() + 
                    "\n+-+-+-+-+-+-+-+-+-+\n";
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Sudoku sudoku)
                return false;
            else
            {
                for(int i = 0; i < Rows.Count; i++)
                {
                    if (!Rows[i].Equals(sudoku.Rows[i]))
                        return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            foreach(var row in Rows)
            {
                hashCode += row.GetHashCode();
            }
            return hashCode;
        }
    }
}
