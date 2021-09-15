using System;
using Xunit;
using System.IO;
using FluentAssertions;

namespace TicTacTotallyAwesome.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("X__" +
                    "_X_" +
                    "__X")]
        [InlineData("__X" +
                    "_X_" +
                    "X__")]
        public void CalculateWinner_DiagonalWinner_ExpectWinnerEvaluation(string inputData)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(inputData);
            Console.SetIn(input);

            TicTacToe.Main(new string[] { "" });

            output.ToString().TrimEnd().Should().Be("X");
        }

        [Theory]
        [InlineData("XXX" +
                    "XX_" +
                    "___")]
        [InlineData("O__" +
                    "XXX" +
                    "__X")]
        [InlineData("O__" +
                    "_X_" +
                    "XXX")]
        public void CalculateWinner_RowWinner_ExpectWinnerEvaluation(string inputData)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(inputData);
            Console.SetIn(input);

            TicTacToe.Main(new string[] { "" });

            output.ToString().TrimEnd().Should().Be("X");
        }

        [Theory]
        [InlineData("O__" +
                    "OXX" +
                    "O_X")]
        [InlineData("_O_" +
                    "_OX" +
                    "OOX")]
        [InlineData("O_O" +
                    "OXO" +
                    "O_O")]
        public void MiddleCellNotEmpty_ColumnWinner_ExpectWinnerEvaluation(string inputData)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(inputData);
            Console.SetIn(input);

            TicTacToe.Main(new string[] {""});

            output.ToString().TrimEnd().Should().Be("O");
        }

        [Fact]
        public void MiddleCellEmpty_ColumnWinner_ExpectWinnerEvaluation()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("O__" +
                                         "O_X" +
                                         "O_X");
            Console.SetIn(input);

            TicTacToe.Main(new string[] { "" });

            output.ToString().TrimEnd().Should().Be("O");
        }

        [Fact]
        public void AllCellsFull_NoWinner_ExpectTiedGame()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("OXO" +
                                         "OXX" +
                                         "XOX");
            Console.SetIn(input);

            TicTacToe.Main(new string[] { "" });

            output.ToString().TrimEnd().Should().Be("TIED");
        }

        [Fact]
        public void CellEmpty_NoWinner_ExpectGameInProgress()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("OX_" +
                                         "OXX" +
                                         "XOX");
            Console.SetIn(input);

            TicTacToe.Main(new string[] { "" });

            output.ToString().TrimEnd().Should().Be("IN_PROGRESS");
        }

        [Fact]
        public void AllEmptyCells_NoWinner_ExpectGameInProgress()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("___" +
                                         "___" +
                                         "___");
            Console.SetIn(input);

            TicTacToe.Main(new string[] { "" });

            output.ToString().TrimEnd().Should().Be("IN_PROGRESS");
        }
    }
}