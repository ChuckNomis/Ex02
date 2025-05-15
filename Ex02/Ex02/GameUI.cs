using System;
using System.Collections.Generic;
using Ex02.ConsoleUtils;

namespace Ex02
{
    internal class GameUI
    {
        private int _maxGuesses;

        public int  getNumberOfGuesses()
        {
            int    numberOfGuesses;
            string input;

            Console.WriteLine("Enter number of guesses (4–10):");

            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out numberOfGuesses)
                    && numberOfGuesses >= 4
                    && numberOfGuesses <= 10)
                {
                    _maxGuesses = numberOfGuesses;
                    return numberOfGuesses;
                }

                Console.WriteLine("Invalid. Please enter an integer between 4 and 10:");
            }
        }

        public void clearScreen()
        {
            Screen.Clear();
        }

        public void printTheBoard(List<Pin> pinsHistory,List<Result> feedbackHistory)
        {
            const int ColWidth = 9;
            string    horizontalLine = "|=========|=======|";

            Console.WriteLine("|" + "Pins:    " + "|" + "Result:" + "|");
            Console.WriteLine(horizontalLine);
            Console.WriteLine("|" + " # # # # " + "|       |");
            Console.WriteLine(horizontalLine);

            for (int row = 0; row < _maxGuesses; row++)
            {
                string pinsCell;
                string resultCell;

                if (row < pinsHistory.Count)
                {
                    pinsCell = string.Join(" ", pinsHistory[row]._pinValue.ToCharArray());
                    resultCell = string.Join(" ", feedbackHistory[row]._result.ToCharArray());
                }
                else
                {
                    pinsCell = string.Empty.PadRight(ColWidth);
                    resultCell = string.Empty.PadRight(ColWidth);
                }

                Console.WriteLine("| "+ pinsCell + " |"+ resultCell+ "|");
                Console.WriteLine(horizontalLine);
            }
        }

        public Pin  getUserGuess()
        {
            Console.WriteLine("Enter your guess (4 letters A–H) or Q to quit:");
            string input = Console.ReadLine().ToUpper();

            while (!InputValidityCheck(input))
            {
                Console.WriteLine("Enter your guess (4 letters A–H) or Q to quit:");
                input = Console.ReadLine().ToUpper();
            }
            
            return new Pin(input);
        }

        public bool InputValidityCheck(string i_input)
        {
            bool          inputStatus = true;
            const int     k_ExpectedLength = 4;
            string        allowedLetters = "ABCDEFGH";
            List<char>    availableLetters = allowedLetters.ToList();
            HashSet<char> seenLetters = new HashSet<char>();

            if (i_input == "Q")
            {
                inputStatus = true;  
            }

            else if (i_input.Length == k_ExpectedLength)
            {
                foreach (char currentChar in i_input)
                {
                    if (!availableLetters.Contains(currentChar) || seenLetters.Contains(currentChar))
                    {
                        inputStatus = false;
                        break;
                    }

                    seenLetters.Add(currentChar);
                }
            }
            else
            {
              inputStatus = false;
            }
            return inputStatus;
        }

        public void showWin()
        {
            Console.WriteLine("Congratulations! You cracked the code!");
        }

        public void showLose()
        {
            Console.WriteLine("You've run out of attempts, you lose!");
        }

        public void showPlayAgain(ref bool keepPlaying)
        {
            Console.WriteLine("Play again? (Y/N):");
            string answer = Console.ReadLine().Trim().ToUpper();

            while (answer != "Y" && answer != "N")
            {
                Console.WriteLine("Invalid input, Play again? (Y/N):");
                answer = Console.ReadLine().Trim().ToUpper();
            }

            if (answer == "Y")
            {
                keepPlaying = true;
            }

            else
            {
                keepPlaying = false;
            }
        }
            
        public void showExit()
        {
            Console.WriteLine("Thanks for playing! Goodbye!");
        }
    }
}
