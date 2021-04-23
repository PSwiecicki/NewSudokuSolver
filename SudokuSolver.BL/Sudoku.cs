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

        public void Solve()
        {
            this.UniquePossibilityInContainer();
            if (IsDone)
                return;
            this.SamePossibilitiesInFewFields();
            if (IsDone)
                return;
            this.SamePossibilities();
            if (IsDone)
                return;
        }
    }
}
