using ToDoApp.Shared;
using FluentValidation;
using System.Net;

namespace ToDoApp.Business
{
    public class CreateLabelRequestValidator : AbstractValidator<LabelRequestDto>
    {
        public CreateLabelRequestValidator()
        {
            RuleFor(item => item.Name)
               .NotEmpty().WithMessage(ErrorMessage.Label_Name_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());


        }
    }
}
