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
                bool result = true;
                foreach (var row in rows)
                    result &= row.IsDone;
                return result;
            }
        }

        public bool IsValidate
        {
            get
            {
                bool result = true;
                foreach (var row in rows)
                    result &= row.IsValid;
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
                    Field field = new ();

                    int squareIndex = (i / 3) * 3 + j / 3;

                    rows[i].Fields.Add(field);
                    columns[j].Fields.Add(field);
                    squares[squareIndex].Fields.Add(field);
                    field.AddContainer(rows[i]).AddContainer(columns[j]).AddContainer(squares[squareIndex]);
                }
            }
        }

        private Sudoku (Sudoku toCopy)
        {
            rows = new List<FieldsContainer>();
            columns = new List<FieldsContainer>();
            squares = new List<FieldsContainer>();

            for (int i = 0; i < 9; i++)
            {
                rows.Add(new FieldsContainer());
                columns.Add(new FieldsContainer());
                squares.Add(new FieldsContainer());
            }

            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < columns.Count; j++)
                {
                    Field field;
                    if (toCopy.Rows[i].Fields[j].IsSet)
                        field = new Field(toCopy.Rows[i].Fields[j].Value);
                    else
                        field = new Field(toCopy.Rows[i].Fields[j].PossibleValues);
                    
                    int squareIndex = (i / 3) * 3 + j / 3;

                    rows[i].Fields.Add(field);
                    columns[j].Fields.Add(field);
                    squares[squareIndex].Fields.Add(field);
                    field.AddContainer(rows[i]).AddContainer(columns[j]).AddContainer(squares[squareIndex]);
                }
            }
        }

        public Sudoku Clone()
        {
            return new Sudoku(this);
        }

        public void InsertDataTo(Sudoku clone)
        {
            for (int i = 0; i < clone.Rows.Count; i++)
            {
                for (int j = 0; j < clone.Rows[i].Fields.Count; j++)
                    if (Rows[i].Fields[j].IsSet)
                        clone.Rows[i].InsertValue(j, Rows[i].Fields[j].Value);
                    else
                    {
                        if (!clone.Rows[i].Fields[j].IsSet)
                        {
                            var itemsToRemove = clone.Rows[i].Fields[j].PossibleValues.Except(Rows[i].Fields[j].PossibleValues).ToList();
                            clone.Rows[i].RemovePossibility(j, itemsToRemove);
                        }
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
                        rows[i].InsertValue(j, dataTable[i, j]);
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
                        if (IsDone || !IsValidate || sudokuBefore.IsDone || !sudokuBefore.IsValidate) //Need to fix it becouse clone isn't a clone
                            return;
                    }
                    while (!this.Equals(sudokuBefore));
                    this.SamePossibilities();
                    if (IsDone || !IsValidate)
                        return;
                }
                while (!this.Equals(sudokuBefore));
                this.SamePossibilitiesInFewFields();
                if (IsDone || !IsValidate)
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
                result += row.ToString() + "\n+-+-+-+-+-+-+-+-+-+\n";
            }
            return result;
        }

        public string ToExtendedString()
        {
            string result = "+-+-+-+-+-+-+-+-+-+\n";
            foreach (var row in rows)
            {
                result += row.ToExtendedString() + "\n+-+-+-+-+-+-+-+-+-+\n";
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Sudoku))
                return false;
            else
            {
                var sudokuObj = (Sudoku)obj;
                for(int i = 0; i < Rows.Count; i++)
                {
                    if (!Rows[i].Equals(sudokuObj.Rows[i]))
                        return false;
                }
                return true;
            }
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
