using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TicketManagerSystem.Api.Controllers;
using TicketManagerSystem.Api.Exceptions;
using TicketManagerSystem.Api.Models.DTO;
using TicketManagerSystem.Api.Models;
using TicketManagerSystem.Api.Repositories;

namespace TicketManagerSystem.UnitTests
{
    [TestClass]
    public class OrdersControllerTest
    {
        Mock<IOrderRepository> _orderRepositoryMock;
        Mock<IMapper> _mapperMoq;    
        List<Order> _moqList;
        List<OrderDTO> _dtoMoq;


        [TestInitialize]
        public void SetupMoqData()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapperMoq = new Mock<IMapper>();
            _moqList = new List<Order>
            {
                new Order {OrderId = 1,
                    NumberOfTickets = 2,
                    OrderedAt = DateTime.Now,
                    TotalPrice = 1500,
                    Customer = new Customer {CustomerId = 1},
                    CustomerId = 1,
                    TicketCategory = new TicketCategory {TicketCategoryId = 1},
                    TicketCategoryId = 1
                }
            };
            _dtoMoq = new List<OrderDTO>
            {
                new OrderDTO
                {
                    OrderID = 1,
                     NumberOfTickets = 2,

                    OrderedAt = DateTime.Now
                }
            };
        }

        [TestMethod]
        public async Task GetAllOrdersReturnListOfOrders()
        {
            //Arrange

            IReadOnlyList<Order> moqOrders = _moqList;
            Task<IReadOnlyList<Order>> moqTask = Task.Run(() => moqOrders);
            _orderRepositoryMock.Setup(moq => moq.GetAll()).Returns(_moqList);

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<OrderDTO>>(It.IsAny<Order>())).Returns(_dtoMoq);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);

            //Act
            var orders = controller.GetAll();
            var orderResult = orders.Result as OkObjectResult;
            var orderDtoList = orderResult.Value as IEnumerable<OrderDTO>;

            //Assert

            Assert.AreEqual(_moqList.Count, orderDtoList.Count());
        }

        [TestMethod]
        public async Task GetOrderByIdReturnNotFoundWhenNoRecordFound()
        {
            //Arrange
            int eventToFind = 11;
            _orderRepositoryMock.Setup(moq => moq.GetByOrderId(eventToFind)).ThrowsAsync(new EntityNotFoundException(eventToFind, nameof(Event)));

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);
            //Act

            var result = await controller.GetByOrderId(eventToFind);
            var orderResult = result.Result as NotFoundObjectResult;


            //Assert

            Assert.IsTrue(orderResult.StatusCode == 404);
        }

        [TestMethod]
        public async Task GetOrderByIdReturnFirstRecord()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetByOrderId(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<OrderDTO>(It.IsAny<Event>())).Returns(_dtoMoq.First());
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);
            //Act

            var result = await controller.GetByOrderId(1);
            var orderResult = result.Result as OkObjectResult;
            var orderCount = orderResult.Value as OrderDTO;

            //Assert

            Assert.IsFalse(int.Equals(0, orderCount.OrderID));
            Assert.AreEqual(1, orderCount.OrderID);
        }
    }
}
