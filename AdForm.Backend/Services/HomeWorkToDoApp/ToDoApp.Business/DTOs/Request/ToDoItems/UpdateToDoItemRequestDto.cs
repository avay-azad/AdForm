using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class UpdateToDoItemRequestDto 
    {
        public string ItemName { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }
        public long ToDoItemId { get; set; }
    }
}
