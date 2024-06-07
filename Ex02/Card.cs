
namespace Ex02
{
    internal class Card
    {
        char m_Content;
        bool m_IsHidden;
        public Card(char i_Content)
        {
            m_Content = i_Content;
            m_IsHidden = true;
        }
        public CardDTO GetCardDTO()
        {
            return new CardDTO(m_Content, m_IsHidden);
        }
        public char Content
        {
            get
            {
                return m_Content;
            }
        }
        public bool IsHidden
        {
            get
            {
                return m_IsHidden;
            }
            set
            {
                m_IsHidden = value;
            }
        }
    }
}
