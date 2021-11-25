using System;
using System.Collections.Generic;
using System.Text;

namespace AdForm.Core
{
    public class APIResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
