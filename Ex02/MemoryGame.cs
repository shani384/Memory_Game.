using System;
using System.Collections.Generic;


namespace Ex02
{
    public class MemoryGame
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_TurnPlayer;
        private ComputerPlayer m_ComputerPlayer;
        public MemoryGame(UserInputDTO i_UserInput)
        {
            m_Board = new Board(i_UserInput.BoardLength, i_UserInput.BoardWidth);
            m_Player1 = new Player(i_UserInput.Player1Name, true);
            if (i_UserInput.IsPlayer2Humen)
            {
                m_Player2 = new Player(i_UserInput.Player2Name, true);
                m_ComputerPlayer = null;
            }
            else
            {
                m_Player2 = new Player(null, false);
                m_ComputerPlayer = new ComputerPlayer(m_Board);
            }

            m_TurnPlayer = m_Player1;
        }
        public void InitiateGame()
        {
            m_Board.InitiateBoard();
            m_Player1.InitiatePlayer();
            m_Player2.InitiatePlayer();
            m_TurnPlayer = m_Player1;
            if(!m_Player2.IsHumen)
            {
                m_ComputerPlayer.InitiateComputerPlayer(m_Board);
            }
        }
        public bool IsGameOver()
        {
            return m_Board.IsAllCardsOpen();
        }
        public BoardDTO GetCurrentStateOfBoard()
        {
            return m_Board.GetBoardDTO();
        }
        public PlayerDTO GetNextTurnPlayer()
        {
            return m_TurnPlayer.CreatePlayerDTO();
        }
        public void RunCurrentTurn(int[] io_CardsChoice)
        {
            bool ContentsOfCardsAreEqual;
            CardCoordinate[] computerCardsChoise;

            if (!m_TurnPlayer.IsHumen)
            {
                computerCardsChoise = m_ComputerPlayer.GetComputerCardsChoice(m_Board);
                io_CardsChoice[0] = computerCardsChoise[0].Row.GetValueOrDefault(-1);
                io_CardsChoice[1] = computerCardsChoise[0].Column.GetValueOrDefault(-1);
                io_CardsChoice[2] = computerCardsChoise[1].Row.GetValueOrDefault(-1);
                io_CardsChoice[3] = computerCardsChoise[1].Column.GetValueOrDefault(-1);
            }

            ContentsOfCardsAreEqual = m_Board.Check2CardsAndRevealThemIfEqual(io_CardsChoice);
            if (m_TurnPlayer.IsHumen && m_ComputerPlayer != null)
            {
                m_ComputerPlayer.UpdateComputerMemory(io_CardsChoice, m_Board, ContentsOfCardsAreEqual);
            }
            if (ContentsOfCardsAreEqual)
            {
                m_TurnPlayer.Score = m_TurnPlayer.Score + 1;
            }
            else
            {
                changeTurnPlayer();
            }
        }
        private void changeTurnPlayer()
        {
            if(m_TurnPlayer == m_Player1)
            {
                m_TurnPlayer = m_Player2;
            }
            else
            {
                m_TurnPlayer = m_Player1;
            }
        }
        public PlayerDTO[] GetPlayerDetails()
        {
            PlayerDTO[] playersDetails = new PlayerDTO[2];
            playersDetails[0] = m_Player1.CreatePlayerDTO();
            playersDetails[1] = m_Player2.CreatePlayerDTO();

            return playersDetails;
        }
        public static bool IsSizeMemoryBoardValid(int i_LengthBoard, int i_WidthBoard)
        {
            return Board.IsSizeBoardEven(i_LengthBoard, i_WidthBoard);
        }
    }
}
