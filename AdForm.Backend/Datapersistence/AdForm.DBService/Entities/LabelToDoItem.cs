using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdForm.DBService
{
    public class LabelToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long LabelId { get; set; }
        [ForeignKey("LabelId")]
        public Labels Label { get; set; }
        [Required]
        public long ToDoItemId { get; set; }
        [ForeignKey("ToDoItemId")]
        public ToDoItems ToDoItem { get; set; }
    }
}
