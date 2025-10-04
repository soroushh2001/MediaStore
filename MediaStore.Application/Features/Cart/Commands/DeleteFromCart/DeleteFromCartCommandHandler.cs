using MediaStore.Application.Contracts.Identity;
using MediaStore.Application.Contracts.Persistence;
using MediatR;

namespace MediaStore.Application.Features.Cart.Commands.DeleteFromCart
{
    public class DeleteFromCartCommandHandler : IRequestHandler<DeleteFromCartCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserService _userService;
        public DeleteFromCartCommandHandler(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IUserService userService)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _userService = userService;
        }

        public async Task<bool> Handle(DeleteFromCartCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(request.OrderDetailId);
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(_userService.UserId);
            if(orderDetail == null) 
                return false;
            _orderDetailRepository.DeleteOrderDetail(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
            order.Sum = await _orderRepository.UpdateSumOrderAsync(order.Id);
            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
