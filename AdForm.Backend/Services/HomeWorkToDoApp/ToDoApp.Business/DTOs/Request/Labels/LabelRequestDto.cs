using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace ToDoApp.Business
{
    public class LabelRequestDto
    {
        public string Name { get; set; }
        [IgnoreDataMember, JsonIgnore]
        public long UserId { get; set; }
    }
}
