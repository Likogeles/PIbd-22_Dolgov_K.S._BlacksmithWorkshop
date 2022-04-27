using AbstractForgeDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractForgeDatabaseImplement
{
    public class BlacksmithWorkshopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=Kirill-PC1\SQLEXPRESS;Initial Catalog=AbstractForgeDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Manufacture> Manufactures { set; get; }
        public virtual DbSet<ManufactureComponent> ManufactureComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
