

namespace Ex02
{
    public class PlayerDTO
    {
        private readonly string r_Name;
        private readonly int r_Score;
        private readonly bool r_isHuman;
        public PlayerDTO(string i_Name, int i_Score, bool i_IsHuman)
        {
            r_Name = i_Name;
            r_Score = i_Score;
            r_isHuman = i_IsHuman;
        }
        public string Name
        {
            get
            {
                return r_Name;
            }
        }
        public int Score
        {
            get
            {
                return r_Score;
            }
        }
        public bool IsHuman
        {
            get
            {
                return r_isHuman;
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
