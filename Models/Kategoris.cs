using System.ComponentModel.DataAnnotations.Schema;

public class Kategoris
{
    public int id { get; set; }
    public string Ad { get; set; }
    [NotMapped]
    public string RefreshToken{get;set;}
}