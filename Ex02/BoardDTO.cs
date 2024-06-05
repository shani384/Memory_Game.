using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class BoardDTO
    {
        private CardDTO[,] m_matrix;
        public BoardDTO(CardDTO[,] i_matrix)
        {
            m_matrix = i_matrix;
        }
        public CardDTO[,] Matrix
        {
            get
            {
                return m_matrix;
            }
        }
        
    }
}
