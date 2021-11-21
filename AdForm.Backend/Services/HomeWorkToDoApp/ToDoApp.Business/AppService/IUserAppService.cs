using System.Threading.Tasks;

namespace ToDoApp.Business
{
    public interface IUserAppService
    {
        Task<long> AuthenticateUser(LoginRequestDto loginRequestDto);
    }
}
