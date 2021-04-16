﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.BL;

namespace SudokuSolver.BLTests
{
    [TestClass]
    public class SudokuTests
    {
        [TestMethod]
        public void ToStringTest()
        {
            Sudoku actual = new Sudoku();
            int[,] table = new int[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {2, 3, 4, 5, 6, 7, 8, 9, 1 },
                {3, 4, 5, 6, 7, 8, 9, 1, 2 },
                {4, 5, 6, 7, 8, 9, 1, 2, 3 },
                {5, 6, 7, 8, 9, 1, 2, 3, 4 },
                {6, 7, 8, 9, 1, 2, 3, 4, 5 },
                {7, 8, 9, 1, 2, 3, 4, 5, 6 },
                {8, 9, 1, 2, 3, 4, 5, 6, 7 },
                {9, 1, 2, 3, 4, 5, 6, 7, 8 }
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|2|3|4|5|6|7|8|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|3|4|5|6|7|8|9|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|4|5|6|7|8|9|1|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|5|6|7|8|9|1|2|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|6|7|8|9|1|2|3|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|7|8|9|1|2|3|4|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|8|9|1|2|3|4|5|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|9|1|2|3|4|5|6|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|1|2|3|4|5|6|7|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);

            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void SudokuIsDoneTest()
        {
            Sudoku actual = new Sudoku();
            int[,] table = new int[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {2, 3, 4, 5, 6, 7, 8, 9, 1 },
                {3, 4, 5, 6, 7, 8, 9, 1, 2 },
                {4, 5, 6, 7, 8, 9, 1, 2, 3 },
                {5, 6, 7, 8, 9, 1, 2, 3, 4 },
                {6, 7, 8, 9, 1, 2, 3, 4, 5 },
                {7, 8, 9, 1, 2, 3, 4, 5, 6 },
                {8, 9, 1, 2, 3, 4, 5, 6, 7 },
                {9, 1, 2, 3, 4, 5, 6, 7, 8 }
            };

            actual.InsertData(table);

            Assert.IsTrue(actual.IsDone);
        }
    }
}