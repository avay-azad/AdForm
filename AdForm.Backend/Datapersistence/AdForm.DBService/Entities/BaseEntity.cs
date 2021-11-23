using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdForm.DBService
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
    }
}