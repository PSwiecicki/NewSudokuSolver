using System;
using System.Collections.Generic;
using Xunit;

namespace SudokuSolver.BL.Tests
{
    public class FieldTest
    {
        [Fact]
        public void NewEmptyField()
        {
            var fieldUnderTest = new Field();
            var exceptedPossibilities = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.Null(fieldUnderTest.Value);
            Assert.False(fieldUnderTest.IsSet);
            Assert.Equal(exceptedPossibilities, fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void NewSettedField()
        {
            var fieldUnderTest = new Field(8);
            var exceptedValue = 8;

            Assert.Equal(exceptedValue, fieldUnderTest.Value);
            Assert.True(fieldUnderTest.IsSet);
            Assert.Null(fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void SetValueInEmptyField()
        {
            var fieldUnderTest = new Field();
            var exceptedValue = 2;

            var setValueResult = fieldUnderTest.SetValue(2);

            Assert.True(setValueResult);
            Assert.Equal(exceptedValue, fieldUnderTest.Value);
            Assert.True(fieldUnderTest.IsSet);
            Assert.Null(fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void SetFieldByLeftOnePossibility()
        {
            var fieldUnderTest = new Field();
            var exceptedValue = 9;

            fieldUnderTest.RemovePossibility(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 });

            Assert.Equal(exceptedValue, fieldUnderTest.Value);
            Assert.True(fieldUnderTest.IsSet);
            Assert.Null(fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void RemovePossibilityFromNotSettedField()
        {
            var fieldUnderTest = new Field();
            var exceptedPossibilities = new List<int>() {1, 2, 3, 5, 6, 7, 8, 9 };

            bool removeResult = fieldUnderTest.RemovePossibility(4);

            Assert.True(removeResult);
            Assert.Null(fieldUnderTest.Value);
            Assert.False(fieldUnderTest.IsSet);
            Assert.Equal(exceptedPossibilities, fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void RemovePossibilitiesFromNotSettedField()
        {
            var fieldUnderTest = new Field();
            var exceptedPossibilities = new List<int>() { 1, 3, 6, 8, 9 };
            var possibilitiesToRemove = new List<int>() { 2, 4, 5, 7 };

            bool removeResult = fieldUnderTest.RemovePossibility(possibilitiesToRemove);

            Assert.True(removeResult);
            Assert.Null(fieldUnderTest.Value);
            Assert.False(fieldUnderTest.IsSet);
            Assert.Equal(exceptedPossibilities, fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void NewFieldWithWrongValue()
        {
            var fieldUnderTest = new Field(10);
            var exceptedPossibilities = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.Null(fieldUnderTest.Value);
            Assert.False(fieldUnderTest.IsSet);
            Assert.Equal(exceptedPossibilities, fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void RemovePossibilityInSettedField()
        {
            var fieldUnderTest = new Field(8);

            Assert.False(fieldUnderTest.RemovePossibility(4));
            Assert.Null(fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void RemoveWrongPossibility()
        {
            var fieldUnderTest = new Field();
            var expectedPossibilities = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            bool removeResult = fieldUnderTest.RemovePossibility(123);

            Assert.False(removeResult);
            Assert.Null(fieldUnderTest.Value);
            Assert.False(fieldUnderTest.IsSet);
            Assert.Equal(expectedPossibilities, fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void SetWrongValue()
        {
            var fieldUnderTest = new Field();
            List<int> exceptedPossibilities = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            bool setValueResult = fieldUnderTest.SetValue(123123);

            Assert.False(setValueResult);
            Assert.Null(fieldUnderTest.Value);
            Assert.False(fieldUnderTest.IsSet);
            Assert.Equal(exceptedPossibilities, fieldUnderTest.PossibleValues);
        }

        [Fact]
        public void SettedFieldToString()
        {
            var fieldUnderTest = new Field(7);
            string expectedString = "7";

            Assert.Equal(expectedString, fieldUnderTest.ToString());
        }

        [Fact]
        public void NotSettedFieldToString()
        {
            var fieldUnderTest = new Field();
            var possibilitiesToRemove = new List<int>() { 2, 5, 8 };
            string expectedString = "1, 3, 4, 6, 7, 9";


            fieldUnderTest.RemovePossibility(possibilitiesToRemove);

            Assert.Equal(expectedString, fieldUnderTest.ToString());
        }
    }
}
