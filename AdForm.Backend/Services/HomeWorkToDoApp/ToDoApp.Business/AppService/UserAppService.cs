using AdForm.DBService;
using AdForm.Core;
using ToDoApp.DataService;
using ToDoApp.Shared;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace ToDoApp.Business
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserDataService _userDataService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Create new instance of <see cref="UserAppService"/> class.
        /// </summary>
        /// <param name="userDataService"></param>
        public UserAppService(IUserDataService userDataService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userDataService = userDataService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        ///  Authenticate user.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        public async Task<long> AuthenticateUser(LoginRequestDto loginRequestDto)
        {
            var user = await _userDataService.GetUsers(_mapper.Map<Users>(loginRequestDto));
            if (user == null)
                throw new AuthenticationException(ErrorMessage.User_Invalid_Login, HttpStatusCode.Unauthorized, AuthenticationExceptionType.Unauthorized);
            else if (AesSymmetric.AesDecrypt(user.Password, _appSettings.AesSymmetricKey) == loginRequestDto.Password)
                return user.UserId;
            else
                throw new AuthenticationException(ErrorMessage.User_Invalid_Login, HttpStatusCode.Unauthorized, AuthenticationExceptionType.Unauthorized);
        }
    }
}
