using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdForm.DBService
{
    public class ToDoItems : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ToDoItemId { get; set; }
        [Required]
        public string Name { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        public long ToDoListId { get; set; }
        [ForeignKey("ToDoListId")]
        public virtual ToDoLists ToDoLists { get; set; }
        public IList<LabelToDoItem> LabelToDoItems { get; set; }
    }
}
