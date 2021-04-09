using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.BL;
using System.Collections.Generic;

namespace SudokuSolver.BLTests
{
    [TestClass]
    public class FieldsContainerTests
    {
        [TestMethod]
        public void NewFieldsContainer()
        {
            var actual = new FieldsContainer();
            var expectedIsDone = false;
            var expectedFields = new List<Field>();
            var expectedValuesToSet = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.AreEqual(expectedIsDone, actual.IsDone);
            CollectionAssert.AreEqual(expectedFields, actual.Fields);
            CollectionAssert.AreEqual(expectedValuesToSet, actual.ValueToSet);
        }
    }
}
