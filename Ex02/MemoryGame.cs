using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class MemoryGame
    {
        private Board m_board;
        private Player m_player1;
        private Player m_player2;
        private Player m_turnPlayer;
        private ComputerPlayer m_computerPlayer;
       

        public MemoryGame(UserInputDTO i_userInput)
        {
            m_board = new Board(i_userInput.BoardLength, i_userInput.BoardWidth);
            m_player1 = new Player(i_userInput.Player1Name, true);
            if (i_userInput.IsPlayer2Humen)
            {
                m_player2 = new Player(i_userInput.Player2Name, true);
            }

            else
            {
                m_player2 = new Player(null, false);
                m_computerPlayer = new ComputerPlayer();
            }
            m_turnPlayer = m_player1;
        }

        public bool IsGameOver()
        {
            return m_board.IsAllCardsOpen();
        }
        public BoardDTO getCurrentStateOfBoard()
        {
            return m_board.getBoardDTO();
        }
        public PlayerDTO getNextTurnPlayer()
        {
            return m_turnPlayer.createPlayerDTO();
        }
        public void runCurrentTurn(ref int[] io_cardsChoice)
        {
            bool res;
            if (!m_turnPlayer.IsHumen)
            {
                io_cardsChoice = m_computerPlayer.getComputerCardsChoice();
                //todo: if return null need to random card
            }

            res = m_board.check2CardsAndRevealThemIfEqual(io_cardsChoice);
            RevealComputerPlayerNewCards(io_cardsChoice);

            if (res == true)
            {
                m_turnPlayer.Score = m_turnPlayer.Score + 1;
            }
            else
            {
                ChangeTurnPlayer();
            }
        }
        private void ChangeTurnPlayer()
        {
            if(m_turnPlayer == m_player1)
            {
                m_turnPlayer = m_player2;
            }
            else
            {
                m_turnPlayer = m_player1;
            }
        }
        public PlayerDTO[] GetPlayerDetails()
        {
            PlayerDTO[] playersDetails = new PlayerDTO[2];
            playersDetails[0] = m_player1.createPlayerDTO();
            playersDetails[1] = m_player2.createPlayerDTO();
            return playersDetails;
        }
        public void RevealComputerPlayerNewCards(int[] cards)
        {
            int[] card1Indexes = cards;
            int[] card2Indexes = cards.Skip(2).ToArray();
            char contentCard1= m_board.GetCardValue(card1Indexes);
            char contentCard2= m_board.GetCardValue(card2Indexes);

            if(contentCard1 != contentCard2)
            {
                m_computerPlayer.RevealNewCard(card1Indexes, contentCard1);
                m_computerPlayer.RevealNewCard(card2Indexes, contentCard2);
            }
            else
            {
                //remove from computer player
            }

        }
    }
}
