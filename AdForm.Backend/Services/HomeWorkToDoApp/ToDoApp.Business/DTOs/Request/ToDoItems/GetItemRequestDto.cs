using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class GetItemRequestDto
    {
        public long Id { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }
    }
}
