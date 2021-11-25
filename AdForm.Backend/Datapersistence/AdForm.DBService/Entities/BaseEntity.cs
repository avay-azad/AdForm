using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdForm.DBService
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}