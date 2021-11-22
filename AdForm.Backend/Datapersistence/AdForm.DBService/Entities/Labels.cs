using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdForm.DBService
{
    public class Labels : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        [Required]
        public string Name { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        public IList<LabelToDoItem> LabelToDoItems { get; set; }
        public IList<LabelToDoList> LabelToDoLists { get; set; }
    }
}
