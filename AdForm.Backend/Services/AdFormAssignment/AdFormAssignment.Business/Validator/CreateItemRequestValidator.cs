using AdFormAssignment.Shared;
using FluentValidation;
using System.Net;

namespace AdFormAssignment.Business
{
    public class CreateItemRequestValidator : AbstractValidator<ToDoItemRequestDto>
    {
        public CreateItemRequestValidator()
        {
            RuleFor(item => item.ItemName)
               .NotEmpty().WithMessage(ErrorMessage.Item_Name_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());


        }
    }
}
