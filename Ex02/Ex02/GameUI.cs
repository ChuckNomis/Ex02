using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace Ex02
{
    internal class GameUI
    {
        public struct ListOfPinsAndResult
        {
            public string Pins;
            public string Result; 
        }

        private List<ListOfPinsAndResult> showAttempts;



        public void   InitializeBoard(int i_maxAttempts)
        {
            showAttempts = new List<ListOfPinsAndResult>();

            showAttempts.Add(new ListOfPinsAndResult { Pins = "####", Result = "" });

            for (int i = 0; i < maxAttempts; i++) 
            {
                showAttempts.Add(new ListOfPinsAndResult { Pins = "", Result = "" }); 
            }
        }

        public void   addGuessAndResult(int i_attemptIndex, string i_guess, string i_feedback)
        {
            showAttempts[attemptIndex + 1] = new ListOfPinsAndResult { Pins = guess, Result = feedback };
        }

        public void   printTheBoard()
        {
            string pins;
            string result; 

            Console.WriteLine("Current board status: ");
            Console.WriteLine("|Pins:  |Result:  |");
            Console.WriteLine("===================");

            foreach (ListOfPinsAndResult attemp in showAttempts)
            {
                pins = string.IsNullOrEmpty(attemp.Pins) ? "       " : attemp.Pins.PadRight(7);
                result = string.IsNullOrEmpty(attemp.Result) ? "         " : attemp.Result.PadRight(9);
                Console.WriteLine($"|{pins}|{result}|");
                Console.WriteLine("===================");
            }
        }

        public string getUserGuess()
        {
            string input = Console.ReadLine().ToUpper(); 
            return input;
        }

        public int    getNumberOfGuesses()
        {
            int    numberOfGuesses;
            string input;

            Console.WriteLine("Enter a number of guesses between 4 and 10")

            while (true)
            {
                input = Console.ReadLine();

                if (int.TryParse(input, out numberOfGuesses) && numberOfGuesses >= 4 && numberOfGuesses <= 10)
                {
                    return numberOfGuesses;
                }

                Console.WriteLine("Invalid number. Enter a number between 4 and 10."); 
            }
        }

        public void   PrintMessage(string i_type)
        {
            switch (type) {
                case "PromptForGuess":
                    Console.WriteLine("Enter your guess(4 letters A-H): "); break;

                case "InvalidInpt":
                    Console.WriteLine("Invalid input! Must be 4 different letters A-H."); break;

                case "victory":
                    Console.WriteLine("Congratulations! You won!"); break;

                case "Defeat":
                    Console.WriteLine("You've run out of attempts, You lose!"); break;

                case "Exit":
                    Console.WriteLine("Goodbye!"); break;
            }
        }

        public void   clearScreen()
        {
            Screen.Clear(); 
        }
    }
}
