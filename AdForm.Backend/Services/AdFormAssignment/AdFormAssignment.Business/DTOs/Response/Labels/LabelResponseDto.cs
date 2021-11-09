using System;
using System.Collections.Generic;
using System.Text;

namespace AdFormAssignment.Business
{
    public class LabelResponseDto : LabelRequestDto
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
