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

        [TestMethod]
        public void CleerValuesToSetTests()
        {
            var actual = new FieldsContainer();
            var expected = new List<int>() { 1, 2, 4, 5, 7, 8, 9 };

            actual.Fields.Add(new Field(3));
            actual.Fields.Add(new Field(6));
            actual.Fields.Add(new Field());
            actual.ClearPossibilities();

            CollectionAssert.AreEqual(expected, actual.ValueToSet);
            CollectionAssert.AreEqual(expected, actual.Fields[2].PossibleValues);
        }

        [TestMethod]
        public void ComplitedContainer()
        {
            var actual = new FieldsContainer();

            for (int i = 1; i <= 9; i++)
                actual.Fields.Add(new Field(i));
            actual.ClearPossibilities();

            Assert.IsTrue(actual.IsDone);
        }
    }
}
