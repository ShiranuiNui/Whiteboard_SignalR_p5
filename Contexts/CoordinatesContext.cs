using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Whiteboard_SignalR_p5.Models;
namespace Whiteboard_SignalR_p5.Contexts
{
    public class CoordinatesContext : DbContext
    {
        public DbSet<Coordinate> Coordinates { get; set; }
        public CoordinatesContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=:memory");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;");
        }
    }
}