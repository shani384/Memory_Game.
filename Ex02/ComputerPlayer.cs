using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    internal class ComputerPlayer
    {
        private List<CardCoordinate> cardCoordinates;
        private IDictionary<Card, CardCoordinate> m_revealedCards;
        private List<CardCoordinate[]> m_readyCardsPairs;

        public ComputerPlayer(Board board)
        {
            int numOfRowsBoard = board.GetNumOfRows();
            int numOfColumnsBoard = board.GetNumOfColumns();
            m_revealedCards = new Dictionary<Card, CardCoordinate>();
            m_readyCardsPairs = new List<CardCoordinate[]>();

            cardCoordinates = new List<CardCoordinate>(numOfRowsBoard * numOfColumnsBoard);
            for (int i = 0; i < numOfRowsBoard; i++)
            { 
                for(int j = 0; j< numOfColumnsBoard; j++)
                {
                    cardCoordinates.Add(new CardCoordinate(i, j));
                }
            }
        }

        public CardCoordinate[] getComputerCardsChoice(Board board)
        {
            Random random = new Random();
            int guessRow;
            int guessColumn;
            Card randCard1 = null, randCard2 = null;
            CardCoordinate checkIfRevealedCard = new CardCoordinate();
            CardCoordinate[] cardsChoise = new CardCoordinate[2];

            if (m_readyCardsPairs.Count > 0)
            {
                cardsChoise = m_readyCardsPairs[0];
                m_readyCardsPairs.RemoveAt(0);
            }
            else
            {
                do
                {
                    guessRow = random.Next(0, board.GetNumOfRows());
                    guessColumn = random.Next(0, board.GetNumOfColumns());
                    randCard1 = board.GetCard(guessRow, guessColumn);

                } while (!randCard1.IsHidden);
                cardsChoise[0] = new CardCoordinate(guessRow, guessColumn);
                if(m_revealedCards.TryGetValue(randCard1, out checkIfRevealedCard))
                {
                    cardsChoise[1] = checkIfRevealedCard;
                    m_revealedCards.Remove(randCard1);
                }
                else
                {

                    do
                    {
                        guessRow = random.Next(0, board.GetNumOfRows());
                        guessColumn = random.Next(0, board.GetNumOfColumns());
                        randCard2 = board.GetCard(guessRow, guessColumn);

                    } while (!randCard2.IsHidden);
                    cardsChoise[1] = new CardCoordinate(guessRow, guessColumn);
                }
            }
            if(randCard1 != null && randCard2 != null)
            {
                if(randCard1.Content != randCard2.Content)
                {

                }
            }
            else if()
                {

            }
            return cardsChoise;
        }
        public void RevealNewCard(int[] indexes, char content)
        {
            if (m_revealedCards.ContainsKey(content))
            {
                if (!IsTheSameCard(m_revealedCards[content], indexes))
                {
                    m_readyCardsPairs.Add(ConcatCards(indexes, m_revealedCards[content]));
                    m_revealedCards.Remove(content);
                }
            }
            else
            {
                m_revealedCards.Add(content, indexes);
            }
        }
        private int[] ConcatCards(int[] i_card1, int[] i_card2)
        {
            int[] res = new int[4];
            res[0] = i_card1[0];
            res[1] = i_card1[1];
            res[2] = i_card2[0];
            res[3] = i_card2[1];
            return res;
        }
        private bool IsTheSameCard(int[] card1, int[] card2)
        {
            return card1[0] == card2[0] && card1[1] == card2[1];
        }
        
    }
}
