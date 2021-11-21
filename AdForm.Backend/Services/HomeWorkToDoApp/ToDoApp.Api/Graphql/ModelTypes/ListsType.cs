using HotChocolate.Types;
using ToDoApp.Business;

namespace ToDoApp.Api
{
    /// <summary>
    /// Lists type mapped to ListDto types for GraphQl.
    /// </summary>
    public class ListsType : ObjectType<ToDoListResponseDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ToDoListResponseDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.CreatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.UpdatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.LabelId).Type<IdType>();
        }
    }
}
