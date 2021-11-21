using System;
using System.Collections.Generic;
using System.Text;

namespace AdForm.Core
{
    public interface IJwtUtils
    {
        public string GenerateToken(long userId);
        public int? ValidateToken(string token);
    }
}
