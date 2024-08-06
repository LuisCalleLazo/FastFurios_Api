using FastFurios_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFurios_Api.Database
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    public DbSet<Player> Player { get; set; }
    public DbSet<ObjectItem> ObjectItem { get; set; }
    public DbSet<ObjectDetail> ObjectDetail { get; set; }
    public DbSet<PaymentSkin> PaymentSkin { get; set; }
    public DbSet<Skin> Skin { get; set; }
    public DbSet<Token> Token { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Token>(t => {
        t.HasOne(t => t.PlayerRef)
          .WithMany(t => t.Tokens)
          .HasForeignKey(t => t.PlayerId);
      });
      
      modelBuilder.Entity<Player>(t => {
        
      });


      modelBuilder.Entity<ObjectItem>(t => {
        t.HasOne(t => t.PlayerRef)
          .WithMany(t => t.ObjectItems)
          .HasForeignKey(t => t.PlayerId);

        t.HasOne(t => t.ObjectDetailRef)
          .WithMany(t => t.ObjectItems)
          .HasForeignKey(t => t.ObjectDetailId);
      });
      
      modelBuilder.Entity<ObjectDetail>(t => {
        
      });

      modelBuilder.Entity<PaymentSkin>(t => {
        t.HasOne(t => t.PlayerRef)
          .WithMany(t => t.PaymentSkins)
          .HasForeignKey(t => t.PlayerId);

        t.HasOne(t => t.SkinRef)
          .WithMany(t => t.PaymentSkins)
          .HasForeignKey(t => t.SkinId);
      });

      modelBuilder.Entity<Skin>(t => {
        
      });

    }
  }
}