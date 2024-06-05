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
        public void InitiatePlayer()
        {
            m_score = 0;
        }
        public string Name
        {
            get
            {
                return r_name;
            }
        }
        public bool IsHumen
        {
            get
            {
                return r_isHumen;
            }
        }
        public int Score
        {
            get
            {
                return m_score;
            }
            set
            {
                m_score = value;
            }
        }
        public PlayerDTO createPlayerDTO()
        {
            return new PlayerDTO(r_name, m_score, r_isHumen);
        }
    }
}
