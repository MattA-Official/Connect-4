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
    class Player
    {
        public string Name { get; set; }
        public ConsoleColor Color { get; set; }
        public char Token { get; set; }

        public Player(string name, ConsoleColor color, char token)
        {
            Name = name;
            Color = color;
            Token = token;
        }
    }
}
