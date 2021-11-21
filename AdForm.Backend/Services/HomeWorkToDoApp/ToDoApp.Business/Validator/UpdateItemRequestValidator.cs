using ToDoApp.Shared;
using FluentValidation;
using System.Net;

namespace ToDoApp.Business
{
    public  class UpdateItemRequestValidator: AbstractValidator<UpdateToDoItemRequestDto>
    {
        public UpdateItemRequestValidator()
        {
            RuleFor(item => item.ItemName)
               .NotEmpty().WithMessage(ErrorMessage.Item_Name_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(item => item.ItemId)
              .NotNull().WithMessage(ErrorMessage.Item_Id_Null).WithErrorCode(HttpStatusCode.BadRequest.ToString());


        }
    }
 
}
