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
        private bool m_isGameOver;
        public bool IsGameOver
        {
            get
            {
                return m_board;
            }
            set
            {
                m_isGameOver = value;
            }
        }

        public void MemoryGame(UserInputTransferor i_userInput)
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
            isGameOver = false;
        }

        public bool CheckIfGameOver()
        {
            return m_board.IsAllCardsOpen();
        }
    }
}
