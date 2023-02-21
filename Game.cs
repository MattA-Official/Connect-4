using System;

/*
A simple implementation of the Connect 4 game. https://en.wikipedia.org/wiki/Connect_Four

Specification

1. Display
    a. Get each player’s name
    b. Each player’s name should be displayed in a different colour
    c. The colour of the player should match the token colour
    d. The game board should be a 7 column by 6 row grid for tokens
    e. The game board should display a further row for column numbers (1 to 7)
    f. The game board should display the current state of the game
2. Gameplay
    a. There must be 2 players
    b. Each player must take alternating turns
    c. Each turn, the player must enter a column number
        i. The player input must be valid
        ii. A token must be placed into the lowest row of the valid selected column
    d. After a token has been placed, the board must be checked for a winner
        i. Check for 4 same coloured tokens in a row horizontally
        ii. Check for 4 same coloured tokens in a row vertically
        iii. Check for 4 same coloured tokens in a row diagonally
    e. If a winner is present, display the winning player and a success message
    f. If a winner is present, end the game
3. Technical
    a. Use the OOP paradigm
    b. Show the specification has been met through testing
*/

namespace Connect_4
{
    class Game
    {
        private Player[] players;
        private char[,] board;
        private int currentPlayer;
        public static char DisplayToken { get; set; } = 'O';

        public Game()
        {
            players = new Player[2];
            board = new char[6, 7];
            currentPlayer = 0;

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        public void AddPlayer(string name, ConsoleColor color, char token)
        {
            players[currentPlayer++] = new Player(name, color, token);
        }

        public void Start()
        {
            bool GameOver = false;

            Console.WriteLine("Welcome to Connect 4!");
            Console.WriteLine("Player 1, please enter your name:");
            AddPlayer(Console.ReadLine(), ConsoleColor.Red, '1');
            Console.WriteLine("Player 2, please enter your name:");
            AddPlayer(Console.ReadLine(), ConsoleColor.Yellow, '2');
            Console.WriteLine("Let's play!");

            currentPlayer = 0;

            while (!GameOver)
            {
                bool isFull = false;

                do
                {
                    Console.Clear();
                    DisplayBoard();

                    if (isFull)
                        Console.WriteLine("Column is full, try again.");

                    int col = GetColumnInput();

                    PlayTurn(col - 1, out isFull);
                } while (isFull);

                GameOver = CheckForWinner();

                if (!GameOver)
                    currentPlayer = (currentPlayer + 1) % 2;
            }

            DisplayWinner();
        }

        private int GetColumnInput()
        {
            // Validate input
            int col;

            Console.Write(
                $"{players[currentPlayer].Name} ({players[currentPlayer].Color}), please enter a column number: "
            );

            while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 7)
            {
                // Clear the line
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);

                Console.Write(
                    $"Invalid input, please try again {players[currentPlayer].Name} ({players[currentPlayer].Color}): "
                );
            }

            return col;
        }

        private void DisplayWinner()
        {
            Console.Clear();
            DisplayBoard();
            Console.WriteLine(
                $"{players[currentPlayer].Name} ({players[currentPlayer].Color}) wins!"
            );
        }

        private void DisplayBoard()
        {
            Console.WriteLine(" 1 2 3 4 5 6 7");
            for (int row = 0; row < board.GetLength(0); row++)
            {
                Console.Write("|");
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == players[0].Token)
                    {
                        Console.ForegroundColor = players[0].Color;
                        Console.Write(DisplayToken);
                        Console.ResetColor();
                    }
                    else if (board[row, col] == players[1].Token)
                    {
                        Console.ForegroundColor = players[1].Color;
                        Console.Write(DisplayToken);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(board[row, col]);
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }

        private void PlayTurn(int col, out bool isFull)
        {
            int row = 5;
            while (row >= 0 && board[row, col] != ' ')
            {
                row--;
            }

            if (row < 0)
            {
                Console.WriteLine("Column is full, try again.");
                isFull = true;
                return;
            }

            board[row, col] = players[currentPlayer].Token;
            isFull = false;
        }

        private bool CheckForWinner()
        {
            // Check for 4 same coloured tokens in a row horizontally
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (
                        board[row, col] != ' '
                        && board[row, col] == board[row, col + 1]
                        && board[row, col] == board[row, col + 2]
                        && board[row, col] == board[row, col + 3]
                    )
                    {
                        return true;
                    }
                }
            }
            // Check for 4 same coloured tokens in a row vertically
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (
                        board[row, col] != ' '
                        && board[row, col] == board[row + 1, col]
                        && board[row, col] == board[row + 2, col]
                        && board[row, col] == board[row + 3, col]
                    )
                    {
                        return true;
                    }
                }
            }
            // Check for 4 same coloured tokens in a row diagonally
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (
                        board[row, col] != ' '
                        && board[row, col] == board[row + 1, col + 1]
                        && board[row, col] == board[row + 2, col + 2]
                        && board[row, col] == board[row + 3, col + 3]
                    )
                    {
                        return true;
                    }
                }
            }
            for (int row = 3; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (
                        board[row, col] != ' '
                        && board[row, col] == board[row - 1, col + 1]
                        && board[row, col] == board[row - 2, col + 2]
                        && board[row, col] == board[row - 3, col + 3]
                    )
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
