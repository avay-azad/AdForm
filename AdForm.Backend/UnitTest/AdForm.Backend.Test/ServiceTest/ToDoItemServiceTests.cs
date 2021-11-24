using AdForm.DBService;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Business;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.ServiceTest
{
    public class ToDoItemServiceTests : MapperInitiator
    {
        private Mock<IToDoItemDataService> _toDoItemDataService;
        private Mock<IToDoListDataService> _toDoListDataService;
        private IToDoItemAppService _toDoItemAppService;
        private readonly ToDoLists _toDoList = new ToDoLists { ToDoListId = 1, Name = "test" };
        private readonly ToDoItems _toDoItem = new ToDoItems { ToDoItemId = 1, Name = "test", ToDoListId = 1 };
        readonly List<ToDoItems> _toDoItems = new List<ToDoItems>();
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
            _toDoItemDataService = new Mock<IToDoItemDataService>();
            _toDoListDataService = new Mock<IToDoListDataService>();
            _toDoItemAppService = new ToDoItemAppService(_toDoItemDataService.Object, _toDoListDataService.Object, Mapper);
            _toDoItemDataService.Setup(p => p.AddAsync(It.IsAny<ToDoItems>())).Returns(Task.FromResult(_toDoItem));
            _toDoItemDataService.Setup(p => p.UpdateAsync(It.IsAny<ToDoItems>())).Returns(Task.FromResult(_toDoItem));
            _toDoItemDataService.Setup(p => p.DeleteAsync(It.IsAny<ToDoItems>())).Returns(Task.FromResult(_toDoItem));
            _toDoItemDataService.Setup(p => p.GetByIdAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_toDoItem));
            _toDoItemDataService.Setup(p => p.GetAllAsync(It.IsAny<long>())).Returns(Task.FromResult(_toDoItems));
            _toDoListDataService.Setup(p => p.GetByIdAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_toDoList));
        }

        /// <summary>
        /// Add ToDoList test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddToDoItemTest()
        {
            ToDoItemResponseDto result = await _toDoItemAppService.CreateAsync(new ToDoItemRequestDto() { ItemName = "test", ToDoListId = 1, UserId = 1 });
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoItemId);
        }
        [Test]
        public async Task UpdateToDoItemTest()
        {
            try
            {
                await _toDoItemAppService.UpdateAsync(1, new UpdateToDoItemRequestDto() { ItemName = "test", ToDoItemId = 1, UserId = 1 });
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }
        [Test]
        public async Task DeleteToDoItemTest()
        {
            try
            {
                await _toDoItemAppService.DeleteAsync(1, 1);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [Test]
        public async Task GetToDoItemById()
        {
            ToDoItemResponseDto result = await _toDoItemAppService.GetByIdAsync(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoItemId);
        }

        [Test]
        public async Task GetToDoItems()
        {
            PagedList<ToDoItemResponseDto> result = await _toDoItemAppService.GetAllAsync(paginationParameters, 1);
            Assert.IsNotNull(result);
        }
    }
}
