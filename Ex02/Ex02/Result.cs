using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class Result
    {
        public const string WinResult = "VVVV";
        public string       _result { get; }

        public Result(Pin pin,Pin target)
        {
            _result = calculateResult (pin._pinValue,  target._pinValue);
        }

        private string calculateResult(string i_guessValue, string i_targetValue)
        {
            int    bulls = 0;
            int    cows = 0; 
            bool[] usedInTarget = new bool[i_targetValue.Length];
            bool[] usedInGuess = new bool[i_guessValue.Length];

            for (int i = 0; i < i_guessValue.Length; i++)
            {
                if (i_guessValue[i] == i_targetValue[i])
                {
                    bulls++;
                    usedInGuess[i] = true;
                    usedInTarget[i] = true;
                }
            }
            
            for (int i = 0;i < i_guessValue.Length;i++)
            {
                if (!usedInGuess[i])
                {
                    for (int j = 0; j < i_guessValue.Length; j++)
                    {
                        if (!usedInTarget[j] && i_guessValue[i] == i_targetValue[j])
                        {
                            cows++;
                            usedInGuess[j] = true;
                            break;
                        }
                    }
                }
            }
            return new string ('V', bulls) + new string ('X', cows);
        }

        public string getResult()
        {
            return _result;
        }

    }
}
