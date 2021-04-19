using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void EasySudokuSolveTest()
        {
            Sudoku actual = new Sudoku();
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

    }
}
