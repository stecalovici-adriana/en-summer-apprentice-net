using Moq;
using System;
using System.Collections;
using TicketManagerSystem.Api.Models;
using TicketManagerSystem.Api.Models.DTO;
using AutoMapper;
using TicketManagerSystem.Api.Repositories;
using TicketManagerSystem.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using TicketManagerSystem.Api.Exceptions;

namespace TicketManagerSystem.UnitTests
{
    [TestClass]
    public class EventsControllerTest
    {
        Mock<IEventRepository> _eventRepositoryMock;
        Mock<IMapper> _mapperMoq;
        List<Event> _moqList;
        List<EventDTO> _dtoMoq;

        [TestInitialize]
        public void SetupMoqData()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _mapperMoq = new Mock<IMapper>();
            _moqList = new List<Event>
            {
                new Event {EventId = 1,
                    EventName = "Moq name",
                    EventDescription = "Moq description",
                    EndDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    EventType = new EventType {EventTypeId = 1,EventTypeName="test event type"},
                    EventTypeId = 1,
                    Venue = new Venue {VenueId = 1,Capacity = 12, Location = "Mock location",Type = "mock type"},
                    VenueId = 1
                }
            };
            _dtoMoq = new List<EventDTO>
            {
                new EventDTO
                {
                    EventDescription = "Moq description",
                    EventID = 1,
                    EventName = "Moq name",
                    EventType = new EventType {EventTypeId = 1,EventTypeName="test event type"}.EventTypeName,
                    Venue = new Venue {VenueId = 1,Capacity = 12, Location = "Mock location",Type = "mock type"}.Location
                }
            };
        }

        [TestMethod]
        public async Task GetAllEventsReturnListOfEvents()
        {
            //Arrange

            IReadOnlyList<Event> moqEvents = _moqList;
            Task<IReadOnlyList<Event>> moqTask = Task.Run(() => moqEvents);
            _eventRepositoryMock.Setup(moq => moq.GetAll()).Returns(_moqList);

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<EventDTO>>(It.IsAny<Event>())).Returns(_dtoMoq);

            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object);

            //Act
            var events = controller.GetAll();
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            //Assert

            Assert.AreEqual(_moqList.Count, eventDTOList.Count());
        }

        [TestMethod]
        public async Task GetEventByIdReturnNotFoundWhenNoRecordFound()
        {
            //Arrange
            int eventToFind = 11;
            _eventRepositoryMock.Setup(moq => moq.GetByEventId(eventToFind)).ThrowsAsync(new EntityNotFoundException(eventToFind, nameof(Event)));
            //_mapperMoq.Setup(moq => moq.Map<IEnumerable<EventDTO>>(It.IsAny<IReadOnlyList<Event>>())).Returns((IEnumerable<EventDTO>)null);
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object);
            //Act

            var result = await controller.GetByEventId(eventToFind);
            var eventResult = result.Result as NotFoundObjectResult;


            //Assert

            Assert.IsTrue(eventResult.StatusCode == 404);
        }

        [TestMethod]
        public async Task GetEventByIdReturnFirstRecord()
        {
            //Arrange
            _eventRepositoryMock.Setup(moq => moq.GetByEventId(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<EventDTO>(It.IsAny<Event>())).Returns(_dtoMoq.First());
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object);
            //Act

            var result = await controller.GetByEventId(1);
            var eventResult = result.Result as OkObjectResult;
            var eventCount = eventResult.Value as EventDTO;

            //Assert

            Assert.IsFalse(string.IsNullOrEmpty(eventCount.EventName));
            Assert.AreEqual(1, eventCount.EventID);
        }

       /*[TestMethod]
        public async Task GetEventByIDThrowsAnException()
        {
            //Arrange
            _eventRepositoryMock.Setup(moq => moq.GetByEventId(It.IsAny<int>())).Throws<Exception>();
            _mapperMoq.Setup(moq => moq.Map<EventDTO>(It.IsAny<Event>())).Returns(_dtoMoq.First());
            var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object);
            //Act

            var result = await controller.GetById(1);

            //Assert

            Assert.IsNull(result);
        }*/
    }
}
