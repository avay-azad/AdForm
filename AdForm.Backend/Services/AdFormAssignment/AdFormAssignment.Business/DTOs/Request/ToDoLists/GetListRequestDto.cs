using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AdFormAssignment.Business
{
    public class GetListRequestDto
    {
        public long Id { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }
    }
}
