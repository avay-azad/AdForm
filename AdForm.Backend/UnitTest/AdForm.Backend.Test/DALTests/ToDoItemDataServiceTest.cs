using AdForm.DBService;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.DALTests
{
    public class ToDoItemDataServiceTest : ToDoAppDbContextInitiator
    {
        private readonly IToDoItemDataService _toDoItemDataService;

        public ToDoItemDataServiceTest()
        {
            _toDoItemDataService = new ToDoItemDataService(DBContext);
            DBContext.ToDoItems.Add(new ToDoItems
            {
                Name = "ToDoItem Test",
                UserId = 1,
                ToDoListId = 1
            });
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Get ToDoItem test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoItemsTest()
        {
            List<ToDoItems> lstToDoItem = await _toDoItemDataService.GetAllAsync(1);
            int count = lstToDoItem.Count;
            Assert.IsNotNull(lstToDoItem);
            Assert.IsTrue(count >= 1);
        }

        /// <summary>
        /// Get ToDoItem by id test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoItemByIdTest()
        {
            ToDoItems toDoItem = await _toDoItemDataService.GetByIdAsync(1, 1);
            Assert.IsNotNull(toDoItem);
            Assert.IsTrue(toDoItem.ToDoItemId == 1);
        }

        /// <summary>
        /// Get ToDoItem by Name test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoItemByNameTest()
        {
            ToDoItems toDoItem = await _toDoItemDataService.GetByNameAsync("ToDoItem Test", 1);
            Assert.IsNotNull(toDoItem);
            Assert.IsTrue(toDoItem.ToDoItemId == 1);
        }

        /// <summary>
        /// Add ToDoItem test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddToDoItemTest()
        {
            ToDoItems toDoItem = await _toDoItemDataService.AddAsync(new ToDoItems { Name = "Buy Laptop", UserId = 1, ToDoListId = 1 });
            Assert.IsNotNull(toDoItem);
            Assert.AreEqual("Buy Laptop", toDoItem.Name);
        }

        /// <summary>
        /// Test to update existing ToDoItem record.
        /// </summary>
        [Test]
        public async Task UpdateToDoItemTest()
        {
            ToDoItems toDoItem = await _toDoItemDataService.UpdateAsync(new ToDoItems { Name = "Sell Laptop", UserId = 1, ToDoItemId = 2 });
            Assert.IsNotNull(toDoItem);
            Assert.AreEqual("Sell Laptop", toDoItem.Name);
        }

        /// <summary>
        /// test to delete existing ToDoItem record.
        /// </summary>
        [Test]
        public async Task DeleteToDoItemTest()
        {
            ToDoItems toDoItem = await _toDoItemDataService.GetByIdAsync(2, 1);
            ToDoItems deletedToDoItem = await _toDoItemDataService.DeleteAsync(toDoItem);
            Assert.IsNotNull(deletedToDoItem);
        }
    }
}
