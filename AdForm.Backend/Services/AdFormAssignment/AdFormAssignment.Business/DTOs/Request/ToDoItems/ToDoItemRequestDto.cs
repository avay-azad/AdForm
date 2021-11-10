using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AdFormAssignment.Business
{
    public class ToDoItemRequestDto
    {
        public string ItemName { get; set; }

        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }

    }
}
