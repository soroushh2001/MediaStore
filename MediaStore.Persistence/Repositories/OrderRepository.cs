using MediaStore.Application.Contracts.Persistence;
using MediaStore.Domain.Entities;
using MediaStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MediaStoreDbContext _context;

        public OrderRepository(MediaStoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<Order?> GetUserLatestOpenOrderAsync(string userId)
        {
            return await _context.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateSumOrderAsync(int orderId)
        {
            return await _context.OrderDetails.Where(o => o.OrderId == orderId).Select(d => d.Count * d.Price).SumAsync();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
