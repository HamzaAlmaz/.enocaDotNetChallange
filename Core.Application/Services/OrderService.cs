using Core.Application.DTOs.order;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ServiceResponse<string>> CreateAsync(CreateOrderDTO dto)
        {
            var configurations = await _orderRepository.GetCarrierConfigurationsAsync();

            if (configurations == null || !configurations.Any())
            {
                throw new ApiException("Sistemde tanımlı kargo konfigürasyonu bulunamadı.", 400);
            }

            var matchingConfigs = configurations
                .Where(c => dto.OrderDesi >= c.CarrierMinDesi && dto.OrderDesi <= c.CarrierMaxDesi)
                .ToList();

            decimal finalCost;
            int carrierId;

            if (matchingConfigs.Any())
            {
                var cheapest = matchingConfigs.OrderBy(c => c.CarrierCost).First();
                finalCost = cheapest.CarrierCost;
                carrierId = cheapest.CarrierId;
            }
            else
            {
                var closest = configurations
                    .OrderBy(c => Math.Abs(c.CarrierMaxDesi - dto.OrderDesi))
                    .First();

                var desiDiff = dto.OrderDesi - closest.CarrierMaxDesi;

                // Carrier navigation property'sinin dolu olduğundan emin olmalısın (Include ile çekilmiş olmalı)
                finalCost = closest.CarrierCost + (desiDiff * closest.Carrier.CarrierPlusDesiCost);
                carrierId = closest.CarrierId;
            }

            var order = new Order
            {
                CarrierId = carrierId,
                OrderDesi = dto.OrderDesi,
                OrderDate = DateTime.Now,
                OrderCarrierCost = finalCost,
            };

            await _orderRepository.AddAsync(order);

            return new ServiceResponse<string>($"Sipariş başarıyla oluşturuldu. Hesaplanan maliyet: {finalCost}");
        }

        public async Task<ServiceResponse<List<Order>>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return new ServiceResponse<List<Order>>(orders);
        }

        public async Task<ServiceResponse<string>> DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new NotFoundException($"{id} ID'li sipariş bulunamadı.");
            }

            await _orderRepository.DeleteAsync(id);
            return new ServiceResponse<string>("Sipariş başarıyla silindi.");
        }
    }
}