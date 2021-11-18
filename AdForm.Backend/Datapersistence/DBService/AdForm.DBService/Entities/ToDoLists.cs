using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdForm.DBService
{
    public class ToDoLists : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        public long? LabelId { get; set; }
        [ForeignKey("LabelId")]
        public virtual Labels Labels { get; set; }
    }
}
