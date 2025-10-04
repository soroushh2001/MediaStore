using MediaStore.Application.Contracts.Identity;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserService _userService;
        private readonly IProductRepository _productRepository;

        public AddToCartCommandHandler(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUserService userService, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userService = userService;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetUserLatestOpenOrderAsync(_userService.UserId);
            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (order == null)
            {
                var newOrder = new Order
                {
                    UserId = _userService.UserId,
                    Sum = product.Price * request.Count
                };
                await _orderRepository.AddOrderAsync(newOrder);
                await _orderRepository.SaveChangesAsync();
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.Id,
                    Count = request.Count,
                    Price = product.Price,
                    ProductId = request.ProductId,
                };
                await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
                await _orderDetailRepository.SaveChangesAsync();
            }
            else
            {
                var orderDetail = await _orderDetailRepository.GetOrderDetailByOrderAndProductIdAsync(order.Id, request.ProductId);
                if (orderDetail == null)
                {
                    orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        Count = request.Count,
                        Price = product.Price,
                        ProductId = request.ProductId,
                    };
                    await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
                    await _orderDetailRepository.SaveChangesAsync();
                }
                else
                {
                    orderDetail.Count += request.Count;
                    _orderDetailRepository.UpdateOrderDetail(orderDetail);
                    await _orderRepository.SaveChangesAsync();
                }
                order.Sum = await _orderRepository.UpdateSumOrderAsync(order.Id);
                _orderRepository.UpdateOrder(order);
                await _orderRepository.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}

