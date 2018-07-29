using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Habers{
    public int id {get;set;}
    public string Baslik { get; set; }
    public string Detay { get; set; }
    public DateTime Tarih { get; set; }
    public int Kategori_id { get; set; }
    [NotMapped]
    public string RefreshToken{get;set;}
}