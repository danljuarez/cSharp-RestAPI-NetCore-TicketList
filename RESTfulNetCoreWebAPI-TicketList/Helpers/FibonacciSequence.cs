namespace RESTfulNetCoreWebAPI_TicketList.Helpers
{
    public class FibonacciSequence : IFibonacciSequence
    {
        /// <summary>
        /// Generates a Fibonacci sequence up to the specified maximum value.
        /// </summary>
        /// <param name="maxFibonacci">The maximum value in the Fibonacci sequence. Defaults to 55</param>
        /// <returns>A list of Fibonacci numbers up to the specified maximum value.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxFibonacci"/> is less than 0.
        /// </exception>
        public List<int> CalculateFibonacci(int maxFibonacci = 55)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxFibonacci);

            var fibonacciSequence = new List<int> { 0, 1 };

            for (int i = 2; ; i++)
            {
                int nextFibonacci = fibonacciSequence[^1] + fibonacciSequence[^2];

                if (nextFibonacci > maxFibonacci)
                {
                    break;
                }

                fibonacciSequence.Add(nextFibonacci);
            }


            return fibonacciSequence;
        }
    }
}
