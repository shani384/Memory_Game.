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
            m_board = new Board();
            player1 = new Player();///לזכור לשים פה תמיד היומן
            player2 = new Player();
        }
        public bool isGameOver()
        {
            return m_board.IsAllCardsOpen();
        }
    }
}
