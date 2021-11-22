using System;

namespace ToDoApp.Business
{
    public class LabelResponseDto : LabelRequestDto
    {
        public long LabelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
