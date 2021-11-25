using AdForm.DBService;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Business;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.ServiceTest
{
    public class LabelServiceTest : MapperInitiator
    {
        private Mock<ILabelDataService> _labelDbService;
        private ILableAppService _lableAppService;
        private readonly Labels _label = new Labels { LabelId = 1, Name = "test" };
        readonly List<Labels>  _labels = new List<Labels>();
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
            _labelDbService = new Mock<ILabelDataService>();
            _lableAppService = new LableAppService(_labelDbService.Object, Mapper);
            _labelDbService.Setup(p => p.AddAsync(It.IsAny<Labels>())).Returns(Task.FromResult(_label));
            _labelDbService.Setup(p => p.DeleteAsync(It.IsAny<Labels>())).Returns(Task.FromResult(_label));
            _labelDbService.Setup(p => p.GetByIdAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_label));
            _labelDbService.Setup(p => p.GetAllAsync(It.IsAny<long>())).Returns(Task.FromResult(_labels));
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabelTest()
        {
            LabelResponseDto result = await _lableAppService.CreateAsync(new LabelRequestDto());
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.LabelId);
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteLabelTest()
        {
            try
            {
                await _lableAppService.DeleteAsync(1, 1);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        /// <summary>
        /// get lable by Id test
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabelById()
        {
            LabelResponseDto result = await _lableAppService.GetByIdAsync(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.LabelId);
        }

        /// <summary>
        /// Get labels
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabels()
        {
            PagedList<LabelResponseDto> result = await _lableAppService.GetAllAsync(paginationParameters, 1);
            Assert.IsNotNull(result);
        }
    }
}
