using Microsoft.Data.Sqlite;
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
            var connectionString =
                new SqliteConnectionStringBuilder { DataSource = "coordinates.db" }.ToString();
            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }
    }
}