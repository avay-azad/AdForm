using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class LabelToDoItemResponseDto
    {
        [IgnoreDataMember, JsonIgnore]
        public long Id { get; set; }
        public long LabelId { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long ToDoItemId { get; set; }
    }
}
