using MediaStore.Application.Contracts.Identity;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Domain.Entities;
using MediatR;

namespace MediaStore.Application.Features.Cart.Commands.IncreaseDecreaseCartItem
{
    public class IncreaseDecreaseCartItemCommandHandler : IRequestHandler<IncreaseDecreaseCartItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserService _userService;

        public IncreaseDecreaseCartItemCommandHandler(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUserService userService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userService = userService;
        }


        public  async Task<bool> Handle(IncreaseDecreaseCartItemCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(request.OrderDetailId);
            if (orderDetail == null) 
                return false;
            switch (request.Op.ToLower())
            {
                case "i":
                    orderDetail.Count += 1;
                    break;
                case "d":

                    orderDetail.Count -= 1;

                    break;
            }
            if (orderDetail.Count == 0)
            {
                _orderDetailRepository.Remove(orderDetail);
            }
            else
            {
                _orderDetailRepository.Update(orderDetail);
            }
            await _orderDetailRepository.SaveChangesAsync();
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(_userService.UserId);
            order!.Sum = await _orderRepository.UpdateSumOrderAsync(order.Id);
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
