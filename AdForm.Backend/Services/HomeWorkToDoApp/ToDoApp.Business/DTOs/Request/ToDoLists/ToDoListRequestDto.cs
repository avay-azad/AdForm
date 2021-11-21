using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class ToDoListRequestDto
    {
        public string ListName { get; set; }

        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }

    }
}
