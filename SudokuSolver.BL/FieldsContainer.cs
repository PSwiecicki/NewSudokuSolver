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
        public bool IsDone { get; private set; }

        public FieldsContainer()
        {
            Fields = new List<Field>();
            ValueToSet = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IsDone = false;
        }

        public void ClearPossibilities()
        {
            //Remove all value which are set
            ValueToSet.RemoveAll(v => Fields.Where(f => f.IsSet).Select(f => f.Value).Contains(v));
            foreach(var field in Fields.Where(x => !x.IsSet))
            {
                var valueToRemove = field.PossibleValues.Except(ValueToSet);
                field.RemovePossibility(valueToRemove);
            }
        }
        
    }
}