using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace TicTacTotallyAwesome
{
    public enum CellState
    {
        [Description("EMPTY")]
        Empty,
        [Description("X")]
        X,
        [Description("O")]
        O
    }

    public enum Winner
    {
        [Description("IN_PROGRESS")]
        InProgress,
        [Description("TIED")]
        Tied,
        [Description("X")]
        X,
        [Description("O")]
        O
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }

    public class TicTacToe
    {

        /**
         * Evaluates the game board to determine the winner.
         * <p>
         * If the game is still in progress, then this method returns InProgress.
         * If the game is tied, then this method returns Tied.
         * Otherwise, returns the winner - either X or O.
         *
         * @param cells - an array of 9 game cells that forms the 3x3 grid of the board.
         * @return the Winner - InProgress, Tied, X, or O
         */
        public static Winner CalculateWinner(CellState[] cells)
        {
            if (cells[4] != CellState.Empty)
            {
                return DiagonalWinner(cells);
            }
            else 
            {
                return RowWinner(cells);
            }
        }

        private static Winner DiagonalWinner(CellState[] cells)
        {
            var referenceState = cells[4];
            if (cells[0] == referenceState && cells[8] == referenceState 
                || cells[2] == referenceState && cells[6] == referenceState)
            {
                return WinnerXorO(referenceState);
            }
            return RowWinner(cells);
        }

        private static Winner ColumnWinner(CellState[] cells)
        {
            for (int i = 0; i < 3; i++)
            {
                var referenceCell = cells[i];
                if (cells[i + 3] == referenceCell && cells[i + 6] == referenceCell && referenceCell != CellState.Empty)
                {
                    var winnerStatus = WinnerXorO(referenceCell);

                    return winnerStatus;
                }
            }
            return IsGameTied(cells);
        }


        private static Winner RowWinner(CellState[] cells)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (i % 3 == 0 && cells[i] == cells[i + 1] && cells[i] == cells[i + 2])
                {
                    var referenceCell = cells[i];
                    if (referenceCell == CellState.Empty)
                    {
                        return Winner.InProgress;
                    }

                    var winnerStatus = WinnerXorO(referenceCell);

                    return winnerStatus;
                }
            }
            return ColumnWinner(cells);
        }

        private static Winner WinnerXorO(CellState referenceState)
        {
            if (referenceState == CellState.X)
            {
                return Winner.X;
            }
            return Winner.O;
        }

        private static Winner IsGameTied(CellState[] cells)
        {
            bool isEmpty = false;
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == CellState.Empty && isEmpty == false)
                {
                    isEmpty = true;
                }
            }

            if (isEmpty == false)
            {
                return Winner.Tied;
            }
            return Winner.InProgress;
        }

        public static CellState[] ConvertInputLineToCellStateArray(string inputLine)
        {
            if (inputLine.Length != 9)
            {
                throw new ArgumentException("Invalid state string. Should have 9 characters.");
            }

            List<CellState> cellStates = new List<CellState>();

            foreach (char c in inputLine)
            {
                if (c == '_') cellStates.Add(CellState.Empty);
                if (c == 'X') cellStates.Add(CellState.X);
                if (c == 'O') cellStates.Add(CellState.O);
            }

            return cellStates.ToArray();
        }

        public static void Main(string[] args)
        {
            string inputLine;

            while ((inputLine = Console.ReadLine()) != null)
            {
                Console.WriteLine(CalculateWinner(ConvertInputLineToCellStateArray(inputLine)).GetDescription());
            }
        }
    }
}