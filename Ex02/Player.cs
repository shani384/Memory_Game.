

namespace Ex02
{
    internal class Player
    {
        private readonly string r_Name;
        private int m_Score;
        private readonly bool r_IsHuman;

        public Player(string i_Name, bool i_IsHuman)
        {
            r_Name = i_Name;
            r_IsHuman = i_IsHuman;
            m_Score = 0;
        }
        public void InitiatePlayer()
        {
            m_Score = 0;
        }
        public string Name
        {
            get
            {
                return r_Name;
            }
        }
        public bool IsHumen
        {
            get
            {
                return r_IsHuman;
            }
        }
        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }
        public PlayerDTO CreatePlayerDTO()
        {
            return new PlayerDTO(r_Name, m_Score, r_IsHuman);
        }
    }
}
