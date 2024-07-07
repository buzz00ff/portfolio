using System;

namespace TicTacToe
{
    class Board
    {
        private char[,] board;

        public Board()
        {
            board = new char[3, 3];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        public void Draw()
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(" " + board[i, 0] + " | " + board[i, 1] + " | " + board[i, 2]);
                if (i < 2)
                    Console.WriteLine("---|---|---");
            }
            Console.WriteLine();
        }

        public bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ';
        }

        public void PlaceMove(int row, int col, char symbol)
        {
            board[row, col] = symbol;
        }

        public bool IsFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                        return false;
                }
            }
            return true;
        }

        public char[,] GetBoardState()
        {
            return board;
        }
    }
}
