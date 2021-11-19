using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public class Field
    {
        private List<FieldsContainer> containersWithThatField;
        private int? fieldValue;
        private List<int> possibleValues;

        public bool IsSet {
            get
            {
                return fieldValue.HasValue;
            }
        }
        public int? Value
        {
            get
            {
                return fieldValue;
            }
            private set
            {
                if(!IsSet)
                {
                    CheckValueIsCorrect(value);
                }
            }
        }
        
        public List<int> PossibleValues { 
            get
            {      
                bool isNotNull = possibleValues != null;
                if (isNotNull)
                    return new List<int>(possibleValues);
                else
                    return null;
            }
        }
        public List<FieldsContainer> ContainersWithThatField
        {
            get
            {
                return new List<FieldsContainer>(containersWithThatField);
            }
        }

        private void CheckValueIsCorrect(int? value)
        {
            if (!value.HasValue)
            {
                fieldValue = value;
            }
            else
            {
                int valueAsInt = value.Value;
                if (possibleValues.Contains(valueAsInt))
                {
                    SetFieldValue(valueAsInt);
                }
            }
        }

        private void SetFieldValue(int value)
        {
            fieldValue = value;
            possibleValues = null;
            RemovePossibilitiesFromContainersWithField(value);
        }

        private void RemovePossibilitiesFromContainersWithField(int value)
        {
            foreach (var container in containersWithThatField)
            {
                container.ValueToSet.Remove(value);
                container.ClearPossibilities(value); // Tym powinna zajmować się kontener, nie pole.
            }
        }

        public Field(int? value = null, int size = 9)
        {
            possibleValues = new List<int>();
            InitializePossibleValues(size);
            containersWithThatField = new List<FieldsContainer>();
            Value = value;
        }

        private void InitializePossibleValues(int size)
        {
            for (int i = 1; i <= size; i++)
                possibleValues.Add(i);
        }

        public bool RemovePossibility(int valueToRemove)
        {
            bool wasRemoved = false;
            if(!IsSet)
            {
                wasRemoved = possibleValues.Remove(valueToRemove);
                CheckIfItsLastOption();
            }
            return wasRemoved;
        }

        private void CheckIfItsLastOption()
        {
            bool isLast = possibleValues.Count == 1;
            if (isLast)
            {
                int onlyOption = possibleValues.First();
                Value = onlyOption;
            }
        }

        public bool RemovePossibility(List<int> valuesToRemove)
        {
            bool wasAnyRemoved = false;
            foreach(var valueToRemove in valuesToRemove)
            {
                bool removeResult = RemovePossibility(valueToRemove);
                wasAnyRemoved |= removeResult;
            }
            return wasAnyRemoved;
        }

        public bool SetValue(int value)
        {
            Value = value;
            bool isSetDone = Value == value;
            return isSetDone;
        }

        public void AddNewContainer(FieldsContainer container)
        {
            containersWithThatField.Add(container);
        }

        public override string ToString()
        {
            if (IsSet)
                return Value.ToString();
            else
            {
                return GetPossibilitiesString();
            }
        }

        private string GetPossibilitiesString()
        {
            string longerString = string.Join(", ", possibleValues);
            return longerString;
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
                else if (possibleValues.Count != field.possibleValues.Count)
                    return false;
                else
                {
                    for (int i = 0; i < possibleValues.Count; i++)
                        if (possibleValues[i] != field.possibleValues[i])
                            return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            if (IsSet)
                hashCode = (int)Value;
            else
                foreach (var possibility in possibleValues)
                    hashCode = hashCode * 10 + possibility;
            return hashCode;
        }
    }
}
