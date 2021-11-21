using ToDoApp.Business;
using HotChocolate.Types;

namespace ToDoApp.Api
{
    public class ItemsType : ObjectType<ToDoItemResponseDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ToDoItemResponseDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.CreatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.UpdatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.LabelId).Type<IdType>();
        }
    }
}
