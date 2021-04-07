using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.BL;
using System;
using System.Collections.Generic;

namespace SudokuSolver.BLTests
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void NewEmptyFieldTest()
        {
            var actual = new Field();
            var exceptedValue = 0;
            var exceptedIsSet = false;
            var exceptedPossibilities = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.AreEqual(exceptedValue, actual.Value);
            Assert.AreEqual(exceptedIsSet, actual.IsSet);
            CollectionAssert.AreEqual(exceptedPossibilities, actual.PossibleValues);
        }

        [TestMethod]
        public void NewSettedFieldTest()
        {
            var actual = new Field(8);
            var exceptedValue = 8;
            var exceptedIsSet = true;
            List<int> exceptedPossibilities = null;

            Assert.AreEqual(exceptedValue, actual.Value);
            Assert.AreEqual(exceptedIsSet, actual.IsSet);
            CollectionAssert.AreEqual(exceptedPossibilities, actual.PossibleValues);
        }

        [TestMethod]
        public void EmptyFieldAfterSetTest()
        {
            var actual = new Field();
            var exceptedValue = 2;
            var exceptedIsSet = true;
            List<int> exceptedPossibilities = null;

            actual.Value = 2;

            Assert.AreEqual(exceptedValue, actual.Value);
            Assert.AreEqual(exceptedIsSet, actual.IsSet);
            CollectionAssert.AreEqual(exceptedPossibilities, actual.PossibleValues);
        }

        [TestMethod]
        public void EmptyFieldAfterRemovePossibility()
        {
            var actual = new Field();
            var exceptedValue = 0;
            var exceptedIsSet = false;
            var exceptedPossibilities = new List<int>() { 2, 4, 5, 6, 7, 8, 9};

            actual.RemovePossibility(1);
            actual.RemovePossibility(3);

            Assert.AreEqual(exceptedValue, actual.Value);
            Assert.AreEqual(exceptedIsSet, actual.IsSet);
            CollectionAssert.AreEqual(exceptedPossibilities, actual.PossibleValues);
        }

        [TestMethod]
        public void WrongValueTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Field(10));          
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var field = new Field(8);

            Assert.ThrowsException<InvalidOperationException>(() => field.RemovePossibility(4));
        }
    }
}
