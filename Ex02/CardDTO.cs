

namespace Ex02
{
    public class CardDTO
    {
        private char m_Content;
        private bool m_IsHidden;
        public CardDTO(char i_Content, bool i_IsHidden)
        {
            m_Content = i_Content;
            m_IsHidden = i_IsHidden;
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
