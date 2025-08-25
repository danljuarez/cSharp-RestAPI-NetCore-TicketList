using Moq;
using RESTfulNetCoreWebAPI_TicketList.Models;
using RESTfulNetCoreWebAPI_TicketList.Repositories;
using RESTfulNetCoreWebAPI_TicketList.Services;

namespace RESTfulNetCoreWebAPI_TicketList.Tests.MSTest.Services
{
    [TestClass]
    public class TicketServiceTests
    {
        private readonly Mock<ITicketRepository> _ticketRepository = new();

        [TestMethod]
        public async Task GetTicketsAsync_Should_return_count_four_for_all_tickets_list()
        {
            // Arrange
            var expectedCount = 4;

            _ticketRepository
                .Setup(repository => repository.GetTicketsAsync())
                .ReturnsAsync(Data.DataFactory.CreateTicketList());

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act
            var result = await ticketService.GetTicketsAsync();

            // Assert
            Assert.AreEqual(expectedCount, result.Count);
        }

        [TestMethod]
        public async Task GetTicketAsync_Should_throw_ArgumentOutOfRangeException_When_ticket_id_is_zero()
        {
            // Arrange
            var id = 0;

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => ticketService.GetTicketAsync(id));
        }

        [TestMethod]
        public async Task GetTicketAsync_Should_not_return_null_ticket_object_When_ticket_exists()
        {
            // Arrange
            var id = 3;

            _ticketRepository
                .Setup(repository => repository.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync(Data.DataFactory.GetATicket());

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act
            var result = await ticketService.GetTicketAsync(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result?.Id);
            Assert.AreEqual("Test Event 03", result?.EventName);
            Assert.AreEqual("Test Event Description 03", result?.Description);
            _ticketRepository.Verify(_ => _.GetTicketAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task AddTicketAsync_Should_throw_ArgumentNullException_When_ticket_object_is_null()
        {
            // Arrange
            var ticketService = new TicketService(_ticketRepository.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => ticketService.AddTicketAsync(null));
        }

        [TestMethod]
        public async Task AddTicketAsync_Should_not_return_null_ticket_object_and_confirm_ticket_id()
        {
            // Arrange
            var expectedTicketId = 5;

            _ticketRepository
                .Setup(repository => repository.AddTicketAsync(It.IsAny<Ticket>()))
                .Verifiable();

            _ticketRepository
                .Setup(repository => repository.SaveAsync())
                .Verifiable();

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act
            var result = await ticketService.AddTicketAsync(Data.DataFactory.CreateTicket());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTicketId, result?.Id);
            _ticketRepository.VerifyAll();
        }

        [TestMethod]
        public async Task PatchTicketAsync_Should_not_return_null_ticket_object()
        {
            // Arrange
            _ticketRepository
                .Setup(repository => repository.UpdateTicket(It.IsAny<Ticket>()))
                .Verifiable();

            _ticketRepository
                .Setup(repository => repository.SaveAsync())
                .Verifiable();

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act
            var result = await ticketService.PatchTicketAsync(Data.DataFactory.TicketToUpdate());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Event 02", result?.EventName);
            _ticketRepository.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteTicketAsync_Should_throw_ArgumentOutOfRangeException_When_ticket_id_is_zero()
        {
            // Arrange
            var id = 0;

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => ticketService.DeleteTicketAsync(id));
        }

        [TestMethod]
        public async Task DeleteTicketAsync_Should_throw_KeyNotFoundException_When_GetTicket_method_returns_null()
        {
            // Arrange
            var id = 5;

            _ticketRepository
                .Setup(repository => repository.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync((Ticket?)null);

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => ticketService.DeleteTicketAsync(id));
        }

        [TestMethod]
        public async Task DeleteTicketAsync_Should_verify_that_all_called_methods_has_been_executed_once()
        {
            // Arrange
            var id = 3;

            _ticketRepository
                .Setup(repository => repository.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync(Data.DataFactory.GetATicket())
                .Verifiable();

            _ticketRepository
                .Setup(repository => repository.DeleteTicket(It.IsAny<Ticket>()))
                .Verifiable();

            _ticketRepository
                .Setup(repository => repository.SaveAsync())
                .Verifiable();

            var ticketService = new TicketService(_ticketRepository.Object);

            // Act
            await ticketService.DeleteTicketAsync(id);

            // Assert
            _ticketRepository.Verify(repository => repository.GetTicketAsync(It.IsAny<int>()), Times.Once);
            _ticketRepository.Verify(repository => repository.DeleteTicket(It.IsAny<Ticket>()), Times.Once);
            _ticketRepository.Verify(repository => repository.SaveAsync(), Times.Once);
        }
    }
}
