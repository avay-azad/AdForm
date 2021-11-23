using System;
using System.Collections.Generic;

namespace ToDoApp.Business
{
    public class ToDoListResponseDto
    {
        public long ToDoListId { get; set; }
        public string Name { get; set; }
        public IList<LabelToDoListResponseDto> LabelToDoLists { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
