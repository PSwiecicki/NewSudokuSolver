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
                if (value == 0 && !IsSet)
                {
                    this.value = 0;
                    IsSet = false;
                }
                else if(value >= 1 && value <= 9 && !IsSet && PossibleValues.Contains(value))
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
            }

        }

        public Field(int value = 0)
        {
            PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ContainersWithThatField = new List<FieldsContainer>();
            SetValue(value);
        }
        public Field(List<int> possibleValues) : this(0)
        {
            var itemsToRemove = PossibleValues.Except(possibleValues).ToList();
            RemovePossibility(itemsToRemove);
        }


        public bool RemovePossibility(int item)
        {
            bool result;
            if(!IsSet)
            {
                result = PossibleValues.Remove(item);
                if (PossibleValues.Count == 1)
                {
                    Value = PossibleValues.First();
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
            Value = value;
            if (Value == value)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            if (IsSet)
                return Value.ToString();
            else
            {
                string result = "";
                foreach (var possibility in PossibleValues)
                    result += possibility + ", ";
                result = result.Remove(result.Length - 2, 2);
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Field field)
                return false;
            else
            {
                if (Value != 0)
                    return Value.Equals(field.Value);
                else if (field.Value != 0)
                    return false;
                else if (PossibleValues.Count != field.PossibleValues.Count)
                    return false;
                else
                {
                    for (int i = 0; i < PossibleValues.Count; i++)
                        if (PossibleValues[i] != field.PossibleValues[i])
                            return false;
                }
            }
            return true;
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
