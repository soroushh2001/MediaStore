using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IOrderDetailRepository
    {
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task<OrderDetail?> GetOrderDetailByOrderAndProductIdAsync(int orderId, int productId);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail detail);
        Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
