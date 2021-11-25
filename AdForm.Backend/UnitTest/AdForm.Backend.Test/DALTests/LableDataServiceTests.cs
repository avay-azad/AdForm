using AdForm.DBService;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.DALTests
{
    public class LableDataServiceTests : ToDoAppDbContextInitiator
    {
        private readonly ILabelDataService _labelDataService;

        public LableDataServiceTests()
        {
            _labelDataService = new LabelDataService(DBContext);
            DBContext.Labels.Add(new Labels
            {
                Name = "something",
                UserId = 1,
            });
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Get labels test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabelsTest()
        {
            List<Labels> LabelList = await _labelDataService.GetAllAsync(1);
            int count = LabelList.Count;
            Assert.IsNotNull(LabelList);
            Assert.IsTrue(count >= 1);
        }

        /// <summary>
        /// Get label by lable id test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabelByIdTest()
        {
            Labels label = await _labelDataService.GetByIdAsync(1, 1);
            Assert.IsNotNull(label);
            Assert.IsTrue(label.LabelId == 1);
        }

        /// <summary>
        /// Get label by lable Name test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabelByNameTest()
        {
            Labels label = await _labelDataService.GetByNameAsync("something", 1);
            Assert.IsNotNull(label);
            Assert.IsTrue(label.Name == "something");
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabelTest()
        {
            Labels addedLabel = await _labelDataService.AddAsync(new Labels { Name = "blue", UserId = 1 });
            Assert.IsNotNull(addedLabel);
            Assert.AreEqual("blue", addedLabel.Name);
        }

        /// <summary>
        /// test to delete existing Label record.
        /// </summary>
        [Test]
        public async Task DeleteLabelTest()
        {
            Labels label = await _labelDataService.GetByNameAsync("blue", 1);
            Labels deletedLabel = await _labelDataService.DeleteAsync(label);
            Assert.IsNotNull(deletedLabel);
            Assert.AreEqual("blue", deletedLabel.Name);
        }
    }
}
