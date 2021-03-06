﻿using AbstractSweetShopBusinessLogic.Enums;
using System;

namespace AbstractSweetShopBusinessLogic.BindingModels
{
    /// <summary>     
    /// Заказ     
    /// </summary>    
    public class OrderBindingModel
    {
        public int? Id { get; set; }

        public int? ClientId { get; set; }

        public int ProductId { get; set; }

        public int? ImplementerId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool? FreeOrders { get; set; }
    }
}