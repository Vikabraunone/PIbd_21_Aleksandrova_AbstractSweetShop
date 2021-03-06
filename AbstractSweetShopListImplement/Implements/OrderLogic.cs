﻿using AbstractSweetShopBusinessLogic.BindingModels;
using AbstractSweetShopBusinessLogic.Enums;
using AbstractSweetShopBusinessLogic.Interfaces;
using AbstractSweetShopBusinessLogic.ViewModels;
using AbstractSweetShopListImplement.Models;
using System;
using System.Collections.Generic;

namespace AbstractSweetShopListImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;

        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order { Id = 1 };
            foreach (var order in source.Orders)
            {
                if (!model.Id.HasValue && order.Id >= tempOrder.Id)
                    tempOrder.Id = order.Id + 1;
                else if (model.Id.HasValue && order.Id == model.Id)
                    tempOrder = order;
            }
            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                    throw new Exception("Элемент не найден");
                CreateModel(model, tempOrder);
            }
            else
                source.Orders.Add(CreateModel(model, tempOrder));
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Products.Count; ++i)
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            throw new Exception("Элемент не найден");
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if (model != null)
                {
                    if (model.Id.HasValue && order.Id == model.Id.Value || model.DateFrom.HasValue && model.DateTo.HasValue
                        && order.DateCreate >= model.DateFrom.Value && order.DateCreate <= model.DateTo.Value
                        || model.ClientId.HasValue && order.ClientId == model.ClientId.Value
                        || model.FreeOrders.HasValue && model.FreeOrders.Value && !order.ImplementerId.HasValue
                        || model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId.Value && order.Status == OrderStatus.Выполняется)
                    {
                        result.Add(CreateViewModel(order));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(order));
            }
            return result;
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.ClientId = model.ClientId;
            order.ProductId = model.ProductId;
            order.ImplementerId = model.ImplementerId;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.Count = model.Count;
            order.DateImplement = model.DateImplement;
            order.Status = model.Status;
            return order;
        }

        private OrderViewModel CreateViewModel(Order order)
        {
            string productName = string.Empty;
            foreach (var product in source.Products)
                if (product.Id == order.ProductId)
                {
                    productName = product.ProductName;
                    break;
                }
            string clientFIO = string.Empty;
            foreach (var client in source.Clients)
                if (client.Id == order.ClientId)
                {
                    clientFIO = client.ClientFIO;
                    break;
                }
            string implementerFIO = string.Empty;
            foreach (var implementer in source.Implementers)
                if (implementer.Id == order.ImplementerId)
                {
                    implementerFIO = implementer.ImplementerFIO;
                    break;
                }
            return new OrderViewModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                ClientFIO = clientFIO,
                ProductId = order.ProductId,
                ProductName = productName,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = implementerFIO,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}