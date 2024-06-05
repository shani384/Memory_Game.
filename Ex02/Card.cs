using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    internal class Card
    {
        char m_content;
        bool m_isHidden;
        public Card(char content)
        {
            m_content = content;
            m_isHidden = true;
        }
        public CardDTO getCardDTO()
        {
            return new CardDTO(m_content, m_isHidden);
        }
        public char Content
        {
            get
            {
                return m_content;
            }
        }
        public bool IsHidden
        {
            get
            {
                return m_isHidden;
            }
            set
            {
                m_isHidden = value;
            }
        }
    }
}
