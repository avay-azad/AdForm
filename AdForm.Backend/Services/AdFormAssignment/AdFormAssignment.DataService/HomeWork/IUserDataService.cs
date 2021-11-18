using AdForm.DBService;
using System.Threading.Tasks;

namespace AdFormAssignment.DataService
{
    public interface IUserDataService
    {
        Task<Users> GetUsers(Users user);
    }
}
