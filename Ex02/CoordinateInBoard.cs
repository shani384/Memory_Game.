

namespace Ex02
{
    internal struct CoordinateInBoard
    {
        private int m_column;
        private int m_row;
        
        public int Column
        {
            get
            {
                return m_column;            }
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
        public CoordinateInBoard(int i_Row ,int i_Column)
        {
            m_row = i_Row;
            m_column = i_Column;
        }
    }
}
