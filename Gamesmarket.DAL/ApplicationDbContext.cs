using Gamesmarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace GamesMarket.DAL
{
    public class ApplicationDbContext : DbContext
    {//Class for working with a database
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        //Managing collection of objects in database with Entity Framework
        public DbSet<Game> Games { get; set; }

    }
}
