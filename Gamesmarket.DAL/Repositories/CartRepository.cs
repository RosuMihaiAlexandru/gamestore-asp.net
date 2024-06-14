using Gamesmarket.DAL.Interfaces;
using Gamesmarket.Domain.Entity;
using Gamesmarket.DAL;
using Microsoft.EntityFrameworkCore;

namespace Gamesmarket.DAL.Repositories
{
    public class CartRepository : IBaseRepository<Cart>
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> Create(Cart entity)
        {
            await _db.Carts.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Cart entity)
        {
            _db.Carts.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Cart> Get(int id)
        {
            return await _db.Carts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Cart>> Select()
        {
            return _db.Carts.ToListAsync();
        }

        public async Task<Cart> Update(Cart entity)
        {
            _db.Carts.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public IQueryable<Cart> GetAll()
        {
            return _db.Carts;
        }
    }
}
