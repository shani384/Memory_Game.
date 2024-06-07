using System;
using System.Collections.Generic;

namespace Ex02
{
    internal class Board
    {
        private Card[,] m_Matrix;
        private int m_NumOfCardsOpen;

        public Board(int i_Length, int i_Width)
        {
            m_Matrix = new Card[i_Length, i_Width];
            m_NumOfCardsOpen = 0;
       }
        public void InitiateBoard()
        {
            int boardSize = m_Matrix.Length;
            List<Card> cards = new List<Card>();
            Random rand = new Random();

            m_NumOfCardsOpen = 0;
            char c = 'A';
            for (int i = 0; i < boardSize / 2; i++)
            {
                cards.Add(new Card(c));
                cards.Add(new Card(c));
                c++;
            }

            while (boardSize > 1) // Shuffle cards
            {
                int range = boardSize - 1;
                int index = rand.Next(range);
                Card value = cards[index];
                cards[index] = cards[range];
                cards[range] = value;
                boardSize--;
            }

            int ind = 0;
            for (int i = 0; i < m_Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m_Matrix.GetLength(1); j++)
                {
                    m_Matrix[i, j] = cards[ind];
                    ind++;
                }
            }
        }
        public bool IsAllCardsOpen()
        {
            bool isAllCardsOpen;

            if (m_Matrix.Length == m_NumOfCardsOpen)
            {
                isAllCardsOpen = true;
            }
            else
            {
                isAllCardsOpen = false;
            }

            return isAllCardsOpen;
        }
        public BoardDTO GetBoardDTO()
        {
            int length = m_Matrix.GetLength(0), width = m_Matrix.GetLength(1);
            CardDTO[,] matrixDTO = new CardDTO[length, width];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrixDTO[i, j] = m_Matrix[i,j].GetCardDTO();
                }
            }

            return new BoardDTO(matrixDTO);
        }
        public bool Check2CardsAndRevealThemIfEqual(int[] i_Indexes)
        {
            bool ContentsOfCardsAreEqual;
            Card card1 = m_Matrix[i_Indexes[0], i_Indexes[1]];
            Card card2 = m_Matrix[i_Indexes[2], i_Indexes[3]];
           
            if(card1.Content == card2.Content)
            {
                card1.IsHidden = false;
                card2.IsHidden = false;
                m_NumOfCardsOpen+=2;
                ContentsOfCardsAreEqual = true;
            }
            else
            {
                ContentsOfCardsAreEqual = false;
            }

            return ContentsOfCardsAreEqual;
        }
        public char GetCardValue(int[] i_CardIndexes)
        {
            return m_Matrix[i_CardIndexes[0], i_CardIndexes[1]].Content;
        }
        public Card GetCard(int i_Row, int i_Column)
        {
            return m_Matrix[i_Row, i_Column];
        }
        public static bool IsSizeBoardEven(int i_LengthBoard, int i_WidthBoard)
        {
            return (i_LengthBoard * i_WidthBoard) % 2 == 0;
        }
        public int GetNumOfRows()
        {
            return m_Matrix.GetLength(0);
        }
        public int GetNumOfColumns()
        {
            return m_Matrix.GetLength(1);
        }
    }
    
}            


