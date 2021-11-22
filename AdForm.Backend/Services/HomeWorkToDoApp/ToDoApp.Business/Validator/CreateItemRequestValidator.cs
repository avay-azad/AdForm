using ToDoApp.Shared;
using FluentValidation;
using System.Net;

namespace ToDoApp.Business
{
    public class CreateItemRequestValidator : AbstractValidator<ToDoItemRequestDto>
    {
        public CreateItemRequestValidator()
        {
            RuleFor(item => item.ItemName)
               .NotEmpty().WithMessage(ErrorMessage.Item_Name_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());
            RuleFor(item => item.ToDoListId)
             .NotEmpty().WithMessage(ErrorMessage.List_Id_Null).WithErrorCode(HttpStatusCode.BadRequest.ToString())
              .GreaterThanOrEqualTo(1).WithMessage(ErrorMessage.List_Id_Null).WithErrorCode(HttpStatusCode.BadRequest.ToString());
        }
    }
}
