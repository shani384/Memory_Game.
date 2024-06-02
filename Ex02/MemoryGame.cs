using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class MemoryGame
    {
        Board m_board;
        Player player1;
        Player player2;

        public void initializeGame(UserInputTransferor i_userInput)
        {
            m_board = new Board(i_userInput.BoardLength, i_userInput.BoardWidth);
            player1 = new Player(i_userInput.Player1Name, true);
            if (i_userInput.IsPlayer2Humen)
            {
                player2 = new Player(i_userInput.Player2Name, true);
            }
            else
            {
                player2 = new Player(null, false);
            }
        }
        public bool isGameOver()
        {
            return m_board.IsAllCardsOpen();
        }
    }
}
