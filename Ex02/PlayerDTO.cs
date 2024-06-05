using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class PlayerDTO
    {
        private readonly string r_name;
        private readonly int r_score;
        private readonly bool r_isHumen;
        public PlayerDTO(string i_name, int i_score, bool i_isHumen)
        {
            r_name = i_name;
            r_score = i_score;
            r_isHumen = i_isHumen;
        }
        public string Name
        {
            get
            {
                return r_name;
            }
        }
        public int Score
        {
            get
            {
                return r_score;
            }
        }
        public bool IsHumen
        {
            get
            {
                return r_isHumen;
            }
        }
        public string WantToQuit
        {
            get
            {
                return WantToQuit;
            }
        }
    }
}
