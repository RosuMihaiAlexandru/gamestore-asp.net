using Gamesmarket.DAL.Interfaces;
using Gamesmarket.Domain.Entity;
using Gamesmarket.DAL;
using Microsoft.EntityFrameworkCore;

namespace Gamesmarket.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Order entity)
        {
            await _db.Orders.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Order entity)
        {
            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Order> Get(int id)
        {
            return await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Order>> Select()
        {
            return _db.Orders.ToListAsync();
        }

        public async Task<Order> Update(Order entity)
        {
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        public IQueryable<Order> GetAll()
        {
            return _db.Orders;
        }
    }
}
