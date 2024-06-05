using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    internal class Board
    {
        private Card[,] m_matrix;
        private int m_numOfCardsOpen;
        //private class Card
        //{
        //    char m_content;
        //    bool m_isHidden;
        //    public Card(char content)
        //    {
        //        m_content = content;
        //        m_isHidden = true;
        //    }
        //    public CardDTO getCardDTO()
        //    {
        //        return new CardDTO(m_content, m_isHidden);
        //    }
        //    public char Content
        //    {
        //        get
        //        {
        //            return m_content;
        //        }
        //    }
        //    public bool IsHidden
        //    {
        //        set
        //        {
        //            m_isHidden = value;
        //        }
        //    }
        //}

        public Board(int i_length, int i_width)
        {
            m_matrix = new Card[i_length, i_width];
            m_numOfCardsOpen = 0;
       }
        public void InitiateBoard()
        {
            m_numOfCardsOpen = 0;
            int boardSize = m_matrix.Length;
            List<Card> cards = new List<Card>();
            Random rand = new Random();

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
            for (int i = 0; i < m_matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m_matrix.GetLength(1); j++)
                {
                    m_matrix[i, j] = cards[ind];
                    ind++;
                }
            }
        }
        public bool IsAllCardsOpen()
        {
            bool res;

            if (m_matrix.Length == m_numOfCardsOpen)
            {
                res = true;
            }
            else
            {
                res = false;
            }

            return res;
        }
        public BoardDTO getBoardDTO()
        {
            int length = m_matrix.GetLength(0), width = m_matrix.GetLength(1);
            CardDTO[,] matrixDTO = new CardDTO[length, width];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrixDTO[i, j] = m_matrix[i,j].getCardDTO();
                }
            }
            return new BoardDTO(matrixDTO);
        }
        public bool check2CardsAndRevealThemIfEqual(int[] i_indexes)
        {
            bool ContentsOfCardsAreEqual;

            Card card1 = m_matrix[i_indexes[0], i_indexes[1]];
            Card card2 = m_matrix[i_indexes[2], i_indexes[3]];
           
            if(card1.Content == card2.Content)
            {
                card1.IsHidden = false;
                card2.IsHidden = false;
                m_numOfCardsOpen+=2;
                ContentsOfCardsAreEqual = true;
            }
            else
            {
                ContentsOfCardsAreEqual = false;
            }
            return ContentsOfCardsAreEqual;
        }
        public char GetCardValue(int[] i_cardIndexes)
        {
            return m_matrix[i_cardIndexes[0], i_cardIndexes[1]].Content;
        }
        public Card GetCard(int i_row, int i_column)
        {
            return m_matrix[i_row, i_column];
        }
        public static bool IsSizeBoardEven(int i_lengthBoard, int i_widthBoard)
        {
            return (i_lengthBoard * i_widthBoard) % 2 == 0;
        }

        public int GetNumOfRows()
        {
            return m_matrix.GetLength(0);
        }
        public int GetNumOfColumns()
        {
            return m_matrix.GetLength(1);
        }
    }
    
}            


