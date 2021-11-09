using System;

public class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public virtual List<TodoLists> Lists { get; set; }
    public virtual List<Labels> Labels { get; set; }
}
