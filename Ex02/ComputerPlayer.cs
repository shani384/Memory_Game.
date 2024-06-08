using System;
using System.Collections.Generic;
using System.Linq;


namespace Ex02
{
    internal class ComputerPlayer
    {
        private List<CardCoordinate> m_CardToRandom;
        private IDictionary<char, CardCoordinate> m_RevealedCards;
        private IDictionary<char, CardCoordinate[]> m_ReadyCardsPairs;

        public ComputerPlayer(Board i_Board)
        {
            int numOfRowsBoard = i_Board.GetNumOfRows();
            int numOfColumnsBoard = i_Board.GetNumOfColumns();

            m_RevealedCards = new Dictionary<char, CardCoordinate>();
            m_ReadyCardsPairs = new Dictionary<char, CardCoordinate[]>();
            m_CardToRandom = new List<CardCoordinate>(numOfRowsBoard * numOfColumnsBoard);
        }
        public void InitiateComputerPlayer(Board i_Board)
        {
            int numOfRowsBoard = i_Board.GetNumOfRows();
            int numOfColumnsBoard = i_Board.GetNumOfColumns();

            m_RevealedCards.Clear();
            m_ReadyCardsPairs.Clear();
            m_CardToRandom.Clear();
            for (int i = 0; i < numOfRowsBoard; i++)
            {
                for (int j = 0; j < numOfColumnsBoard; j++)
                {
                    m_CardToRandom.Add(new CardCoordinate(i, j));
                }
            }
        }
        public void UpdateComputerMemory(int[] i_CardsChoice, Board i_Board, bool i_ContentsOfCardsAreEqual)
        {
            Card card1, card2;
            CardCoordinate coordinate1, coordinate2;

            coordinate1 = new CardCoordinate(i_CardsChoice[0], i_CardsChoice[1]);
            coordinate2 = new CardCoordinate(i_CardsChoice[2], i_CardsChoice[3]);
            card1 = i_Board.GetCard(coordinate1.Row.GetValueOrDefault(-1),coordinate1.Column.GetValueOrDefault(-1));
            card2 = i_Board.GetCard(coordinate2.Row.GetValueOrDefault(-1), coordinate2.Column.GetValueOrDefault(-1));
            m_CardToRandom.Remove(coordinate1);
            m_CardToRandom.Remove(coordinate2);
            if (i_ContentsOfCardsAreEqual)
            {
                takeOutUsedCardsFromMemory(card1, card2, coordinate1, coordinate2);
            }
            else
            {
                rememberOtherPlayerOpenCards(card1, card2, coordinate1, coordinate2);
            }
        }
        private void takeOutUsedCardsFromMemory(Card i_Card1, Card i_Card2, CardCoordinate i_Coordinate1, CardCoordinate i_Coordinate2)
        {
            //CardCoordinate checkIfRevealedCoordinate;
            m_RevealedCards.Remove(i_Card1.Content);
            m_ReadyCardsPairs.Remove(i_Card1.Content);

            //if (m_RevealedCards.TryGetValue(i_Card1.Content, out checkIfRevealedCoordinate) && isTheSameCard(i_Coordinate1, checkIfRevealedCoordinate))
            //{
            //    m_RevealedCards.Remove(i_Card1.Content);
            //}
            //if (m_RevealedCards.TryGetValue(i_Card2.Content, out checkIfRevealedCoordinate) && isTheSameCard(i_Coordinate2, checkIfRevealedCoordinate))
            //{
            //    m_RevealedCards.Remove(i_Card2.Content);
            //}
        }
        private void rememberOtherPlayerOpenCards(Card i_Card1, Card i_Card2, CardCoordinate i_Coordinate1, CardCoordinate i_Coordinate2)
        {
            CardCoordinate checkIfRevealedCoordinate;
            CardCoordinate[] checkIfAllreadyFound = new CardCoordinate[2];
            CardCoordinate[] newPairOfEqualCards = new CardCoordinate[2];

            if (!m_RevealedCards.TryGetValue(i_Card1.Content, out checkIfRevealedCoordinate) && !m_ReadyCardsPairs.TryGetValue(i_Card1.Content, out checkIfAllreadyFound))
            {
                m_RevealedCards[i_Card1.Content] = i_Coordinate1;
            }
            else if (!isTheSameCard(i_Coordinate1, checkIfRevealedCoordinate) && !m_ReadyCardsPairs.TryGetValue(i_Card1.Content, out checkIfAllreadyFound))
            {
                m_RevealedCards.Remove(i_Card1.Content);
                newPairOfEqualCards[0] = i_Coordinate1;
                newPairOfEqualCards[1] = checkIfRevealedCoordinate;
                m_ReadyCardsPairs[i_Card1.Content] = newPairOfEqualCards;
            }

            if (!m_RevealedCards.TryGetValue(i_Card2.Content, out checkIfRevealedCoordinate) && !m_ReadyCardsPairs.TryGetValue(i_Card2.Content, out checkIfAllreadyFound))
            {
                m_RevealedCards[i_Card2.Content] = i_Coordinate2;
            }
            else if (!isTheSameCard(i_Coordinate2, checkIfRevealedCoordinate) && !m_ReadyCardsPairs.TryGetValue(i_Card2.Content, out checkIfAllreadyFound))
            {
                m_RevealedCards.Remove(i_Card2.Content);
                newPairOfEqualCards[0] = i_Coordinate2;
                newPairOfEqualCards[1] = checkIfRevealedCoordinate;
                m_ReadyCardsPairs[i_Card2.Content] = newPairOfEqualCards;
            }

        }
        public CardCoordinate[] GetComputerCardsChoice(Board i_Board)
        {
            bool pairOfCardsFound = false;
            CardCoordinate[] cardsChoise = new CardCoordinate[2];

            while (m_ReadyCardsPairs.Count > 0 && !pairOfCardsFound)
            {
                cardsChoise = getCardsFromReadyCards(i_Board, out pairOfCardsFound);
            }
            if (!pairOfCardsFound)
            {
                cardsChoise = randomOneOrTwoCards(i_Board);
            }

            return cardsChoise;
        }
        private CardCoordinate[] getCardsFromReadyCards(Board i_Board, out bool o_PairOfCardsFound)
        {
            CardCoordinate[] cardsChoise = new CardCoordinate[2];
            Card newRevealedCard1;
            o_PairOfCardsFound = false; 

            cardsChoise = m_ReadyCardsPairs.Values.First();
            m_ReadyCardsPairs.Remove(m_ReadyCardsPairs.First().Key);
            newRevealedCard1 = i_Board.GetCard(cardsChoise[0].Row.GetValueOrDefault(-1), cardsChoise[0].Column.GetValueOrDefault(-1));
            if (newRevealedCard1.IsHidden)
            {
                o_PairOfCardsFound = true;
            }

            return cardsChoise;
        }
        private CardCoordinate[] randomOneOrTwoCards(Board i_Board)
        {
            Random random = new Random();
            int index;
            Card newRevealedCard1 = null, newRevealedCard2 = null;
            CardCoordinate checkIfRevealedCoordinate = new CardCoordinate();
            CardCoordinate[] cardsChoise = new CardCoordinate[2];
            CardCoordinate[] newPairOfEqualCards = new CardCoordinate[2];
            CardCoordinate[] checkIfAllreadyFound = new CardCoordinate[2];

            index = random.Next(m_CardToRandom.Count);
            cardsChoise[0] = m_CardToRandom[index];
            m_CardToRandom.Remove(m_CardToRandom[index]);
            newRevealedCard1 = i_Board.GetCard(cardsChoise[0].Row.GetValueOrDefault(-1), cardsChoise[0].Column.GetValueOrDefault(-1));
            if (m_RevealedCards.TryGetValue(newRevealedCard1.Content, out checkIfRevealedCoordinate) && !isTheSameCard(cardsChoise[0], checkIfRevealedCoordinate))
            {
                cardsChoise[1] = checkIfRevealedCoordinate;
                m_RevealedCards.Remove(newRevealedCard1.Content);
            }
            else
            {
                index = random.Next(m_CardToRandom.Count);
                cardsChoise[1] = m_CardToRandom[index];
                m_CardToRandom.Remove(m_CardToRandom[index]);
                newRevealedCard2 = i_Board.GetCard(cardsChoise[1].Row.GetValueOrDefault(-1), cardsChoise[1].Column.GetValueOrDefault(-1));
                if (newRevealedCard1.Content != newRevealedCard2.Content)
                {
                    if (m_RevealedCards.TryGetValue(newRevealedCard2.Content, out checkIfRevealedCoordinate) && !m_ReadyCardsPairs.TryGetValue(newRevealedCard2.Content, out checkIfAllreadyFound))
                    {
                        newPairOfEqualCards[0] = cardsChoise[1];
                        newPairOfEqualCards[1] = checkIfRevealedCoordinate;
                        m_ReadyCardsPairs[newRevealedCard2.Content] = newPairOfEqualCards;
                        m_RevealedCards.Remove(newRevealedCard2.Content);
                    }
                    else
                    {
                        m_RevealedCards[newRevealedCard2.Content] = cardsChoise[1];
                    }
                    m_RevealedCards[newRevealedCard1.Content] = cardsChoise[0];
                }
            }

            return cardsChoise;
        }
        private bool isTheSameCard(CardCoordinate i_Card1, CardCoordinate i_Card2)
        {
            return i_Card1.Row == i_Card2.Row && i_Card1.Column == i_Card2.Column;
        }
    }
}
