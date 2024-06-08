using System;


namespace Ex02
{
    public class UserInterface
    {
        private MemoryGame m_MemoryGame;
        private BoardDTO m_LastBoardState; 
        private bool m_PlayerWantToQuit = false;
        public void StartGame()
        {
            bool userWantsToEnd = false;

            Console.WriteLine("Welcome to the memory game!");
            UserInputDTO gameSettingsFromUser = getGameSettingsFromUser();
            Console.WriteLine("From now on at anytime you want to quit the game please press 'Q'");
            m_MemoryGame = new MemoryGame(gameSettingsFromUser);
            while (!userWantsToEnd)
            {
                newGame();
                while (!m_MemoryGame.IsGameOver())
                {
                    m_LastBoardState = m_MemoryGame.GetCurrentStateOfBoard();
                    TurnInTheGame();
                    if(m_PlayerWantToQuit)
                    {
                        break;
                    }
                }
                if(m_PlayerWantToQuit)
                {
                    Console.WriteLine("Game stopped");
                    break;
                }
                printResultsOfTheGame();
                userWantsToEnd = isUserWantsAnotherRound();
            }
            Console.WriteLine("Bye Bye...");
        }
        private void newGame()
        {
            m_MemoryGame.InitiateGame();
        }
        private bool isUserWantsAnotherRound()
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
                    m_PlayerWantToQuit = true;
                }
                else
                {
                    Console.WriteLine("Invalid Choise, Please enter 'Y' if you want another round and 'N' if not.");
                }
            }

            return userWantsToEnd;
        }
        private void printBoard(string i_PlayerName)
        {
            int rows = m_LastBoardState.Matrix.GetLength(0);
            int columns = m_LastBoardState.Matrix.GetLength(1);

            if(i_PlayerName != null)
            {
                Console.WriteLine(string.Format(@"{0}'s turn!", i_PlayerName));
                Console.WriteLine("==============================");
            }
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


                    Console.Write("  " + cardValueOnBoard(m_LastBoardState.Matrix[i, j]) + "  |");
                }
                Console.WriteLine();
                Console.WriteLine("   " + new string('=', columns * 6));
            }
        }
        private char cardValueOnBoard(CardDTO i_Card)
        {
            if (i_Card.IsHidden)
            {
                return ' ';
            }

            return i_Card.Content;
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
            boardLength = getBoradLength();
            boardWidth = getBoradWidth();
            while (!isSizeBoardValid(boardLength, boardWidth))
            {
                Console.WriteLine("The board size is odd, please enter the board size again");
                boardLength = getBoradLength();
                boardWidth = getBoradWidth();
            }

            return new UserInputDTO(player1Name, player2Name, boardLength, boardWidth, isPlayer2Humen);
        }
        private bool isSizeBoardValid(int i_BoardLength, int i_BoardWidth)
        {
            return MemoryGame.IsSizeMemoryBoardValid(i_BoardLength, i_BoardWidth);
        }
        private int getBoradLength()
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
        private int getBoradWidth()
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
        private bool humenOrComputerStrSelection(out string o_Player2Name)
        {
            bool isValidInput = false;
            string humenOrComputerStrSelection;
            bool isPlayer2Humen = false;
            o_Player2Name = null;

            Console.WriteLine("Please enter 'Y' if you want to play against another player or enter 'N' if you want to play against the computer");
            humenOrComputerStrSelection = Console.ReadLine();
            while (!isValidInput)
            {
                if (humenOrComputerStrSelection == "Y")
                {
                    isPlayer2Humen = true;
                    isValidInput = true;
                    Console.WriteLine("Please enter the second player name: ");
                    o_Player2Name = Console.ReadLine();
                }
                else if (humenOrComputerStrSelection == "N")
                {
                    isPlayer2Humen = false;
                    o_Player2Name = null;
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
        private CoordinateInBoard? getPlayerCardChoice()
        {
            string rowStr = null, columnStr = null;
            bool validInput = false;
            CoordinateInBoard? coordinateInBoard = null;

            Console.WriteLine(string.Format(@"Please enter number of a row (1 - {0}):", m_LastBoardState.Matrix.GetLength(1)));
            while (!validInput)
            {
                rowStr = Console.ReadLine();
                if (rowStr != "Q")
                {
                    if (isRowInputValid(rowStr))
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
                    m_PlayerWantToQuit = true;
                    validInput = true;
                }
            }
            if (!m_PlayerWantToQuit)
            {
                validInput = false;
                Console.WriteLine(string.Format(@"Please enter number of a column (A - {0}):", (char)('A' + m_LastBoardState.Matrix.GetLength(0) - 1)));
                while (!validInput)
                {
                    columnStr = Console.ReadLine();
                    if (columnStr != "Q")
                    {
                        if (isColumnInputValid(columnStr))
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
                        m_PlayerWantToQuit = true;
                        validInput = true;
                    }
                }
            }

            return coordinateInBoard;
        }
        private bool isRowInputValid(string i_Row)
        {
            return i_Row.Length != 1 || char.Parse(i_Row) < '1' || char.Parse(i_Row) > m_LastBoardState.Matrix.GetLength(1) + '0';
        }
        private bool isColumnInputValid(string i_Column)
        {
            return i_Column.Length != 1 || char.Parse(i_Column) < 'A' || char.Parse(i_Column) > m_LastBoardState.Matrix.GetLength(1) + 'A' - 1;
        }
        public void TurnInTheGame()
        {
            PlayerDTO player = m_MemoryGame.GetNextTurnPlayer();

            printBoard(null);
            if (player.IsHuman)
            {
                humanPlayerTurn(player.Name);
            }
            else
            {
                notHumanPlayer();
            }
        }
        private void humanPlayerTurn(string i_PlayerName)
        {
            CoordinateInBoard? card1Choice;
            CoordinateInBoard? card2Choice;
            int[] cardsConcat = null;

            Console.WriteLine(string.Format(@"{0} it is your turn!", i_PlayerName));
            card1Choice = getPlayerCardChoice();
            if (!m_PlayerWantToQuit)
            {
                while (!isCardHidden(card1Choice))
                {
                    Console.WriteLine("This i_Card is not hidden so you cannot choose it. Please select another i_Card.");
                    card1Choice = getPlayerCardChoice();
                }
                if (!m_PlayerWantToQuit)
                {
                    showChosenCard(card1Choice, i_PlayerName);
                    card2Choice = getPlayerCardChoice();
                    while (!isCardHidden(card2Choice))
                    {
                        Console.WriteLine("This i_Card is not hidden so you cannot choose it. Please select another i_Card.");
                        card2Choice = getPlayerCardChoice();
                    }
                    if (!m_PlayerWantToQuit)
                    {
                        showChosenCard(card2Choice, i_PlayerName);
                        System.Threading.Thread.Sleep(2000);
                        ConsoleUtils.Screen.Clear();
                        cardsConcat = concatCardsChoice(card1Choice, card2Choice);
                    }
                }
                m_MemoryGame.RunCurrentTurn(cardsConcat);
            }
        }
        private void notHumanPlayer()
        {
            CoordinateInBoard? card1Choice;
            CoordinateInBoard? card2Choice;
            int[] cardsConcat = null;

            Console.WriteLine("It is computer turn!");
            cardsConcat = new int[4];
            m_MemoryGame.RunCurrentTurn(cardsConcat);
            card1Choice = new CoordinateInBoard(cardsConcat[0], cardsConcat[1]);
            card2Choice = new CoordinateInBoard(cardsConcat[2], cardsConcat[3]);
            showChosenCard(card1Choice,"Computer");
            showChosenCard(card2Choice,"Computer");
            System.Threading.Thread.Sleep(4000);
            ConsoleUtils.Screen.Clear();
        }
        private void showChosenCard(CoordinateInBoard? i_CardIndexes, string i_Name)
        {
            if (i_CardIndexes.HasValue)
            {
                m_LastBoardState.Matrix[i_CardIndexes.Value.Row, i_CardIndexes.Value.Column].IsHidden = false;
                ConsoleUtils.Screen.Clear();
                printBoard(i_Name);
            }
            else
            {
                Console.WriteLine("Invalid i_Card coordinates.");
            }
        }
        private int[] concatCardsChoice(CoordinateInBoard? i_Card1, CoordinateInBoard? i_Card2)
        {
            int[] dataCards = new int[4];

            if (i_Card1.HasValue && i_Card2.HasValue)
            {
                dataCards[0] = i_Card1.Value.Row;
                dataCards[1] = i_Card1.Value.Column;
                dataCards[2] = i_Card2.Value.Row;
                dataCards[3] = i_Card2.Value.Column;
            }

            return dataCards;
        }
        private bool isCardHidden(CoordinateInBoard? i_Card)
        {
            bool isCardHidden = false;

            if (i_Card.HasValue)
            {
                isCardHidden = m_LastBoardState.Matrix[i_Card.Value.Row, i_Card.Value.Column].IsHidden;
            }

            return isCardHidden;
             
        }
        private void printResultsOfTheGame()
        {
            PlayerDTO[] players = m_MemoryGame.GetPlayerDetails();
            string winnerName = checkWhoIsTheWinner(players);

            if (winnerName != null)
            {
                Console.WriteLine("And the winner is...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine(winnerName + "!");
            }
            else
            {
                Console.WriteLine("The game ended in a draw");
            }
            Console.WriteLine(string.Format(@"{0}'s score: {1}", players[0].Name, players[0].Score));
            if (players[1].IsHuman)
            {
                Console.WriteLine(string.Format(@"{0}'s score: {1}", players[1].Name, players[1].Score));
            }
            else
            {
                Console.WriteLine(string.Format(@"Computer's score: {0}", players[1].Score));
            }
        }
        private string checkWhoIsTheWinner(PlayerDTO[] i_players)
        {
            string winnerName = null;

            if (i_players[0].Score > i_players[1].Score)
            {
                winnerName = i_players[0].Name;
            }
            else if (i_players[1].Score > i_players[0].Score)
            {
                if(!i_players[1].IsHuman)
                {
                    winnerName = "Computer";
                }
                else
                {
                    winnerName = i_players[1].Name;
                }
            }

            return winnerName;
        }
    }
}
