using System;
using System.Collections.Generic;

namespace ToDoApp.Business
{
    public class ToDoItemResponseDto
    {
        public long ToDoItemId { get; set; }
        public string Name { get; set; }
        public long ToDoListId { get; set; }
        public IList<LabelToDoItemResponseDto> ToDoLabelIds { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
