using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class UserInterface
    {
        private MemoryGame memoryGame;
        private BoardDTO m_lastBoardState;


        public void startGame()
        {
            bool userWantsToEnd = false;
            
            Console.WriteLine("Welcome to the memory game!");
            UserInputDTO gameSettingsFromUser = getGameSettingsFromUser();
            memoryGame = new MemoryGame(gameSettingsFromUser);
            while (!userWantsToEnd) 
            {
                //NewGame();
                while (!memoryGame.IsGameOver())
                {
                    m_lastBoardState = memoryGame.getCurrentStateOfBoard();
                    TurnInTheGame();

                }
                //PrintResultsOfTheGame();
                userWantsToEnd = IsUserWantsAnotherRound();
            }
            
        }
        private bool IsUserWantsAnotherRound()
        {
            bool isValidChoise = false;
            string userChoiceStr;
            bool userWantsToEnd = false;

            Console.WriteLine("Game over :( Would you like another round? (Y/N): ");
            while(!isValidChoise)
            { 
                userChoiceStr = Console.ReadLine();
                if(userChoiceStr == "N" || userChoiceStr != "Q")
                {
                   userWantsToEnd = true;
                   isValidChoise = true;
                }
                else if(userChoiceStr == "Y")
                {
                   isValidChoise = true;
                   userWantsToEnd = false;
                }
                else
                {
                    Console.Write("Invalid Choise, Please enter 'Y' if you want another round and 'N' if not.");
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
            if(card.IsHidden)
            {
                return ' ';
            }
            return card.Content;
        }
        private UserInputDTO getGameSettingsFromUser()
        {
            string player1Name, player2Name = null, boardLengthStr, boardWidthStr;
            int boardLength, boardWidth;
            bool isPlayer2Humen = false;
            bool isValidInput = false;
            string humenOrComputerStrSelection;

            Console.WriteLine("Please enter your name: ");
            player1Name = Console.ReadLine();
            Console.WriteLine("Please enter 'Y' if you want to play against another player or enter 'N' if you want to play against the computer");
            humenOrComputerStrSelection = Console.ReadLine();
            while (!isValidInput)
            {
                if(humenOrComputerStrSelection == "Y")
                {
                    isPlayer2Humen = true;
                    isValidInput = true;
                    Console.WriteLine("Please enter the second player name: ");
                    player2Name = Console.ReadLine();
                }
                else if(humenOrComputerStrSelection == "N")
                {
                    isPlayer2Humen = false;
                    player2Name = null;
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' if you want to play against another player or enter 'N' if you want to play against the computer");
                    humenOrComputerStrSelection = Console.ReadLine();

                }
            }
            
            Console.WriteLine("Please enter the length of the game board (a number between 4-6): ");
            int.TryParse(boardLengthStr = Console.ReadLine(), out boardLength);
            while(boardLength < 4 || boardLength > 6)
            {
                Console.Write("Invalid Length, Please enter a number between 4 to 6: ");
                int.TryParse(boardLengthStr = Console.ReadLine(), out boardLength);
            }
            Console.WriteLine("Please enter the width of the game board (a number between 4-6): ");
            int.TryParse(boardWidthStr = Console.ReadLine(), out boardWidth);
            while(boardWidth < 4 || boardWidth > 6)
            {
                Console.Write("Invalid Width, Please enter a number between 4 to 6: ");
                int.TryParse(boardLengthStr = Console.ReadLine(), out boardLength);
            }
            return new UserInputDTO(player1Name, player2Name, boardLength, boardWidth, isPlayer2Humen);
        }
        private int[] GetPlayerCardChoice()
        {
            string[] res = new string[2];
            bool validInput = false;

            Console.WriteLine(string.Format(@"Please enter number of a row (1 - {0}):", m_lastBoardState.Matrix.GetLength(1)));
            while(validInput == false)
            {
                res[0] = Console.ReadLine();
                if(res[0].Length != 1 || char.Parse(res[0]) < '1' || char.Parse(res[0]) > m_lastBoardState.Matrix.GetLength(1)+'0')
                {
                    Console.WriteLine("Invalid input, try again");
                }
                else
                {
                    validInput = true;
                }
            }
            validInput = false;
            Console.WriteLine(string.Format(@"Please enter number of a column (A - {0}):", (char)('A'+m_lastBoardState.Matrix.GetLength(0)-1)));
            while (validInput == false)
            {
                res[1] = Console.ReadLine();
                if (res[1].Length != 1 || char.Parse(res[1]) < 'A' || char.Parse(res[1]) > m_lastBoardState.Matrix.GetLength(1) + 'A' - 1)
                {
                    Console.WriteLine("Invalid input, try again");
                }
                else
                {
                    validInput = true;
                }
            }
            return convertCardsUserChoiceToMatrixIndex(res);
        }
        public void TurnInTheGame()
        {
            PlayerDTO player = memoryGame.getNextTurnPlayer();
            int[] card1Choice = null;
            int[] card2Choice = null;
            int[] cardsConcat = null;
            bool res;


            PrintBoard();
            if (player.IsHumen)
            {
                Console.WriteLine(string.Format(@"{0} it is your turn!", player.Name));
                
                card1Choice = GetPlayerCardChoice();
                while (!IsCardHidden(card1Choice))
                {
                    Console.WriteLine("This card is not hidden so you cannot choose it. Please select another card.");
                    card1Choice = GetPlayerCardChoice();
                }
                    showChosenCard(card1Choice);
                card2Choice = GetPlayerCardChoice();
                while (!IsCardHidden(card2Choice))
                {
                    Console.WriteLine("This card is not hidden so you cannot choose it. Please select another card.");
                    card2Choice = GetPlayerCardChoice();
                } 
                showChosenCard(card2Choice);
                System.Threading.Thread.Sleep(2000);
                Console.Clear();//todo: change to what guy gave us.
                cardsConcat = ConcatCardsChoice(card1Choice, card2Choice);
            }
            
            res = memoryGame.runCurrentTurn(ref cardsConcat);
            
            if (!player.IsHumen)
            {
                showChosenCard(cardsConcat);
                showChosenCard(cardsConcat.Skip(2).ToArray());
                Console.WriteLine("It is computer turn!");
                System.Threading.Thread.Sleep(4000);
                Console.Clear();//todo: change to what guy gave us.
            }


        }
        private int[] convertCardsUserChoiceToMatrixIndex(string[] i_cardsChoice)
        {
            int[] res = new int[2];
            res[0] = int.Parse(i_cardsChoice[0]) - 1;
            res[1] = char.Parse(i_cardsChoice[1]) -'A';

            return res;
        }
        private void showChosenCard(int[] cardIndexes)
        {
            m_lastBoardState.Matrix[cardIndexes[0], cardIndexes[1]].IsHidden = false;
            Console.Clear();//todo: change to what guy gave us
            PrintBoard();
        }
        private int[] ConcatCardsChoice(int[] i_card1, int[] i_card2)
        {
            int[] res = new int[4];
            res[0] = i_card1[0];
            res[1] = i_card1[1];
            res[2] = i_card2[0];
            res[3] = i_card2[1];
            return res;
        }
        public bool IsCardHidden(int[] card)
        {
            if(m_lastBoardState.Matrix[card[0], card[1]].IsHidden)
            {
                return true;
            }
            return false;
        }
    }
}
