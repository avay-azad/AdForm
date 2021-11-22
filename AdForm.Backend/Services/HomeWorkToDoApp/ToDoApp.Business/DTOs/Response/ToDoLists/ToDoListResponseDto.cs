using System;

namespace ToDoApp.Business
{
    public class ToDoListResponseDto
    {
        public long ToDoListId { get; set; }
        public string Name { get; set; }
        public long? LabelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
