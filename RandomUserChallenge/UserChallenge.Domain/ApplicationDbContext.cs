using System;
using UserChallenge.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using UserChallenge.Domain.Entities;

namespace UserChallenge.Domain
{

  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<User> users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //  modelBuilder.RemovePluralizingTableNameConvention();

      modelBuilder.Entity<User>().Property(l => l.Id).HasDefaultValueSql("newsequentialid()");

      base.OnModelCreating(modelBuilder);
    }
  }
}
