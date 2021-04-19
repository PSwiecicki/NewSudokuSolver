using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    static class FieldExtension
    {
        public static Field AddContainer(this Field field, FieldsContainer container)
        {
            field.ContainersWithThatField.Add(container);
            return field;
        }
    }
}
