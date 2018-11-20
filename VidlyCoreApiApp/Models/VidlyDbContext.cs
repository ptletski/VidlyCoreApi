using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VidlyCoreApp.Models
{
    // Design Time Factory
    public class VidlyDbContextFactory : IDesignTimeDbContextFactory<VidlyDbContext>
    {
        VidlyDbContext IDesignTimeDbContextFactory<VidlyDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VidlyDbContext>();
            optionsBuilder.UseSqlite("Data Source=vidly.db");

            return new VidlyDbContext(optionsBuilder.Options);
        }
    }

    class sqlite_keyindex
    {
        public string name { get; set; }

        [Key]
        public int seq { get; set; }
    }

    public class VidlyDbContext : DbContext
    {
        static public readonly string MoviesTable = "Movies"; 
        static public readonly string CustomersTable = "Customers";
        static public readonly string InventoryControlTable = "InventoryControl";

        public VidlyDbContext(DbContextOptions<VidlyDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<RentalTransaction> RentalTransactions { get; set; }
        public DbSet<RentedMovie> RentedMovies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<InventoryControlEntry> InventoryControl { get; set; }
        public DbSet<ContentProvider> ContentProviders { get; set; }
        public DbSet<MpaRating> MpaRatings { get; set; }
        private DbSet<sqlite_keyindex> sqlite_sequence { get; set; }

        public int RetrieveLastAutoIncrementKey(string tableName)
        {
            int keyId = 0;

            try
            {
                keyId = this.sqlite_sequence.Single(sq => sq.name == tableName).seq;
            }
            catch (Exception exception)
            {
                Debug.Assert(false, $"Failure Acquiring Last Key from {tableName}");
                Debug.Assert(false, exception.Message);
            }

            return keyId;
        }
    }
}
