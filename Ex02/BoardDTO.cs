

namespace Ex02
{
    public class BoardDTO
    {
        private CardDTO[,] m_matrix;
        public BoardDTO(CardDTO[,] i_Matrix)
        {
            m_matrix = i_Matrix;
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
