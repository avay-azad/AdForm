using AdForm.DBService;
using AdForm.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace AdFormAssignment.DataService
{
    public class UserDataService : IUserDataService
    {

        private readonly HomeworkDBContext _dbContext;

        public UserDataService(HomeworkDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Users> GetUsers(Users user)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName && u.Password == user.Password);
            return result;
        }
    }
}
