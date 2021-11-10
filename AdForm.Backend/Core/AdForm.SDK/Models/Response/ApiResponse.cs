using System;
using System.Collections.Generic;
using System.Text;

namespace AdForm.SDK
{
    public class APIResponse<T>
    {
        public bool IsSucess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
