using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class UserInterface
    {
        private MemoryGame memoryGame;


        public void startGame()
        {
            bool userWantsToEnd = false;
            
            Console.WriteLine("Welcome to the memory game!");
            UserInputTransferor userInput = getUserInput();
            memoryGame = new MemoryGame(userInput);
            while (!userWantsToEnd) 
            {
                NewGame();
                while (!memoryGame.IsGameOver)
                {
                    PrintBoard();
                    TurnInTheGame();
                }
                PrintResultsOfTheGame();
                userWantsToEnd = IsUserWantsAnotherRound();
            }
            
        }
        private bool IsUserWantsAnotherRound()
        {
            bool isValidChoise = false;
            string userChoiceStr;
            bool userWantsToEnd;

            Console.Write("Game over :( Would you like another round? (Y/N): ");
            while(!isValidChoise)
            { 
                userChoiceStr = Console.Read();
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
            int rows = m_matrix.GetLength(0);
            int columns = m_matrix.GetLength(1);

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
                    Console.Write("  " + m_matrix[i, j].ContentUp + "  |");
                }
                Console.WriteLine();
                Console.WriteLine("   " + new string('=', columns * 6));
            }
        }
        private UserInputTransferor getUserInput()
        {
            string player1Name, player2Name, boardLengthStr, boardWidthStr;
            int boardLength, boardWidth;
            bool isPlayer2Humen;
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
                }
            }
            
            Console.WriteLine("Please enter the length of the game board (a number between 4-6): ");
            int.TryParse(boardLengthStr = Console.ReadLine(), out boardLength);
            while(boardLength < 4 || boardLength > 6)
            {
                Console.Write("Invalid Length, Please enter a number between 4 to 6: ")
                int.TryParse(boardLengthStr = Console.ReadLine(), out boardLength);
            }
            Console.WriteLine("Please enter the width of the game board (a number between 4-6): ");
            int.TryParse(boardWidthStr = Console.ReadLine(), out boardWidth);
            while(boardWidth < 4 || boardWidth > 6)
            {
                Console.Write("Invalid Width, Please enter a number between 4 to 6: ")
                int.TryParse(boardLengthStr = Console.ReadLine(), out boardLength);
            }
            return new UserInputTransferor(player1Name, player2Name, boardLength, boardWidth, isPlayer2Humen);
        }
    }
}
