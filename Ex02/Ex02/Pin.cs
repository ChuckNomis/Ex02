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
            string     AllowedLetters = "ABCDEFGH";
            int        SequenceLength = 4;
            List<char> pool = AllowedLetters.ToList();
            char[]     chars = new char[SequenceLength];

            for (int i = 0; i < SequenceLength; i++)
            {
                int idx = _rng.Next(pool.Count);
                chars[i] = pool[idx];
                pool.RemoveAt(idx);
            }

            string result = new string(chars);
            return new Pin(result);
        }

    }
}
