using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTfulNetCoreWebAPI_TicketList.Controllers;
using RESTfulNetCoreWebAPI_TicketList.Extensions;
using RESTfulNetCoreWebAPI_TicketList.Models;
using RESTfulNetCoreWebAPI_TicketList.Models.Request;
using RESTfulNetCoreWebAPI_TicketList.Services;
using System.Net;

namespace RESTfulNetCoreWebAPI_TicketList.Tests.MSTest.Controllers
{
    [TestClass]
    public class TicketsControllerTests
    {
        private readonly Mock<ITicketService> _ticketService = new();
        private readonly Mock<IMapper> _mapper = new();

        [TestMethod]
        public async Task GetAllTicketsAsync_Should_return_statusCode_Ok_and_count_four_for_all_tickets_list()
        {
            // Arrange
            var expectedTicketsCount = 4;

            _ticketService
                .Setup(_ => _.GetTicketsAsync())
                .ReturnsAsync(Data.DataFactory.CreateTicketList())
                .Verifiable();

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.GetAllTicketsAsync();
            var result = (ObjectResult)actionResult;
            var tickets = result?.Value as List<Ticket>;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.AreEqual(expectedTicketsCount, tickets?.Count);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(result?.Value, typeof(List<Ticket>));
            Assert.AreEqual("Test Event 01", tickets?[0].EventName);
            Assert.AreEqual("Test Event 02", tickets?[1].EventName);
            _ticketService.Verify(ts => ts.GetTicketsAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetTicketById_Should_return_statusCode_ok_When_ticket_is_found()
        {
            // Arrange
            _ticketService
                .Setup(_ => _.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync(Data.DataFactory.GetATicket());

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.GetTicketById(It.IsAny<int>());
            var result = actionResult as OkObjectResult;
            var ticket = result?.Value as Ticket;

            var statusCode = result?.StatusCode;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, statusCode);
            Assert.IsInstanceOfType(result?.Value, typeof(Ticket));
            Assert.IsNotNull(ticket);
            Assert.AreEqual("Test Event 03", ticket?.EventName);
        }

        [TestMethod]
        public async Task GetTicketById_Should_return_statusCode_NotFound_When_GetTicket_returns_null()
        {
            // Arrange
            _ticketService
                .Setup(_ => _.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync((Ticket?)null);

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.GetTicketById(It.IsAny<int>());
            var result = actionResult as NotFoundResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, result?.StatusCode);
        }

        [TestMethod]
        public async Task GetTicketById_Should_return_statusCode_BadRequest_When_ArgumentOutOfRangeException_is_thrown()
        {
            // Arrange
            int id = 0;
            _ticketService
                .Setup(_ => _.GetTicketAsync(It.IsAny<int>()))
                .Throws(() => new ArgumentOutOfRangeException(nameof(id), "Parameter value cannot be cero or negative"));

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.GetTicketById(It.IsAny<int>());
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.AreEqual("Parameter value cannot be cero or negative (Parameter 'id')", result?.Value);
        }

        [TestMethod]
        public async Task PostTicket_Should_return_statusCode_Created_and_test_ticket_number_When_new_ticket_is_added()
        {
            // Arrange
            var expectedTicketId = 5;

            _mapper
                .Setup(_ => _.Map<Ticket>(It.IsAny<TicketInputDTO>()))
                .Returns(Data.DataFactory.CreateTicket());

            _ticketService
                .Setup(_ => _.AddTicketAsync(It.IsAny<Ticket>()))
                .ReturnsAsync(Data.DataFactory.CreateTicket());

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.PostTicket(It.IsAny<TicketInputDTO>());
            var result = actionResult as CreatedAtActionResult;
            var ticket = result?.Value as Ticket;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.Created, result?.StatusCode);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTicketId, ticket?.Id);
            Assert.AreEqual("Test Event Name", ticket?.EventName);
            Assert.AreEqual(Data.DataFactory.GetTestTicketNumber(expectedTicketId), ticket?.TicketNumber);
        }

        [TestMethod]
        public async Task PostTicket_Should_return_statusCode_BadRequest_When_ArgumentException_is_thrown()
        {
            // Arrange
            _ticketService
                .Setup(_ => _.AddTicketAsync(It.IsAny<Ticket>()))
                .Throws<ArgumentNullException>();

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.PostTicket(It.IsAny<TicketInputDTO>());
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.AreEqual("Value cannot be null.", result?.Value);
        }

        [TestMethod]
        public async Task PatchTicket_Should_update_ticket_confirm_changes_and_return_statusCode_Ok_When_ticket_is_found()
        {
            // Arrange
            var ticket = Data.DataFactory.GetATicket();
            var currentTicket = ticket.DeepCopy(); // DeepCopy() is an extension method created on this project

            _ticketService
                .Setup(_ => _.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync(ticket)
                .Verifiable();

            _ticketService
                .Setup(_ => _.PatchTicketAsync(It.IsAny<Ticket>()))
                .Verifiable();

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            var patchTicket = new JsonPatchDocument<Ticket>();
            patchTicket.Replace(_ => _.EventName, "Updated Test Event Name");
            patchTicket.Replace(_ => _.Description, "Updated Test Event Description");

            // Act
            var actionResult = await ticketsController.PatchTicket(It.IsAny<int>(), patchTicket);
            var result = actionResult as OkObjectResult;
            var updatedTicket = result?.Value as Ticket;

            var statusCode = result?.StatusCode;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, statusCode);
            Assert.AreEqual("Test Event 03", currentTicket.EventName);
            Assert.AreEqual("Test Event Description 03", currentTicket.Description);
            Assert.AreEqual("Updated Test Event Name", updatedTicket?.EventName);
            Assert.AreEqual("Updated Test Event Description", updatedTicket?.Description);
            _ticketService.VerifyAll();
        }

        [TestMethod]
        public async Task PatchTicket_Should_return_statusCode_BadRequest_When_ticket_id_is_zero()
        {
            // Arrange
            int id = 0;
            _ticketService
                .Setup(_ => _.GetTicketAsync(It.IsAny<int>()))
                .Throws(() => new ArgumentOutOfRangeException(nameof(id), "Parameter value cannot be cero or negative"));

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);
            var patchTicket = new JsonPatchDocument<Ticket>();

            // Act
            var actionResult = await ticketsController.PatchTicket(0, patchTicket);
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.AreEqual("Parameter value cannot be cero or negative (Parameter 'id')", result?.Value);
        }

        [TestMethod]
        public async Task PatchTicket_Should_return_statusCode_NotFound_When_GetTicket_returns_null()
        {
            // Arrange
            _ticketService
                .Setup(_ => _.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync((Ticket?)null);

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.PatchTicket(It.IsAny<int>(), It.IsAny<JsonPatchDocument<Ticket>>());
            var result = actionResult as NotFoundResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, result?.StatusCode);
        }

        [TestMethod]
        public async Task PatchTicket_Should_return_statusCode_InternalServerError_When_Exception_is_thrown()
        {
            // Arrange
            _ticketService
                .Setup(_ => _.GetTicketAsync(It.IsAny<int>()))
                .ReturnsAsync(Data.DataFactory.GetATicket())
                .Verifiable();

            _ticketService
                .Setup(_ => _.PatchTicketAsync(It.IsAny<Ticket>()))
                .Throws<Exception>()
                .Verifiable();

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);
            var patchTicket = new JsonPatchDocument<Ticket>();

            // Act
            var actionResult = await ticketsController.PatchTicket(It.IsAny<int>(), patchTicket);
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result?.StatusCode);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result?.Value);
            _ticketService.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteTicket_Should_return_statusCode_Ok_When_ticket_is_deleted()
        {
            // Arrange
            _ticketService
                .Setup(_ => _.DeleteTicketAsync(It.IsAny<int>()));

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.DeleteTicket(It.IsAny<int>());
            var result = (OkResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.IsNotNull(actionResult);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public async Task DeleteTicket_Should_return_statusCode_NotFound_When_KeyNotFoundException_is_thrown()
        {
            // Arrange
            _ticketService
                .Setup(_ => _.DeleteTicketAsync(It.IsAny<int>()))
                .Throws(() => new KeyNotFoundException("Ticket not found."));

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.DeleteTicket(It.IsAny<int>());
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, result?.StatusCode);
            Assert.AreEqual("Ticket not found.", result?.Value);
        }

        [TestMethod]
        public async Task DeleteTicket_Should_return_statusCode_BadRequest_When_ArgumentOutOfRangeException_is_thrown()
        {
            // Arrange
            int id = 0;
            _ticketService
                .Setup(_ => _.DeleteTicketAsync(It.IsAny<int>()))
                .Throws(() => new ArgumentOutOfRangeException(nameof(id), "Parameter value cannot be cero or negative"));

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.DeleteTicket(It.IsAny<int>());
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result?.StatusCode);
            Assert.AreEqual("Parameter value cannot be cero or negative (Parameter 'id')", result?.Value);
        }

        [TestMethod]
        public async Task DeleteTicket_Should_return_statusCode_InternalServerError_When_Exception_is_thrown()
        {
            _ticketService
                .Setup(_ => _.DeleteTicketAsync(It.IsAny<int>()))
                .Throws<Exception>();

            var ticketsController = new TicketsController(_ticketService.Object, _mapper.Object);

            // Act
            var actionResult = await ticketsController.DeleteTicket(It.IsAny<int>());
            var result = (ObjectResult)actionResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result?.StatusCode);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result?.Value);
        }
    }
}
