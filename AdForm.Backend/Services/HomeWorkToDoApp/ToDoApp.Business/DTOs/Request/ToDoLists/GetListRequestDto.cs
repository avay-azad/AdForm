using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class GetListRequestDto
    {
        public long ToDoListId { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }
    }
}
