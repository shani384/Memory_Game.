
namespace Ex02
{
    internal struct CardCoordinate
    {
        private int? m_Column;
        private int? m_Row;

        public int? Column
        {
            get
            {
                return m_Column;
            }
            set
            {
                m_Column = value;
            }
        }
        public int? Row
        {
            get
            {
                return m_Row;
            }
            set
            {
                m_Row = value;
            }
        }
        public CardCoordinate(int i_Row, int i_Column)
        {
            m_Row = i_Row;
            m_Column = i_Column;
        }
    }
}
