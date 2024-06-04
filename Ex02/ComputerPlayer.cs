using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    class ComputerPlayer
    {

        IDictionary<char, int[]> m_revealedCards;
        List<int[]> m_readyCardsPairs;

        public int[] getComputerCardsChoice()
        {
            int[] res;


            if (m_readyCardsPairs.Count > 0)
            {
                res = m_readyCardsPairs.First();
                m_readyCardsPairs.RemoveAt(0);
            }
            else
            {
                res = null;//todo: return random
            }
            return res;
        }
        public void RevealNewCard(int[] indexes, char content)
        {
            if (m_revealedCards.ContainsKey(content))
            {
                if (!IsTheSameCard(m_revealedCards[content], indexes))
                {
                    m_readyCardsPairs.Add(ConcatCards(indexes, m_revealedCards[content]));
                    m_revealedCards.Remove(content);
                }
            }
            else
            {
                m_revealedCards.Add(content, indexes);
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
        private bool IsTheSameCard(int[] card1, int[] card2)
        {
            return card1[0] == card2[0] && card1[1] == card2[1];
        }
        
    }
}
