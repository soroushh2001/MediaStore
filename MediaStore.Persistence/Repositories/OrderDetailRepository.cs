using MediaStore.Application.Contracts.Persistence;
using MediaStore.Domain.Entities;
using MediaStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Persistence.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly MediaStoreDbContext _context;

        public OrderDetailRepository(MediaStoreDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
        }

        public async Task<OrderDetail?> GetOrderDetailByOrderAndProductIdAsync(int orderId, int productId)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void DeleteOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Remove(detail);
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }
    }
}
