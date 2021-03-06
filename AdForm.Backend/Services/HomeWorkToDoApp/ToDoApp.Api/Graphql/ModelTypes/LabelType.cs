using HotChocolate.Types;
using ToDoApp.Business;

namespace ToDoApp.Api
{
    /// <summary>
    /// Label type mapped to LabelDto types for GraphQl.
    /// </summary>
    public class LabelType : ObjectType<LabelResponseDto>
    {
        protected override void Configure(IObjectTypeDescriptor<LabelResponseDto> descriptor)
        {
            descriptor.Field(a => a.LabelId).Type<IdType>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.CreatedDate).Type<DateTimeType>();
            descriptor.Field(a => a.UpdatedDate).Type<DateTimeType>();
        }
    }
}
