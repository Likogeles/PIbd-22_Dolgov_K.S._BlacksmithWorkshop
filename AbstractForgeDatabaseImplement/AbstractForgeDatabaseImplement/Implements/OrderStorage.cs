using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using AbstractForgeDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractForgeDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new BlacksmithWorkshopDatabase();
            return context.Orders.Select(CreateModel).ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model) {
            if (model == null)
            {
                return null;
            }
            using var context = new BlacksmithWorkshopDatabase();
            //return context.Orders.Where(rec => rec.Status == model.Status).Select(CreateModel).ToList();
            return context.Orders
            .Include(rec => rec.Manufacture)
            .Where(rec => rec.Id.Equals(model.Id) || rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model) {
            if (model == null)
            {
                return null;
            }
            using var context = new BlacksmithWorkshopDatabase();
            var order = context.Orders.FirstOrDefault(rec => rec.Status == model.Status || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model) {
            using var context = new BlacksmithWorkshopDatabase();
            using var transaction = context.Database.BeginTransaction();
            int maxId = context.Orders.Count() > 0 ? context.Components.Max(rec => rec.Id) : 0;
            
            try
            {
                context.Orders.Add(CreateModel(model, new Order()));

                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }
        public void Update(OrderBindingModel model) {
            using var context = new BlacksmithWorkshopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(OrderBindingModel model) {
            using var context = new BlacksmithWorkshopDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.ManufactureId = model.ManufactureId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;

            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            using var context = new BlacksmithWorkshopDatabase();
            string manufactureName = null;
            foreach (var document in context.Manufactures)
            {
                if (document.Id == order.ManufactureId)
                {
                    manufactureName = document.ManufactureName;
                }
            }

            return new OrderViewModel
            {
                Id = order.Id,
                ManufactureId = order.ManufactureId,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                ManufactureName = manufactureName,
            };
        }

    }
}
