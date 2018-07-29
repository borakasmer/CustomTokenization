using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Yorums{
    public int id { get; set; }
    public string Icerik { get; set; }
    public string Isim { get; set; }
    public DateTime Tarih { get; set; }
    public int Haber_id { get; set; }
    [NotMapped]
    public string RefreshToken{get;set;}

}