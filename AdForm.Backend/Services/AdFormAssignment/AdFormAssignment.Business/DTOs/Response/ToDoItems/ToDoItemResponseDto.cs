using System;

namespace AdFormAssignment.Business
{
    public class ToDoItemResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? LabelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
