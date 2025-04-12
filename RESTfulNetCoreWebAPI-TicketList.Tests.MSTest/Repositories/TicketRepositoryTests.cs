using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RESTfulNetCoreWebAPI_TicketList.Data;
using RESTfulNetCoreWebAPI_TicketList.Extensions;
using RESTfulNetCoreWebAPI_TicketList.Models;
using RESTfulNetCoreWebAPI_TicketList.Repositories;

namespace RESTfulNetCoreWebAPI_TicketList.Tests.MSTest.Repositories
{
    [TestClass]
    public class TicketRepositoryTests
    {
        private TicketContext? _dbContext;
        private TicketRepository? _ticketRepository;
        private DbContextOptionsBuilder<TicketContext>? optionBuilder;

        [TestInitialize]
        public void Initialize()
        {
            optionBuilder = new DbContextOptionsBuilder<TicketContext>();
            optionBuilder.UseInMemoryDatabase("TicketInMemoryDBTest", new InMemoryDatabaseRoot());
            _dbContext = new TicketContext(optionBuilder.Options);

            _dbContext?.Tickets.AddRange(Data.DataFactory.CreateTicketList());
            _dbContext?.SaveChanges();

            _ticketRepository = new TicketRepository(_dbContext ?? throw new ArgumentNullException());
        }

        [TestCleanup]
        public void CleanUp()
        {
            _dbContext = null;
            _ticketRepository = null;
            optionBuilder = null;
        }

        [TestMethod]
        public async Task GetTicketsAsync_Should_confirm_ticket_list_instance_type_and_list_result_count()
        {
            // Arrange
            var ticketCount = 4;

            // Act
            var result = await _ticketRepository!.GetTicketsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<List<Ticket>>(result);
            Assert.AreEqual(ticketCount, result?.Count);
        }

        [TestMethod]
        public void GetTicket_Should_confirm_ticket_instance_type_and_requested_ticket_id()
        {
            // Arrange
            var ticketId = 1;

            // Act
            var result = _ticketRepository?.GetTicket(ticketId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Ticket));
            Assert.AreEqual(ticketId, result.Id);
        }

        [TestMethod]
        public async Task AddTicket_Should_confirm_ticket_instance_type_and_new_added_ticket()
        {
            // Arrange
            var expectedListCount = 4;
            var currentCount = (await _ticketRepository!.GetTicketsAsync())?.Count ?? 0;

            // Act
            var result = _ticketRepository?.AddTicket(Data.DataFactory.AddTicket(DateTime.Now.AddDays(-2)));
            _dbContext?.SaveChanges();

            var resultId = result?.Id ?? throw new ArgumentNullException();
            var resultCount = (await _ticketRepository!.GetTicketsAsync())?.Count ?? 0;

            // Assert
            Assert.IsInstanceOfType<Ticket>(result);
            Assert.AreEqual("New Test Event Name", result.EventName);
            Assert.AreEqual("New Test Event Description", result.Description);
            Assert.AreEqual(
                Data.DataFactory.GetTestTicketNumber(resultId, DateTime.Now.AddDays(-2)),
                result.TicketNumber
            );
            Assert.AreEqual(expectedListCount, currentCount);
            Assert.AreEqual(expectedListCount + 1, resultCount);
        }

        [TestMethod]
        public void UpdateTicket_Should_update_ticket_id_number_three_and_confirm_applied_changes()
        {
            // Arrange
            var ticketId = 3;

            var ticket = _ticketRepository?.GetTicket(ticketId);
            var currentTicketResult = ticket.DeepCopy(); // DeepCopy() is an extension method created on this project

            var ticketToUpdate = Data.DataFactory.TicketFieldsToUpdate();

            if (ticket != null)
            {
                ticket.EventName = ticketToUpdate.EventName;
                ticket.Description = ticketToUpdate.Description;
            }

            // Act
            _ticketRepository?.UpdateTicket(ticket ?? throw new ArgumentNullException());
            _dbContext?.SaveChanges();

            var ticketResult = _ticketRepository?.GetTicket(ticketId);

            // Assert
            Assert.AreEqual(ticketId, currentTicketResult?.Id);
            Assert.AreEqual("Test Event 03", currentTicketResult?.EventName);
            Assert.AreEqual("Test Event Description 03", currentTicketResult?.Description);

            Assert.AreEqual(ticketId, ticketResult?.Id);
            Assert.AreEqual("Updated Test Event Name", ticketResult?.EventName);
            Assert.AreEqual("Updated Test Event Description", ticketResult?.Description);
        }

        [TestMethod]
        public async Task DeleteTicket_Should_delete_ticket_id_number_three_and_confirm_ticket_no_longer_exists()
        {
            // Arrange
            var ticketId = 3;
            var expectedCount = 4;
            var currentCount = (await _ticketRepository!.GetTicketsAsync())?.Count ?? 0;
            var ticket = _ticketRepository?.GetTicket(ticketId);

            // Act
            _ticketRepository?.DeleteTicket(ticket ?? throw new ArgumentNullException());
            _dbContext?.SaveChanges();

            var resultCount = (await _ticketRepository!.GetTicketsAsync())?.Count ?? 0;
            var ticketResult = _ticketRepository?.GetTicket(ticketId);

            // Assert
            Assert.AreEqual(expectedCount, currentCount);
            Assert.IsNotNull(ticket);
            Assert.AreEqual(expectedCount - 1, resultCount);
            Assert.IsNull(ticketResult);
        }

        [TestMethod]
        public async Task SaveAsync_Should_confirm_new_ticket_has_been_added_When_method_is_called()
        {
            // Arrange
            var expectedCount = 4;
            var currentCount = (await _ticketRepository!.GetTicketsAsync())?.Count ?? 0;
            var result = _ticketRepository?.AddTicket(Data.DataFactory.AddTicket());

            // Act
            _ticketRepository?.SaveAsync().Wait();
            var resultCount = (await _ticketRepository!.GetTicketsAsync())?.Count ?? 0;

            // Assert
            Assert.IsInstanceOfType<Ticket>(result);
            Assert.AreEqual(expectedCount, currentCount);
            Assert.AreEqual(expectedCount + 1, resultCount);
        }
    }
}
