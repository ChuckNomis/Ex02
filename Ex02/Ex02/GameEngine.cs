using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex02
{
    public class GameEngine
    {
        private int             _maxAttempts = 0;
        private GameUI          _ui = null;
        private static Random   _random = new Random();
        private const int       _sequenceLength = 4;

        public GameEngine(int maxAttempts)
        {
            _maxAttempts = AskForMaxAttempts();
            _ui =          new GameUI();            
        }
        public void StartGame()
        {
            do
            {
                _ui.ClearScreen();
                _ui.ShowWelcome();

                // 1) Generate the secret code and clear history
                string secret = GenerateTargetWord();
                var history = new List<Guess>();

                // 2) Main loop: up to _maxAttempts
                while (history.Count < _maxAttempts)
                {
                    _ui.RenderBoard(history, _maxAttempts);

                    string guess = _ui.ReadGuess();
                    if (guess.Equals("Q", StringComparison.OrdinalIgnoreCase))
                        return;  // exit game entirely

                    if (!TryValidateGuess(guess, out string error))
                    {
                        _ui.ShowInvalidGuess(error);
                        continue;
                    }

                    string feedback = Compare(guess, secret);
                    history.Add(new Guess(guess, feedback));
                    _ui.ShowFeedback(feedback);

                    // 3) Check win condition
                    if (feedback == new string('V', SequenceLength))
                    {
                        _ui.ShowWin(history.Count, secret);
                        break;
                    }
                }

                // 4) If we ran out of attempts without winning
                if (history.Count >= _maxAttempts
                    && history.Last().Feedback != new string('V', SequenceLength))
                {
                    _ui.ShowLose();
                }

            } while (_ui.AskPlayAgain());

            _ui.ShowGoodbye();
        }
        private static string GenerateTargetWord() // Generates a target word for the game
        {
            const string letters = "ABCDEFGH";      // letters A–H
            var pool = letters.ToList();            // Convert to a list so we can pick without repeats
            var codeChars = new char[4];

            for (int i = 0; i < 4; i++)
            {
                int idx = _random.Next(pool.Count); // pick a random index in the pool
                codeChars[i] = pool[idx];
                pool.RemoveAt(idx);                 // Removes them from the pool
            }

            return new string(codeChars);
        }
        private bool TryValidateGuess(string input, out string errorMessage) // Returns true if valid guess or false and errorMessage if not.
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = "Guess cannot have white spaces.";
                return false;
            }

            if (input.Length != 4)
            {
                errorMessage = "Guess must be 4 letters.";
                return false;
            }

            char[] chars = input.ToUpper().ToCharArray();
            // Letter range A–H
            foreach (var c in chars)
            {
                if (c < 'A' || c > 'H')
                {
                    errorMessage = "Allowed letters are A–H.";
                    return false;
                }
            }

            // No duplicates
            if (chars.Distinct().Count() != chars.Length)
            {
                errorMessage = "No duplicate letters allowed.";
                return false;
            }

            return true;
        }
        private int AskForMaxAttempts()
        {
            while (true)
            {
                string raw = _ui.ReadMaxAttemptsInput();

                // Check if its an integer
                if (!int.TryParse(raw, out int attempts))
                {
                    _ui.ShowInvalidMax("That’s not a number. Please enter digits only.");
                    continue;
                }

                //Check the allowed range
                if (attempts < 10 || attempts > 100)
                {
                    _ui.ShowInvalidMax("Number must be between 10 and 100.");
                    continue;
                }

                // OK!
                return attempts;
            }
        }

    }

}
