using FluentValidation;
using System.Net;
using ToDoApp.Shared;

namespace ToDoApp.Business
{
    public class AssignLabelRequestvalidator : AbstractValidator<AssignLabelRequestDto>
    {
        public AssignLabelRequestvalidator()
        {
            RuleForEach(x => x.LabelId).GreaterThanOrEqualTo(1).WithMessage(ErrorMessage.List_Id_Null).WithErrorCode(HttpStatusCode.BadRequest.ToString()); ;
        }
           
    }
}
