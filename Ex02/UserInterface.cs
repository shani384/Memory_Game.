using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class UserInterface
    {
        private MemoryGame m_memoryGame;
        private BoardDTO m_lastBoardState; /////////////////////////////////////////////////// null?
        private bool m_playerWantToQuit = false;
        public void startGame()
        {
            bool userWantsToEnd = false;
            Console.WriteLine("Welcome to the memory game!");
            UserInputDTO gameSettingsFromUser = getGameSettingsFromUser();
            Console.WriteLine("From now on at anytime you want to quit the game please press 'Q'");
            m_memoryGame = new MemoryGame(gameSettingsFromUser);
            while (!userWantsToEnd)
            {
                NewGame();
                while (!m_memoryGame.IsGameOver())
                {
                    m_lastBoardState = m_memoryGame.getCurrentStateOfBoard();
                    TurnInTheGame();
                    if(m_playerWantToQuit)
                    {
                        break;
                    }
                }
                if(m_playerWantToQuit)
                {
                    Console.WriteLine("Game stopped");
                    break;
                }
                PrintResultsOfTheGame();
                userWantsToEnd = IsUserWantsAnotherRound();
            }
        }
        private void NewGame()
        {
            m_memoryGame.initiateGame();
        }
        private bool IsUserWantsAnotherRound()
        {
            bool isValidChoise = false;
            string userChoiceStr;
            bool userWantsToEnd = false;

            Console.WriteLine("Game over :( Would you like another round? (Y/N): ");
            while (!isValidChoise)
            {
                userChoiceStr = Console.ReadLine();
                if (userChoiceStr == "N")
                {
                    userWantsToEnd = true;
                    isValidChoise = true;
                }
                else if (userChoiceStr == "Y")
                {
                    Console.WriteLine("Let's go for another round!");
                    isValidChoise = true;
                    userWantsToEnd = false;
                }
                else if(userChoiceStr == "Q")
                {
                    isValidChoise = true;
                    userWantsToEnd = true;
                    m_playerWantToQuit = true;
                }
                else
                {
                    Console.WriteLine("Invalid Choise, Please enter 'Y' if you want another round and 'N' if not.");
                }
            }
            return userWantsToEnd;
        }
        private void PrintBoard()
        {
            int rows = m_lastBoardState.Matrix.GetLength(0);
            int columns = m_lastBoardState.Matrix.GetLength(1);

            Console.Write("    ");
            for (int j = 0; j < columns; j++)
            {
                Console.Write("   " + (char)('A' + j) + "  ");
            }
            Console.WriteLine();
            Console.WriteLine("   " + new string('=', columns * 6));
            for (int i = 0; i < rows; i++)
            {
                Console.Write((i + 1).ToString().PadRight(3) + "|");
                for (int j = 0; j < columns; j++)
                {


                    Console.Write("  " + cardValueOnBoard(m_lastBoardState.Matrix[i, j]) + "  |");
                }
                Console.WriteLine();
                Console.WriteLine("   " + new string('=', columns * 6));
            }
        }
        private char cardValueOnBoard(CardDTO card)
        {
            if (card.IsHidden)
            {
                return ' ';
            }
            return card.Content;
        }
        private UserInputDTO getGameSettingsFromUser()
        {
            string player1Name, player2Name;
            int boardLength, boardWidth;
            bool isPlayer2Humen;

            Console.WriteLine("Please enter your name: ");
            player1Name = Console.ReadLine();
            isPlayer2Humen = humenOrComputerStrSelection(out player2Name);
            Console.WriteLine("Now you need to choose a board size so that the number of squares is even");
            boardLength = GetBoradLength();
            boardWidth = GetBoradWidth();
            while (!IsSizeBoardValid(boardLength, boardWidth))
            {
                Console.WriteLine("The board size is odd, please enter the board size again");
                boardLength = GetBoradLength();
                boardWidth = GetBoradWidth();
            }
            return new UserInputDTO(player1Name, player2Name, boardLength, boardWidth, isPlayer2Humen);
        }
        private bool IsSizeBoardValid(int i_boardLength, int i_boardWidth)
        {
            return MemoryGame.IsSizeMemoryBoardValid(i_boardLength, i_boardWidth);
        }
        private int GetBoradLength()
        {
            int boardLength;
            string boardLengthStr;

            Console.WriteLine("Please enter the length of the game board (a number between 4-6): ");
            boardLengthStr = Console.ReadLine();
            int.TryParse(boardLengthStr, out boardLength);
            while (boardLength < 4 || boardLength > 6)
            {
                Console.WriteLine("Invalid Length, Please enter a number between 4 to 6: ");
                int.TryParse(boardLengthStr = Console.ReadLine(), out boardLength);
            }
            return boardLength;
        }
        private int GetBoradWidth()
        {
            string boardWidthStr;
            int boardWidth;

            Console.WriteLine("Please enter the width of the game board (a number between 4-6): ");
            int.TryParse(boardWidthStr = Console.ReadLine(), out boardWidth);
            while (boardWidth < 4 || boardWidth > 6)
            {
                Console.WriteLine("Invalid Width, Please enter a number between 4 to 6: ");
                int.TryParse(boardWidthStr = Console.ReadLine(), out boardWidth);
            }
            return boardWidth;
        }
        private bool humenOrComputerStrSelection(out string o_player2Name)
        {
            bool isValidInput = false;
            string humenOrComputerStrSelection;
            bool isPlayer2Humen = false;
            o_player2Name = null;

            Console.WriteLine("Please enter 'Y' if you want to play against another player or enter 'N' if you want to play against the computer");
            humenOrComputerStrSelection = Console.ReadLine();
            while (!isValidInput)
            {
                if (humenOrComputerStrSelection == "Y")
                {
                    isPlayer2Humen = true;
                    isValidInput = true;
                    Console.WriteLine("Please enter the second player name: ");
                    o_player2Name = Console.ReadLine();
                }
                else if (humenOrComputerStrSelection == "N")
                {
                    isPlayer2Humen = false;
                    o_player2Name = null;
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' if you want to play against another player or enter 'N' if you want to play against the computer");
                    humenOrComputerStrSelection = Console.ReadLine();
                }
            }
            return isPlayer2Humen;
        }
        private CoordinateInBoard? GetPlayerCardChoice()
        {
            string rowStr = null, columnStr = null;
            bool validInput = false;
            CoordinateInBoard? coordinateInBoard = null;
            Console.WriteLine(string.Format(@"Please enter number of a row (1 - {0}):", m_lastBoardState.Matrix.GetLength(1)));
            while (!validInput)
            {
                rowStr = Console.ReadLine();
                if (rowStr != "Q")
                {
                    if (rowStr.Length != 1 || char.Parse(rowStr) < '1' || char.Parse(rowStr) > m_lastBoardState.Matrix.GetLength(1) + '0')
                    {
                        Console.WriteLine("The row number is out of bounds of the board, try again");
                    }
                    else
                    {
                        validInput = true;
                    }
                }
                else
                {
                    m_playerWantToQuit = true;
                    validInput = true;
                }
            }
            if (!m_playerWantToQuit)
            {
                validInput = false;
                Console.WriteLine(string.Format(@"Please enter number of a column (A - {0}):", (char)('A' + m_lastBoardState.Matrix.GetLength(0) - 1)));
                while (!validInput)
                {
                    columnStr = Console.ReadLine();
                    if (columnStr != "Q")
                    {
                        if (columnStr.Length != 1 || char.Parse(columnStr) < 'A' || char.Parse(columnStr) > m_lastBoardState.Matrix.GetLength(1) + 'A' - 1)
                        {
                            Console.WriteLine("The column character is outside the board boundaries, try again");
                        }
                        else
                        {
                            validInput = true;
                            coordinateInBoard = new CoordinateInBoard(int.Parse(rowStr) - 1, char.Parse(columnStr) - 'A');
                        }
                    }
                    else
                    {
                        m_playerWantToQuit = true;
                        validInput = true;
                    }
                }
            }
            return coordinateInBoard;
        }
        public void TurnInTheGame()
        {
            PlayerDTO player = m_memoryGame.getNextTurnPlayer();
            CoordinateInBoard? card1Choice;
            CoordinateInBoard? card2Choice;
            int[] cardsConcat = null;

            PrintBoard();
            if (player.IsHumen)
            {
                Console.WriteLine(string.Format(@"{0} it is your turn!", player.Name));
                card1Choice = GetPlayerCardChoice();
                if (!m_playerWantToQuit)
                {
                    while (!IsCardHidden(card1Choice))
                    {
                        Console.WriteLine("This card is not hidden so you cannot choose it. Please select another card.");
                        card1Choice = GetPlayerCardChoice();
                    }
                    if (!m_playerWantToQuit)
                    {
                        showChosenCard(card1Choice);
                        card2Choice = GetPlayerCardChoice();
                        while (!IsCardHidden(card2Choice))
                        {
                            Console.WriteLine("This card is not hidden so you cannot choose it. Please select another card.");
                            card2Choice = GetPlayerCardChoice();
                        }
                        if (!m_playerWantToQuit)
                        {
                            showChosenCard(card2Choice);
                            System.Threading.Thread.Sleep(2000);
                            ConsoleUtils.Screen.Clear();
                            cardsConcat = ConcatCardsChoice(card1Choice, card2Choice);
                        }
                    }
                    m_memoryGame.runCurrentTurn(cardsConcat);
                }
            }
            else
            {
                //card1Choice = new CoordinateInBoard(cardsConcat[0], cardsConcat[1]);
                //card2Choice = new CoordinateInBoard(cardsConcat[2], cardsConcat[3]);
                //showChosenCard(card1Choice);
                //showChosenCard(card2Choice);
                Console.WriteLine("It is computer turn!");

                System.Threading.Thread.Sleep(4000);
                ConsoleUtils.Screen.Clear();
            }
        }
        //private int[] convertCardsUserChoiceToMatrixIndex(string[] i_cardsChoice)
        //{
        //    int[] res = new int[2];
        //    res[0] = int.Parse(i_cardsChoice[0]) - 1;
        //    res[1] = char.Parse(i_cardsChoice[1]) -'A';

        //    return res;
        //}
        private void showChosenCard(CoordinateInBoard? cardIndexes)
        {
            if (cardIndexes.HasValue)
            {
                m_lastBoardState.Matrix[cardIndexes.Value.Row, cardIndexes.Value.Column].IsHidden = false;
                ConsoleUtils.Screen.Clear();
                PrintBoard();
            }
            else
            {
                Console.WriteLine("Invalid card coordinates.");
            }
        }
        private int[] ConcatCardsChoice(CoordinateInBoard? i_card1, CoordinateInBoard? i_card2)
        {
            int[] dataCards = new int[4];
            if (i_card1.HasValue && i_card2.HasValue)
            {
                dataCards[0] = i_card1.Value.Row;
                dataCards[1] = i_card1.Value.Column;
                dataCards[2] = i_card2.Value.Row;
                dataCards[3] = i_card2.Value.Column;
            }
            return dataCards;
        }
        public bool IsCardHidden(CoordinateInBoard? card)
        {
            bool isCardHidden = false;
            if (card.HasValue)
            {
                isCardHidden = m_lastBoardState.Matrix[card.Value.Row, card.Value.Column].IsHidden;
            }
            return isCardHidden;
             
        }
        private void PrintResultsOfTheGame()
        {
            PlayerDTO[] players = m_memoryGame.GetPlayerDetails();
            Console.WriteLine(string.Format(@"{0}'s score: {1}", players[0].Name, players[0].Score));
            if (players[1].IsHumen)
            {
                Console.WriteLine(string.Format(@"{0}'s score: {1}", players[1].Name, players[1].Score));
            }
            else
            {
                Console.WriteLine(string.Format(@"Computer's score: {0}", players[1].Score));
            }
        }
    }
}
