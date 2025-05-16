using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class Pin
    {
        public string _pinValue { get; }

        public Pin(string Pin)
        {
            _pinValue = Pin; 
        }

        public static Pin GenerateTargetPin()
        {
            Random     _rng = new Random();
            string allowedLetters = GameConstants.k_AllowedLetters;  
            int        SequenceLength = GameConstants.k_ExpectedLength;
            List<char> pool = allowedLetters.ToList();
            char[]     chars = new char[SequenceLength];
            int idx;
            string result;

            for (int i = 0; i < SequenceLength; i++)
            {
                idx = _rng.Next(pool.Count);
                chars[i] = pool[idx];
                pool.RemoveAt(idx);
            }

            result = new string(chars);
            return new Pin(result);
        }

    }
}
