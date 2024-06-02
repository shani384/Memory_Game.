﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    internal class Board
    {
        private Card[,] m_matrix;
        private int m_numOfCardsOpen;
        private struct Card
        {
            char m_content;
            bool m_isHidden;
            public Card(char content)
            {
                m_content = content;
                m_isHidden = true;
            }
        }

        public Board(int i_length, int i_width)
        {
            m_matrix = new Card[i_length, i_width];
            m_numOfCardsOpen = 0;
            initiateBoard();
       }
        private void initiateBoard()
        {
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

            while (boardSize > 1) // Shuff cards
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
    }
}

