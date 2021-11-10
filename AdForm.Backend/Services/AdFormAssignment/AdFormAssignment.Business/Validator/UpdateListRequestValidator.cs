using AdFormAssignment.Shared;
using FluentValidation;
using System.Net;

namespace AdFormAssignment.Business
{
    public  class UpdateListRequestValidator: AbstractValidator<UpdateToDoListRequestDto>
    {
        public UpdateListRequestValidator()
        {
            RuleFor(item => item.ListName)
               .NotEmpty().WithMessage(ErrorMessage.List_Name_Empty).WithErrorCode(HttpStatusCode.BadRequest.ToString());

            RuleFor(item => item.ListId)
              .NotNull().WithMessage(ErrorMessage.List_Id_Null).WithErrorCode(HttpStatusCode.BadRequest.ToString());


        }
    }
 
}
