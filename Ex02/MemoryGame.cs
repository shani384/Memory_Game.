using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class MemoryGame
    {
        private Board m_board;
        private Player player1;
        private Player player2;
       

        public MemoryGame(UserInputDTO i_userInput)
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

        public bool IsGameOver()
        {
            return m_board.IsAllCardsOpen();
        }
        public BoardDTO getCurrentStatusOfBoard()
        {
            return m_board.getBoardDTO();
        }
    }
}
