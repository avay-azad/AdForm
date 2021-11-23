using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class LabelToDoListResponseDto
    {
        [IgnoreDataMember, JsonIgnore]
        public long Id { get; set; }
        public long LabelId { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long ToDoListId { get; set; }
    }
}
