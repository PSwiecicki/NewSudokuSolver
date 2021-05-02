using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public class FieldsContainer
    {
        public List<Field> Fields { get; set; }
        public List<int> ValueToSet { get; private set; }
        public bool IsDone
        {
            get
            {
                foreach (var field in Fields)
                    if (!field.IsSet)
                        return false;
                return true;
            }
        }

        public bool IsValid
        {
            get
            { 
                var valueSetted = new HashSet<int>();
                var settedFields = Fields.Where(f => f.IsSet).ToList();
                foreach(var field in settedFields)
                {
                    valueSetted.Add(field.Value);
                }
                return valueSetted.Count == settedFields.Count;
            }
        }

        public FieldsContainer()
        {
            Fields = new List<Field>(9);
            ValueToSet = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public void ClearPossibilities(int value)
        {
            foreach(var field in Fields.Where(f => !f.IsSet).ToList())
            {
                field.RemovePossibility(value);
            }
        }

        public bool InsertValue(int fieldIndex, int value)
        {
            bool result;
            if (0 <= fieldIndex && fieldIndex < Fields.Count)
                result = Fields[fieldIndex].SetValue(value);
            else
                return false;
            return result;
        }

        public void RemovePossibility(int index, int item)
        {
            Fields[index].RemovePossibility(item);
        }

        public void RemovePossibility(int index, List<int> items)
        {
            foreach (var item in items)
            {
                RemovePossibility(index, item);
            }
        }

        public override string ToString()
        {
            string result = "|";
            foreach (var field in Fields)
            {
                result += field.Value + "|";
            }
            return result;
        }

        public string ToExtendedString()
        {
            string result = "|";
            foreach (var field in Fields)
            {
                result += field.ToString() + "|";
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || (obj is not FieldsContainer container))
                return false;
            else
            {
                for(int i = 0; i < this.Fields.Count; i++)
                {
                    if (!Fields[i].Equals(container.Fields[i]))
                        return false;
                }
                return true;
            }
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            for(int i = 0; i < Fields.Count; i++)
            {
                hashCode = hashCode * i + Fields[i].GetHashCode();
            }
            return hashCode;
        }
    }
}