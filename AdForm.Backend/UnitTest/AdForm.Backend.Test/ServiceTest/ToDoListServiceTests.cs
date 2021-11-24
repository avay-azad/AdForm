using AdForm.DBService;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Business;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.ServiceTest
{
    public class ToDoListServiceTests : MapperInitiator
    {
        private Mock<IToDoListDataService> _toDoListDataService;
        private IToDoListAppService _toDoListAppService;
        private readonly ToDoLists _toDoList = new ToDoLists { ToDoListId = 1, Name = "test" };
        readonly List<ToDoLists> _toDoLists = new List<ToDoLists>();
        readonly PaginationParameters paginationParameters = new PaginationParameters()
        {
            PageNumber = 1,
            PageSize = 10,
            SearchText = "test"
        };
        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _toDoListDataService = new Mock<IToDoListDataService>();
            _toDoListAppService = new ToDoListAppService(_toDoListDataService.Object, Mapper);
            _toDoListDataService.Setup(p => p.AddAsync(It.IsAny<ToDoLists>())).Returns(Task.FromResult(_toDoList));
            _toDoListDataService.Setup(p => p.UpdateAsync(It.IsAny<ToDoLists>())).Returns(Task.FromResult(_toDoList));
            _toDoListDataService.Setup(p => p.DeleteAsync(It.IsAny<ToDoLists>())).Returns(Task.FromResult(_toDoList));
            _toDoListDataService.Setup(p => p.GetByIdAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_toDoList));
            _toDoListDataService.Setup(p => p.GetAllAsync(It.IsAny<long>())).Returns(Task.FromResult(_toDoLists));
        }

        /// <summary>
        /// Add ToDoList test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddToDoListTest()
        {
            ToDoListResponseDto result = await _toDoListAppService.CreateAsync(new ToDoListRequestDto() { ListName = "test", UserId = 1 });
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoListId);
        }
        [Test]
        public async Task UpdateToDoListTest()
        {
            try
            {
                await _toDoListAppService.UpdateAsync(1, new ToDoListRequestDto() { ListName = "test", UserId = 1 });
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }
        [Test]
        public async Task DeleteToDoListTest()
        {
            try
            {
                await _toDoListAppService.DeleteAsync(1, 1);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [Test]
        public async Task GetToDoListById()
        {
            ToDoListResponseDto result = await _toDoListAppService.GetAsync(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoListId);
        }

        [Test]
        public async Task GetToDoLists()
        {
            PagedList<ToDoListResponseDto> result = await _toDoListAppService.GetAsync(paginationParameters, 1);
            Assert.IsNotNull(result);
        }
    }
}
