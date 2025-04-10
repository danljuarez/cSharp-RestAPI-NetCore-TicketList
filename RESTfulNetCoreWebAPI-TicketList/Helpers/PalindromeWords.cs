using System.Text.RegularExpressions;

namespace RESTfulNetCoreWebAPI_TicketList.Helpers
{
    public class PalindromeWords : IPalindromeWords
    {
        private const string ALLOWED_WORD_PATTERN = @"^(?![0-9]+$)[a-zA-Z0-9_-]{2,}$"; // Pattern that ensures the word contains at least two characters and does not consist only of numbers.
        private const string TRY_SUGGESTION = "Try with ['civic', 'type', 'radar'], will respond with ['civic', 'radar']";

        /// <summary>
        /// Retrieves a list of palindrome words from the given list of words.
        /// </summary>
        /// <param name="words">The list of words to check for palindromes.</param>
        /// <returns>A list of palindrome words.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input list or an element is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the input list contains at least one element that is not allowed.</exception>
        public List<string> GetPalindromeWords(string[]? words)
        {
            if (words == null || words.Length == 0)
            {
                throw new ArgumentNullException($"{nameof(words)} list is null or empty. {TRY_SUGGESTION}");
            }

            if (!words.All(IsWordAllowed))
            {
                throw new ArgumentException($"{nameof(words)} list contains at least one element that is not allowed. {TRY_SUGGESTION}");
            }

            return words.Where(IsPalindrome).ToList();
        }

        private bool IsWordAllowed(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException($"Word cannot be null or empty.");
            }

            return Regex.IsMatch(word, ALLOWED_WORD_PATTERN);
        }

        private bool IsPalindrome(string word)
        {
            var reversedWord = new string(word.Reverse().ToArray());
            return word.ToLower() == reversedWord.ToLower();
        }
    }
}
