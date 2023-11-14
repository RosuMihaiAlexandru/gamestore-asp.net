using Gamesmarket.Domain.Entity;
using GamesMarket.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamesMarket.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {//Implementation of async CRUD operations
        private readonly ApplicationDbContext _db;
        public GameRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Game entity)
        {
            await _db.Games.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Game> Get(int id)
        {
            return await _db.Games.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Game>> Select() 
        {
            return _db.Games.ToListAsync();
        }

        public async Task<bool> Delete(Game entity)
        {
            _db.Games.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Game> GetByName(string name)
        {
            return await _db.Games.FirstOrDefaultAsync(x => x.Name == name);
        }

		public async Task<Game> Update(Game entity)
		{
			_db.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
		}
	}
}
