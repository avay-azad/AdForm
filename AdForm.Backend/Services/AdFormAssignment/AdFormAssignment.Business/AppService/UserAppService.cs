using AdForm.Entities;
using AdForm.SDK;
using AdFormAssignment.DataService;
using AutoMapper;
using System;
using System.Net;
using System.Threading.Tasks;
using AdFormAssignment.Shared;

namespace AdFormAssignment.Business
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserDataService _userDataService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="UserAppService"/> class.
        /// </summary>
        /// <param name="userDataService"></param>
        public UserAppService(IUserDataService userDataService, IMapper mapper)
        {
            _userDataService = userDataService;
            _mapper = mapper;

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
            return user.Id;
        }
    }
}
