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
        private struct Card
        {
            char m_content;
            bool m_isHidden;
            Card(char content)
            {
                m_content = content;
                m_isHidden = true;
            }
        }
        public Board(int i_length, int i_width)
        {
            m_matrix = new Card[i_length, i_width];
            //לגנרט זוגות של קלפים על הלוח
            
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
