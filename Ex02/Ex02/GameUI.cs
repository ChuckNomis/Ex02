using System;
using System.Collections.Generic;
using System.Linq;
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

        public void ClearScreen()
        {
            Screen.Clear();
        }

        public void PrintTheBoard(List<Pin> pinsHistory, List<Result> feedbackHistory)
        {
            string horizontalLine = $"|{new string('=', GameConstants.k_PinsColWidth + 2)}|{new string('=', GameConstants.k_ResultColWidth + 2)}|";

            Console.WriteLine($"| {"Pins:",-GameConstants.k_PinsColWidth} | {"Result:",-GameConstants.k_ResultColWidth} |");
            Console.WriteLine(horizontalLine);
            Console.WriteLine($"| {"# # # #",-GameConstants.k_PinsColWidth} | {"",-GameConstants.k_ResultColWidth} |");
            Console.WriteLine(horizontalLine);

            for (int row = 0; row < _maxGuesses; row++)
            {
                string pinsCell = "";
                string resultCell = "";

                if (row < pinsHistory.Count)
                {
                    pinsCell = string.Join(" ", pinsHistory[row]._pinValue.ToCharArray());
                    resultCell = string.Join(" ", feedbackHistory[row]._result.ToCharArray());
                }

                Console.WriteLine($"| {pinsCell,-GameConstants.k_PinsColWidth} | {resultCell,-GameConstants.k_ResultColWidth} |");
                Console.WriteLine(horizontalLine);
            }
        }



        public Pin  GetUserGuess()
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
            int           k_ExpectedLength = GameConstants.k_ExpectedLength;
            string        allowedLetters = GameConstants.k_AllowedLetters;
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

        public void ShowWin()
        {
            Console.WriteLine("Congratulations! You cracked the code!");
        }

        public void ShowLose(string i_targetValue)
        {
            Console.WriteLine("You've run out of attempts, you lose!");
            Console.WriteLine($"the target was: {i_targetValue}");
        }

        public void ShowPlayAgain(ref bool keepPlaying)
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
            
        public void ShowExit()
        {
            Console.WriteLine("Thanks for playing! Goodbye!");
        }
    }
}
