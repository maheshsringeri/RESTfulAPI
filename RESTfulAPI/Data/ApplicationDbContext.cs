using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Models;

namespace RESTfulAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Villa1",
                    Details = "Villa1 Details",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate=DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "Villa2",
                    Details = "Villa2 Details",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                }, 
                new Villa
                {
                    Id = 3,
                    Name = "Villa3",
                    Details = "Villa3 Details",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 4,
                    Name = "Villa4",
                    Details = "Villa4 Details",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                }

                );
        }
    }
}
