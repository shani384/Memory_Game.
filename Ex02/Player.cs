using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    internal class Player
    {
        private readonly string r_name;
        private int m_score;
        private readonly bool r_isHumen;

        public Player(string i_name, bool i_isHumen)
        {
            r_name = i_name;
            r_isHumen = i_isHumen;
            m_score = 0;

        }

    }
}
