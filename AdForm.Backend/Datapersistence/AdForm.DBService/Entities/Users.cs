using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdForm.DBService
{
    public class Users : BaseEntity
    {
        public Users()
        {
            ToDoItems = new HashSet<ToDoItems>();
            ToDoLists = new HashSet<ToDoLists>();
            Labels = new HashSet<Labels>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ToDoItems> ToDoItems { get; set; }

        public virtual ICollection<ToDoLists> ToDoLists { get; set; }
        public virtual ICollection<Labels> Labels { get; set; }
    }

}