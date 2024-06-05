using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public struct CardCoordinate
    {
        private int m_column;
        private int m_row;

        public int Column
        {
            get
            {
                return m_column;
            }
            set
            {
                m_column = value;
            }
        }
        public int Row
        {
            get
            {
                return m_row;
            }
            set
            {
                m_row = value;
            }
        }
        public CardCoordinate(int i_row, int i_column)
        {
            m_row = i_row;
            m_column = i_column;
        }
    }
}
