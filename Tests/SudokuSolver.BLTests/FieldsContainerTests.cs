using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.BL;
using System.Collections.Generic;

namespace SudokuSolver.BLTests
{
    [TestClass]
    public class FieldsContainerTests
    {
        [TestMethod]
        public void NewFieldsContainerTest()
        {
            var actual = new FieldsContainer();
            var expectedIsDone = false;
            var expectedFields = 9;
            var expectedValuesToSet = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.AreEqual(expectedIsDone, actual.IsDone);
            Assert.AreEqual(expectedFields, actual.Fields.Count);
            CollectionAssert.AreEqual(expectedValuesToSet, actual.ValueToSet);
        }

        [TestMethod]
        public void CleerValuesToSetTest()
        {
            var actual = new FieldsContainer();
            var expected = new List<int>() { 1, 2, 4, 5, 7, 8, 9 };

            actual.SetField(0, 3);
            actual.SetField(1, 6);
            actual.ClearPossibilities();

            CollectionAssert.AreEqual(expected, actual.ValueToSet);
            CollectionAssert.AreEqual(expected, actual.Fields[2].PossibleValues);
        }

        [TestMethod]
        public void ComplitedContainerTest()
        {
            var actual = new FieldsContainer();
            
            for(int i = 0; i < 9; i++)
                actual.Fields[i].Value = i + 1;
            actual.ClearPossibilities();

            Assert.IsTrue(actual.IsDone);
        }

        [TestMethod]
        public void SetFieldsTest()
        {
            var actual = new FieldsContainer();

            actual.Fields[1].Value = 1;
            actual.Fields[5].Value = 5;

            Assert.AreEqual(1, actual.Fields[1].Value);
            Assert.AreEqual(5, actual.Fields[5].Value);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var actual = new FieldsContainer();
            var expected = "|1|0|3|0|0|0|0|0|9|";

            actual.Fields[0].Value = 1;
            actual.Fields[2].Value = 3;
            actual.Fields[8].Value = 9;

            Assert.AreEqual(expected, actual.ToString());
        }
        
    }
}
