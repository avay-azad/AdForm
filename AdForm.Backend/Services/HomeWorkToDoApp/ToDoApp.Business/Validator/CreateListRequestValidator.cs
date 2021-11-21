using ToDoApp.Shared;
using FluentValidation;
using System.Net;

namespace ToDoApp.Business
{
    public class CreateListRequestValidator : AbstractValidator<ToDoListRequestDto>
    {
        public CreateListRequestValidator()
        {
            RuleFor(item => item.ListName)
               .NotEmpty().WithMessage(ErrorMessage.List_Name_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());


        }
    }
}
