﻿using AbstractSweetShopBusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AbstractSweetShopDatabaseImplement.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Product Product { get; set; }
    }
}