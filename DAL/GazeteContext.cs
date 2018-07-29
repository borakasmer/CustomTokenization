 using Microsoft.EntityFrameworkCore;
public class GazeteContext : DbContext
  {
    public DbSet<Habers> Habers { get; set; }     
    public DbSet<Kategoris> Kategoris { get; set; }   
    public DbSet<Yorums> Yorums { get; set; }   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);   
      optionsBuilder.UseSqlServer("Server=tcp:******,****;Initial Catalog=Gazete;User ID=****;Password=****;");                
    }
  }