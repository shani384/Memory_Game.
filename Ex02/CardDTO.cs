using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class CardDTO
    {
        char m_content;
        bool m_isHidden;
        public CardDTO(char i_content, bool i_isHidden)
        {
            m_content = i_content;
            m_isHidden = i_isHidden;
        }
        public char Content
        {
            get
            {
                return m_content;
            }
        }
        public bool IsHissen
        {
            get
            {
                return m_isHidden;
            }
        }
    }
}
