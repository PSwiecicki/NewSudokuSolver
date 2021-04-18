﻿using System;
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
            Fields = new List<Field>(9);
            ValueToSet = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            IsDone = false;
        }

        private void ClearPossibilities(int value)
        {
            foreach(var field in Fields.Where(f => !f.IsSet))
            {
                field.RemovePossibility(value);
                if (field.IsSet)
                    ClearPossibilities(field.Value);
            }
        }

        public void ClearPossibilities()
        {
            //Remove all value which are set
            ValueToSet.RemoveAll(v => Fields.Where(f => f.IsSet).Select(f => f.Value).Contains(v));
            foreach(var field in Fields.Where(x => !x.IsSet))
            {
                var valueToRemove = field.PossibleValues.Except(ValueToSet).ToList();
                field.RemovePossibility(valueToRemove);
                if(field.IsSet)
                {
                    ValueToSet.Remove(field.Value);   
                }
            }
            if(ValueToSet.Count == 0)
                IsDone = true;
        }

        public void InsertValue(int index, int value)
        {
            if (index >= 0 && index < Fields.Count)
            {
                if (Fields[index].SetValue(value))
                {
                    ClearPossibilities(value);
                    ValueToSet.Remove(value);
                }
                if (ValueToSet.Count == 0)
                    IsDone = true;
            }
        }

        public override string ToString()
        {
            string result = "|";
            foreach (var field in Fields)
                result += field.Value + "|";
            return result;
        }
    }
}