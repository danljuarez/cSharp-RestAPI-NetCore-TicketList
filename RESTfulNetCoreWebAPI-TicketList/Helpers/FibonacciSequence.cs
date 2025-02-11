namespace RESTfulNetCoreWebAPI_TicketList.Helpers
{
    public class FibonacciSequence : IFibonacciSequence
    {
        public List<int> CalculateFibonacci(int maxFibonacci = 55)
        {
            if (maxFibonacci < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var fiboSequence = new List<int>();

            var isNotFiboInitial = false;
            for (var i = 0; i < int.MaxValue; i++)
            {
                if (isNotFiboInitial)
                {
                    // Break loop if Fibonacci number is greater than maxFibonacci value
                    if ((fiboSequence[i - 1] + fiboSequence[i - 2]) > maxFibonacci)
                    {
                        break;
                    }
                    // Add Fibonacci number to list
                    fiboSequence.Add(fiboSequence[i - 1] + fiboSequence[i - 2]);
                }
                else
                {
                    // Break loop if max Fibonacci number is within initial numbers
                    if (i > maxFibonacci)
                    {
                        break;
                    }

                    // Only for initial Fibonacci numbers
                    fiboSequence.Add(i);
                    if (i == 1)
                    {
                        isNotFiboInitial = true;
                    }
                }
            }

            return fiboSequence;
        }
    }
}
