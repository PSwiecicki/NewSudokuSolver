using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public class FieldsContainer
    {
        private List<Field> fields;
        public IReadOnlyList<Field> Fields { 
            get
            {
                return fields.AsReadOnly();
            }
        }
        public List<int> ValueToSet { get; private set; }
        public bool IsDone { get; private set; }

        public FieldsContainer()
        {
            fields = new List<Field>(9);
            for (int i = 0; i < 9; i++)
                fields.Add(new Field());
            ValueToSet = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IsDone = false;
        }

        public void ClearPossibilities()
        {
            //Remove all value which are set
            ValueToSet.RemoveAll(v => fields.Where(f => f.IsSet).Select(f => f.Value).Contains(v));
            foreach(var field in fields.Where(x => !x.IsSet))
            {
                var valueToRemove = field.PossibleValues.Except(ValueToSet).ToList();
                field.RemovePossibility(valueToRemove);
            }
            if(ValueToSet.Count == 0)
            {
                IsDone = true;
            }
        }

        public void SetField(int index, int value)
        {
            if (value > 0 && value < 10)
                fields[index].Value = value;
        }

        public override string ToString()
        {
            string result = "|";
            foreach (var field in fields)
                result += field.Value + "|";
            return result;
        }
    }
}