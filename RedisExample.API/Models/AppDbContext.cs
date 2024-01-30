using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace RedisExample.API.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        public DbSet<Products> Productss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Products>().HasData(
                new Products()
                {
                    Id = 1,
                    Name = "Araba 1",
                    Price = 1200
                },
                new Products()
                {
                    Id = 2,
                    Name = "Araba 2",
                    Price = 1200
                },
                new Products()
                {
                    Id = 3,
                    Name = "Araba 3",
                    Price = 1200
                }

                );



            base.OnModelCreating(modelBuilder);
        }
    }
}
