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
            System.Console.WriteLine("Welcome to the memory game!");
            UserInputTransferor userInput = getUserInput();
            memoryGame.initializeGame(userInput);

        }
        private UserInputTransferor getUserInput()
        {
            string player1Name, player2Name;
            int boardLength, boardWidth;
            bool isPlayer2Humen;
            string humenOrComputerStrSelection;

            System.Console.WriteLine("Please enter your name: ");
            player1Name = System.Console.ReadLine();
            System.Console.WriteLine("Please enter 'Y' if you want to play against another player or enter 'N' if you want to play against the computer");
            humenOrComputerStrSelection = System.Console.ReadLine();
            if(humenOrComputerStrSelection == "Y")
            {
                isPlayer2Humen = true;
                //read here the 2nd name
            }
            else if(humenOrComputerStrSelection == "N")
            {
                isPlayer2Humen = false;
            }
            //read from user
            return new UserInputTransferor(player1Name, player2Name, boardLength, boardWidth, isPlayer2Humen);
        }
    }
}
