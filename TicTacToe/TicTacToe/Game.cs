using System;

namespace TicTacToe
{
    class Game
    {
        private Board board;
        private bool playerTurn; // true - Player X, false - Computer O

        public Game()
        {
            board = new Board();
            playerTurn = true; // Rozpoczęcie gry od gracza X
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");

            while (true)
            {
                board.Draw();

                int row, col;
                if (playerTurn)
                {
                    Console.WriteLine("Player's turn (X)");
                    (row, col) = GetPlayerMove();
                }
                else
                {
                    Console.WriteLine("Computer's turn (O)");
                    (row, col) = GetComputerMove();
                }

                if (board.IsValidMove(row, col))
                {
                    char symbol = playerTurn ? 'X' : 'O';
                    board.PlaceMove(row, col, symbol);

                    if (CheckWin(symbol))
                    {
                        board.Draw();
                        Console.WriteLine((playerTurn ? "Player" : "Computer") + " wins!");
                        break;
                    }

                    if (board.IsFull())
                    {
                        board.Draw();
                        Console.WriteLine("It's a draw!");
                        break;
                    }

                    playerTurn = !playerTurn; // Przełącz na następnego gracza
                }
                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private (int, int) GetPlayerMove()
        {
            Console.Write("Enter row (1-3): ");
            int row = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Enter column (1-3): ");
            int col = int.Parse(Console.ReadLine()) - 1;
            return (row, col);
        }

        private (int, int) GetComputerMove()
        {
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (!board.IsValidMove(row, col));
            return (row, col);
        }

        private bool CheckWin(char symbol)
        {
            char[,] state = board.GetBoardState();
            // Sprawdzenie poziomo, pionowo i na ukos
            for (int i = 0; i < 3; i++)
            {
                if (state[i, 0] == symbol && state[i, 1] == symbol && state[i, 2] == symbol)
                    return true;
                if (state[0, i] == symbol && state[1, i] == symbol && state[2, i] == symbol)
                    return true;
            }
            if (state[0, 0] == symbol && state[1, 1] == symbol && state[2, 2] == symbol)
                return true;
            if (state[0, 2] == symbol && state[1, 1] == symbol && state[2, 0] == symbol)
                return true;
            return false;
        }
    }
}
