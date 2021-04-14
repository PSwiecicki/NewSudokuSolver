using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public class Field
    {
        private int value;
        public List<int> PossibleValues { get; set; }
        public bool IsSet { get; private set; }
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                if (value == 0)
                {
                    this.value = 0;
                    IsSet = false;
                }
                else if(value >= 1 && value <= 9 && PossibleValues.Contains(value))
                {
                    this.value = value;
                    PossibleValues = null;
                    IsSet = true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Argument value should be between 1 to 9.");
                }
            }

        }

        public Field(int value = 0)
        {
            Value = value;
        }

        public void RemovePossibility(int item)
        {
            if(PossibleValues != null)
            {
                PossibleValues.Remove(item);
                if (PossibleValues.Count == 1)
                {
                    Value = PossibleValues[0];
                }
            }
            else
            {
                throw new InvalidOperationException("This field doesn't have any possible values to remove.");
            }
        }

        public void RemovePossibility(List<int> items)
        {
            foreach(var item in items)
            {
                RemovePossibility(item);
            }
        }
    }
}
