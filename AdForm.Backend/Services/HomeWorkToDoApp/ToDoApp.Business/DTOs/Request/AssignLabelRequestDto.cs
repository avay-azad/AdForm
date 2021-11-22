using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class AssignLabelRequestDto
    {
        public IList<long> LabelId { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }
     }
}
