﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.BL;
using System.Collections.Generic;

namespace SudokuSolver.BLTests
{
    [TestClass]
    public class SudokuTests
    {
        [TestMethod]
        public void ToStringTest()
        {
            Sudoku actual = new ();
            int[,] table = new int[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {4, 5, 6, 7, 8, 9, 1, 2, 3 },
                {7, 8, 9, 1, 2, 3, 4, 5, 6 },
                {2, 3, 4, 5, 6, 7, 8, 9, 1 },
                {5, 6, 7, 8, 9, 1, 2, 3, 4 },
                {8, 9, 1, 2, 3, 4, 5, 6, 7 },
                {3, 4, 5, 6, 7, 8, 9, 1, 2 },
                {6, 7, 8, 9, 1, 2, 3, 4, 5 },
                {9, 1, 2, 3, 4, 5, 6, 7, 8 }
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|2|3|4|5|6|7|8|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|5|6|7|8|9|1|2|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|8|9|1|2|3|4|5|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|3|4|5|6|7|8|9|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|6|7|8|9|1|2|3|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|9|1|2|3|4|5|6|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|4|5|6|7|8|9|1|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|7|8|9|1|2|3|4|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|1|2|3|4|5|6|7|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);

            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void SudokuIsDoneTest()
        {
            Sudoku actual = new ();
            int[,] table = new int[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {4, 5, 6, 7, 8, 9, 1, 2, 3 },
                {7, 8, 9, 1, 2, 3, 4, 5, 6 },
                {2, 3, 4, 5, 6, 7, 8, 9, 1 },
                {5, 6, 7, 8, 9, 1, 2, 3, 4 },
                {8, 9, 1, 2, 3, 4, 5, 6, 7 },
                {3, 4, 5, 6, 7, 8, 9, 1, 2 },
                {6, 7, 8, 9, 1, 2, 3, 4, 5 },
                {9, 1, 2, 3, 4, 5, 6, 7, 8 }
            };

            actual.InsertData(table);

            Assert.IsTrue(actual.IsDone);
        }

        [TestMethod]
        public void EasySudokuSolveTest()
        {
            Sudoku actual = new ();
            int[,] table = new int[,]
            {
                {0, 0, 0, 0, 5, 0, 1, 0, 7 },
                {9, 0, 0, 0, 8, 6, 3, 4, 0 },
                {4, 2, 7, 0, 0, 9, 0, 6, 8 },
                {0, 0, 8, 2, 6, 0, 0, 0, 0 },
                {7, 6, 4, 0, 3, 0, 2, 0, 9 },
                {0, 3, 2, 0, 0, 4, 6, 8, 0 },
                {8, 7, 1, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 5, 1 },
                {2, 0, 0, 0, 0, 8, 4, 7, 3 }
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|8|3|4|5|2|1|9|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|1|5|7|8|6|3|4|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|2|7|3|1|9|5|6|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|9|8|2|6|1|7|3|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|6|4|8|3|5|2|1|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|3|2|9|7|4|6|8|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|7|1|5|4|3|9|2|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|4|9|6|2|7|8|5|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|5|6|1|9|8|4|7|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);

            System.Console.WriteLine(expected);
            System.Console.WriteLine(actual.ToString());
            System.Console.WriteLine(actual.ToExtendedString());

            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void MediumSudokuSolveTest()
        {
            Sudoku actual = new();
            int[,] table = new int[,]
            {
                { 0, 8, 0, 5, 0, 3, 0, 7, 0},
                { 2, 0, 7, 0, 0, 4, 0, 0, 0},
                { 6, 1, 3, 8, 0, 2, 0, 0, 0},
                { 0, 0, 2, 4, 6, 9, 0, 0, 0},
                { 0, 0, 0, 0, 8, 0, 0, 9, 0},
                { 0, 0, 6, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 2, 0, 0, 1, 5},
                { 4, 2, 9, 0, 0, 0, 0, 0, 3},
                { 0, 0, 1, 0, 0, 7, 9, 2, 0}
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|8|4|5|1|3|6|7|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|5|7|6|9|4|8|3|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|1|3|8|7|2|5|4|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|3|2|4|6|9|1|5|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|4|5|2|8|1|3|9|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|9|6|7|3|5|2|8|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|7|8|9|2|6|4|1|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|2|9|1|5|8|7|6|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|6|1|3|4|7|9|2|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);

            System.Console.WriteLine(expected);
            System.Console.WriteLine(actual.ToString());
            System.Console.WriteLine(actual.ToExtendedString());

            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void HardSudokuSolveTest()
        {
            Sudoku actual = new();
            int[,] table = new int[,]
            {
                { 8, 0, 1, 0, 0, 0, 0, 4, 5},
                { 0, 0, 0, 0, 0, 0, 7, 0, 6},
                { 0, 5, 6, 0, 0, 0, 8, 0, 0},
                { 0, 9, 0, 7, 0, 0, 1, 0, 0},
                { 0, 0, 0, 0, 8, 0, 0, 0, 0},
                { 0, 0, 0, 2, 0, 0, 5, 3, 8},
                { 0, 0, 0, 0, 4, 0, 0, 8, 0},
                { 4, 2, 7, 0, 0, 0, 0, 1, 0},
                { 0, 0, 0, 0, 9, 0, 0, 0, 4}
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|7|1|9|2|6|3|4|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|4|9|8|5|1|7|2|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|5|6|4|7|3|8|9|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|9|8|7|3|4|1|6|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|3|2|6|8|5|4|7|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|6|4|2|1|9|5|3|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|1|5|3|4|2|6|8|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|2|7|5|6|8|9|1|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|8|3|1|9|7|2|5|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);
            actual.Solve();

            System.Console.WriteLine(expected);
            System.Console.WriteLine(actual.ToString());
            System.Console.WriteLine(actual.ToExtendedString());

            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void ExpertSudokuSolveTest()
        {
            Sudoku actual = new();
            int[,] table = new int[,]
            {
                { 4, 0, 5, 7, 0, 0, 0, 0, 0 },
                { 9, 2, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 1, 5, 8 },
                { 0, 0, 0, 0, 0, 0, 0, 6, 9 },
                { 0, 8, 0, 0, 0, 6, 7, 0, 0 },
                { 0, 9, 0, 0, 0, 0, 0, 0, 1 },
                { 6, 0, 0, 0, 9, 0, 0, 0, 3 },
                { 0, 0, 0, 0, 0, 7, 6, 0, 0 },
                { 5, 0, 0, 1, 0, 0, 0, 0, 2 }
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|1|5|7|3|8|9|2|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|2|8|6|5|1|3|4|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|6|3|2|4|9|1|5|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|5|7|3|8|2|4|6|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|8|4|9|1|6|7|3|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|9|6|4|7|5|2|8|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|7|2|8|9|4|5|1|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|3|1|5|2|7|6|9|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|4|9|1|6|3|8|7|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);
            actual.Solve();

            System.Console.WriteLine(expected);
            System.Console.WriteLine(actual.ToString());
            System.Console.WriteLine(actual.ToExtendedString());

            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        public void TheWorldsHardestSudokuSolveTest()
        {
            Sudoku actual = new();
            int[,] table = new int[,]
            {
                { 8, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 3, 6, 0, 0, 0, 0, 0 },
                { 0, 7, 0, 0, 9, 0, 2, 0, 0 },
                { 0, 5, 0, 0, 0, 7, 0, 0, 0 },
                { 0, 0, 0, 0, 4, 5, 7, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 3, 0 },
                { 0, 0, 1, 0, 0, 0, 0, 6, 8 },
                { 0, 0, 8, 5, 0, 0, 0, 1, 0 },
                { 0, 9, 0, 0, 0, 0, 4, 0, 0 }
            };
            var expected = "+-+-+-+-+-+-+-+-+-+\n" +
                           "|8|1|2|7|5|3|6|4|9|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|9|4|3|6|8|2|1|7|5|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|6|7|5|4|9|1|2|8|3|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|1|5|4|2|3|7|8|9|6|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|3|6|9|8|4|5|7|2|1|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|2|8|7|1|6|9|5|3|4|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|5|2|1|9|7|4|3|6|8|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|4|3|8|5|2|6|9|1|7|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n" +
                           "|7|9|6|3|1|8|4|5|2|" +
                           "\n+-+-+-+-+-+-+-+-+-+\n";

            actual.InsertData(table);
            actual.Solve();

            System.Console.WriteLine(expected);
            System.Console.WriteLine(actual.ToString());
            System.Console.WriteLine(actual.ToExtendedString());

            Assert.AreEqual(expected, actual.ToString());
        }
    }
}
