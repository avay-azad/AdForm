using System;
using System.Collections.Generic;
using System.Text;

namespace AdForm.SDK
{
    public interface IJwtUtils
    {
        public string GenerateToken(long userId);
        public int? ValidateToken(string token);
    }
}
