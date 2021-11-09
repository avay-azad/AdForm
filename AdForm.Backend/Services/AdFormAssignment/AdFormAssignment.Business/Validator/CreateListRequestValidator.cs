using AdFormAssignment.Shared;
using FluentValidation;
using System.Net;

namespace AdFormAssignment.Business
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
