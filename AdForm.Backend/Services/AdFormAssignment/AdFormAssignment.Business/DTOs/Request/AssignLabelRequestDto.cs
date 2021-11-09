using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace AdFormAssignment.Business
{
    public class AssignLabelRequestDto
    {
        public long EntityId { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long LabelId { get; set; }
    }
}
