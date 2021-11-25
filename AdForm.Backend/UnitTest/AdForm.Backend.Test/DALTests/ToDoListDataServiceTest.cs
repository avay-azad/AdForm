using AdForm.DBService;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.DALTests
{
    public class ToDoListDataServiceTest : ToDoAppDbContextInitiator
    {
        private readonly IToDoListDataService _toDoListDataService;

        public ToDoListDataServiceTest()
        {
            _toDoListDataService = new ToDoListDataService(DBContext);
            DBContext.ToDoLists.Add(new ToDoLists
            {
                Name = "ToDoLis Test",
                UserId = 1
            });
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Get ToDoList test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoListsTest()
        {
            List<ToDoLists> lstToDoList = await _toDoListDataService.GetAllAsync(1);
            int count = lstToDoList.Count;
            Assert.IsNotNull(lstToDoList);
            Assert.IsTrue(count >= 1);
        }
      
        /// <summary>
        /// Get ToDoList by id test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoListByIdTest()
        {
            ToDoLists toDoList = await _toDoListDataService.GetByIdAsync(1, 1);
            Assert.IsNotNull(toDoList);
            Assert.IsTrue(toDoList.ToDoListId == 1);
        }

        /// <summary>
        /// Get ToDoList by Name test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoListByNameTest()
        {
            ToDoLists toDoList = await _toDoListDataService.GetByNameAsync("ToDoLis Test", 1);
            Assert.IsNotNull(toDoList);
            Assert.IsTrue(toDoList.ToDoListId == 1);
        }

        /// <summary>
        /// Add ToDoList test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddToDoList()
        {
            ToDoLists todoList = await _toDoListDataService.AddAsync(new ToDoLists { Name = "Buy Laptop", UserId = 1 });
            Assert.IsNotNull(todoList);
            Assert.AreEqual("Buy Laptop", todoList.Name);
        }

        /// <summary>
        /// Test to update existing ToDoList record.
        /// </summary>
        [Test]
        public async Task UpdateToDoList()
        {
            ToDoLists todoList = await _toDoListDataService.UpdateAsync(new ToDoLists { Name = "Sell Laptop", UserId = 1, ToDoListId = 2 });
            Assert.IsNotNull(todoList);
            Assert.AreEqual("Sell Laptop", todoList.Name);
        }

        /// <summary>
        /// test to delete existing ToDoList record.
        /// </summary>
        [Test]
        public async Task DeleteToDoList()
        {
            ToDoLists todoList = await _toDoListDataService.GetByIdAsync(2, 1);
            ToDoLists deletedTodoList = await _toDoListDataService.DeleteAsync(todoList);
            Assert.IsNotNull(deletedTodoList);
        }
    }
}
