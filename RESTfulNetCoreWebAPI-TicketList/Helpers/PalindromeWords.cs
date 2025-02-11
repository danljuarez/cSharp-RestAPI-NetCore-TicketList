using System.Text.RegularExpressions;

namespace RESTfulNetCoreWebAPI_TicketList.Helpers
{
    public class PalindromeWords : IPalindromeWords
    {
        private const string DO_NOT_ALLOW_SANITIZED_WORDS_WITH_JUST_NUMBERS = @"^(?![0-9]+$)[a-zA-Z0-9_-]{2,}$";

        public List<string> GetPalindromeWords(string[]? words)
        {
            var trySuggestion = "Try with ['civic', 'type', 'radar'], will respond with ['civic', 'radar']";

            if (words == null || words.Length == 0)
            {
                throw new ArgumentNullException($"{nameof(words)} list is null or empty. {trySuggestion}");
            }

            // Detect if each of the words in the list is allowed
            if (!IsEachWordAllowed(words))
            {
                throw new ArgumentException($"{nameof(words)} list contain at least an element that is not allowed. {trySuggestion}");
            }

            var palindromeWords = new List<string>();
            foreach (var word in words)
            {
                // Reverse word
                var reversedWord = "";
                for (var i = word.Length - 1; i >= 0; i--)
                {
                    reversedWord += word.Substring(i, 1);
                }

                // Determine if it is a palindrome word
                if (word.ToLower() == reversedWord.ToLower())
                {
                    palindromeWords.Add(word);
                }
            }

            return palindromeWords;
        }

        private bool IsASanitizedWordWithoutJustNumbers(string word)
        {
            var regex = new Regex(DO_NOT_ALLOW_SANITIZED_WORDS_WITH_JUST_NUMBERS);

            return regex.IsMatch(word);
        }

        private bool IsEachWordAllowed(string[] words)
        {
            foreach (var word in words)
            {
                if (string.IsNullOrEmpty(word))
                {
                    throw new ArgumentNullException($"{nameof(word)} cannot be null or empty.");
                }

                if (!IsASanitizedWordWithoutJustNumbers(word))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
