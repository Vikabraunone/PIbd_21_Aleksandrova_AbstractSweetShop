﻿using AbstractSweetShopBusinessLogic.BindingModels;
using AbstractSweetShopBusinessLogic.Interfaces;
using AbstractSweetShopBusinessLogic.ViewModels;
using AbstractSweetShopDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractSweetShopDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Email == model.Email && rec.Id != model.Id);
                if (element != null)
                    throw new Exception("Уже есть клиент с таким логином!");
                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                        throw new Exception("Клиент не найден");
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.ClientFIO = model.ClientFIO;
                element.Email = model.Email;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                context.Orders.RemoveRange(context.Orders.Where(rec => rec.ClientId == model.Id));
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                    throw new Exception("Клиент не найден");
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new AbstractSweetShopDatabase())
            {
                var result = context.Clients
                .Where(rec => model == null || rec.Email.Equals(model.Email) && rec.Password.Equals(model.Password)
                || rec.Id == model.Id)
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientFIO = rec.ClientFIO,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
                if (result.Count() == 0)
                    return null;
                else
                    return result;
            }
        }
    }
}