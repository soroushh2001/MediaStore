using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order?> GetUserLatestOpenOrderAsync(string userId);
        Task<int> UpdateSumOrderAsync(int orderId);
        void UpdateOrder(Order order);
        Task SaveChangesAsync();
    }
}
