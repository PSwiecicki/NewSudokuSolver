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
                else if(value >= 1 && value <= 9 && PossibleValues != null && PossibleValues.Contains(value) && this.value == 0)
                {
                    this.value = value;
                    PossibleValues = null;
                    IsSet = true;
                    foreach (var container in ContainersWithThatField)
                    {
                        container.ValueToSet.Remove(Value);
                        container.ClearPossibilities(Value);
                    }
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
        public Field(List<int> possibleValues)
        {
            PossibleValues = new List<int>();
            foreach (var possibiity in possibleValues)
                PossibleValues.Add(possibiity);
            ContainersWithThatField = new List<FieldsContainer>();
            SetValue(0);
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
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";
            if (IsSet)
                result += Value;
            else
            {
                foreach (var possibility in PossibleValues)
                    result += possibility + ", ";
                result = result.Remove(result.Length - 2, 2);
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Field))
                return false;
            else
            {
                var fieldObj = (Field)obj;
                if (Value != 0)
                    return Value.Equals(fieldObj.Value);
                else if (fieldObj.Value != 0)
                    return false;
                else if (PossibleValues.Count != fieldObj.PossibleValues.Count)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < PossibleValues.Count; i++)
                        if (PossibleValues[i] != fieldObj.PossibleValues[i])
                            return false;
                    return true;
                }

            }
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            if (IsSet)
                hashCode = Value;
            else
                foreach (var possibility in PossibleValues)
                    hashCode = hashCode * 10 + possibility;
            return hashCode;
        }
    }
}
