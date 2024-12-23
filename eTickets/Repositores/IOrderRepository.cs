using eTickets.Models;

namespace eTickets.Repositores
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
