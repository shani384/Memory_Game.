using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class ComputerPlayer
    {
        IDictionary<char, int[]> m_revealedCards;
        List<int[]> m_readyPairs;

        public int[] getComputerCardsChoice()
        {
            int[] res;

            if (m_readyPairs.Count > 0)
            {
                res = m_readyPairs.First();
                m_readyPairs.RemoveAt(0);
            }
            else
            {
                res = null;
            }

            return res;
        }
        public void AddNewCard(int[] indexes, char content)
        {
            if (m_revealedCards.ContainsKey(content))
            {
                m_readyPairs.Add(ConcatCards(indexes, m_revealedCards[content]);//אני פה!!!!!!!
            }
        }
        private int[] ConcatCards(int[] i_card1, int[] i_card2)
        {
            int[] res = new int[4];
            res[0] = i_card1[0];
            res[1] = i_card1[1];
            res[2] = i_card2[0];
            res[3] = i_card2[1];
            return res;
        }
    }
}
