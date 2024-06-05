using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class UserInputDTO
    {
        private readonly string r_player1Name;
        private readonly string r_player2Name;
        private readonly int r_boardLength;
        private readonly int r_boardWidth;
        private readonly bool r_isPlayer2Humen; 

        public UserInputDTO(string i_player1Name, string i_player2Name, int i_boardLength, int i_boardWidth, bool i_isPlayer2Humen)
        {
            r_player1Name = i_player1Name;
            r_player2Name = i_player2Name;
            r_boardLength = i_boardLength;
            r_boardWidth = i_boardWidth;
            r_isPlayer2Humen = i_isPlayer2Humen;
        }
        public string Player1Name
        {
            get
            {
                return r_player1Name;
            }
        }
        public string Player2Name
        {
            get
            {
                return r_player2Name;
            }
        }
        public int BoardLength
        {
            get
            {
                return r_boardLength;
            }
        }
        public int BoardWidth
        {
            get
            {
                return r_boardWidth;
            }
        }
        public bool IsPlayer2Humen
        {
            get
            {
                return r_isPlayer2Humen;
            }
        }

    }
}
