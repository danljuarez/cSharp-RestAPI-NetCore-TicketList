using RESTfulNetCoreWebAPI_TicketList.Models;

namespace RESTfulNetCoreWebAPI_TicketList.Tests.MSTest.Data
{
    public static class DataFactory
    {
        public static List<Ticket> CreateTicketList()
            => new List<Ticket>()
            {
                new Ticket()
                {
                    Id = 1,
                    EventName = "Test Event 01",
                    Description = "Test Event Description 01",
                    EventDate = DateTime.Now.AddDays(1),
                },
                new Ticket()
                {
                    Id = 2,
                    EventName = "Test Event 02",
                    Description = "Test Event Description 02",
                    EventDate = DateTime.Now
                },
                new Ticket()
                {
                    Id = 3,
                    EventName = "Test Event 03",
                    Description = "Test Event Description 03",
                    EventDate = DateTime.Now.AddDays(-3),
                },
                new Ticket()
                {
                    Id = 4,
                    EventName = "Test Event 04",
                    Description = "Test Event Description 04",
                    EventDate = DateTime.Now.AddDays(-5),
                }
            };

        public static Ticket CreateTicket()
            => new Ticket()
            {
                Id = 5,
                EventName = "Test Event Name",
                Description = "Test Event Description",
                EventDate = DateTime.Now
            };

        public static Ticket GetATicket()
            => new Ticket()
            {
                Id = 3,
                EventName = "Test Event 03",
                Description = "Test Event Description 03",
                EventDate = DateTime.Now
            };

        public static Ticket TicketToUpdate()
            => new Ticket()
            {
                Id = 2,
                EventName = "Test Event 02",
                Description = "Test Event Description 02",
                EventDate = DateTime.Now
            };

        public static string GetTestTicketNumber(int id = 1, DateTime? testEventDate = null)
        {
            var testDate = testEventDate ?? DateTime.Now;

            return $"{testDate.ToString("yyyyMMdd")}{id.ToString().PadLeft(5, '0')}";
        }

        public static Ticket AddTicket(DateTime? testEventDate = null)
        {
            var testDate = testEventDate ?? DateTime.Now;

            return new Ticket()
            {
                EventName = "New Test Event Name",
                Description = "New Test Event Description",
                EventDate = testDate
            };
        }

        public static Ticket TicketFieldsToUpdate()
            => new Ticket()
            {
                EventName = "Updated Test Event Name",
                Description = "Updated Test Event Description"
            };

        public static List<int> GetFirstSinglesFibonacciSequence()
            => [0, 1, 1, 2, 3, 5, 8];

        public static string[] GetPalindromeRequestList()
            => ["civic", "type", "radar"];

        public static List<string> GetPalindromeResponseList()
            => ["civic", "radar"];

        public static string[] GetAtLeastOnePalindromeItemWithOnlyOneChar()
            => ["madam", "a"];

        public static string[] GetAtLeastOnePalindromeItemWithOnlyANumber()
            => ["rotator", "2500"];

        public static string[] GetAtLeastOnePalindromeItemWithEmptyValue()
            => ["Kayak", ""];

        public static string?[]? GetAtLeastOnePalindromeItemWithNullValue()
            => ["Noon", null];
    }
}
