﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractSweetShopDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string ClientFIO { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<Order> Orders { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<MessageInfo> MessageInfoes { set; get; }
    }
}
