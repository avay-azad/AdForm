using System.Threading.Tasks;

namespace AdFormAssignment.Business
{
    public interface IUserAppService
    {
        Task<long> AuthenticateUser(LoginRequestDto loginRequestDto);
    }
}
