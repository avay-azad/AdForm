using AdForm.DBService;
using System.Threading.Tasks;

namespace ToDoApp.DataService
{
    public interface IUserDataService
    {
        Task<Users> GetUsers(Users user);
    }
}
