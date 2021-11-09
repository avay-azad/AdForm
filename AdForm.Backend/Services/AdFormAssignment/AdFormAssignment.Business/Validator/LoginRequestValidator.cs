using FluentValidation;
using AdFormAssignment.Shared;
using System.Net;

namespace AdFormAssignment.Business
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(user => user.UserName)
               .NotEmpty().WithMessage(ErrorMessage.User_UserName_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ErrorMessage.User_Password_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());
                
        }
    }
}
