using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IOrderDetailRepository
    {
        Task AddAsync(OrderDetail orderDetail);
        Task<OrderDetail?> GetByOrderAndProductIdAsync(int orderId, int productId);
        void Update(OrderDetail orderDetail);
        void Remove(OrderDetail detail);
        Task<OrderDetail?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
