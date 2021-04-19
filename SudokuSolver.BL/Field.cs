using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public class Field
    {
        public List<FieldsContainer> ContainersWithThatField { get; set; }
        private int value;
        public List<int> PossibleValues { get; private set; }
        public bool IsSet { get; private set; }
        public int Value
        {
            get
            {
                return value;
            }
            private set
            {
                if (value == 0 && PossibleValues != null)
                {
                    this.value = 0;
                    IsSet = false;
                }
                else if(value >= 1 && value <= 9 && PossibleValues.Contains(value) && this.value == 0)
                {
                    this.value = value;
                    PossibleValues = null;
                    IsSet = true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(paramName: nameof(value), message: "Argument value should be between 1 to 9.");
                }
            }

        }

        public Field(int value = 0)
        {
            PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ContainersWithThatField = new List<FieldsContainer>();
            SetValue(value);
        }

        public bool RemovePossibility(int item)
        {
            bool result;
            if(PossibleValues != null)
            {
                result = PossibleValues.Remove(item);
                if (PossibleValues.Count == 1)
                {
                    SetValue(PossibleValues[0]);
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool RemovePossibility(List<int> items)
        {
            bool result = false;
            foreach(var item in items)
            {
                result |= RemovePossibility(item);
            }
            return result;
        }

        public bool SetValue(int value)
        {
            bool result = true;
            try
            {
                Value = value;
                foreach (var container in ContainersWithThatField)
                    container.ClearPossibilities(value);
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
