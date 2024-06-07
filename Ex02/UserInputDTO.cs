

namespace Ex02
{
    public class UserInputDTO
    {
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly int r_BoardLength;
        private readonly int r_BoardWidth;
        private readonly bool r_IsPlayer2Humen; 

        public UserInputDTO(string i_Player1Name, string i_Player2Name, int i_BoardLength, int i_BoardWidth, bool i_IsPlayer2Humen)
        {
            r_Player1Name = i_Player1Name;
            r_Player2Name = i_Player2Name;
            r_BoardLength = i_BoardLength;
            r_BoardWidth = i_BoardWidth;
            r_IsPlayer2Humen = i_IsPlayer2Humen;
        }
        public string Player1Name
        {
            get
            {
                return r_Player1Name;
            }
        }
        public string Player2Name
        {
            get
            {
                return r_Player2Name;
            }
        }
        public int BoardLength
        {
            get
            {
                return r_BoardLength;
            }
        }
        public int BoardWidth
        {
            get
            {
                return r_BoardWidth;
            }
        }
        public bool IsPlayer2Humen
        {
            get
            {
                return r_IsPlayer2Humen;
            }
        }

    }
}
