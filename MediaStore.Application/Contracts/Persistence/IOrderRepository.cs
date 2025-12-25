using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetUserLatestOpenOrderAsync(string userId);
        Task<int> UpdateSumOrderAsync(int orderId);
        void Update(Order order);
        Task SaveChangesAsync();
    }
}
