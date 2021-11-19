using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BL
{
    public static class FieldExtension
    {
        public static Field AddContainer(this Field field, FieldsContainer container)
        {
            field.AddNewContainer(container);
            container.Fields.Add(field);
            return field;
        }
    }
}
