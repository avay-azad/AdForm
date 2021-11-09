using System;

public class Labels
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Description { get; set; }
    public virtual Users User { get; set; }
}
