using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using AbstractForgeFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractForgeFileImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly FileDataListSingleton source;

        public OrderStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        
        public List<OrderViewModel> GetFullList()
        {
            return source.Orders.Select(CreateModel).ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            //return source.Orders.Where(rec => rec.Status == model.Status).Select(CreateModel).ToList();
            return source.Orders
            .Where(rec => rec.Id.Equals(model.Id) || rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var order = source.Orders.FirstOrDefault(rec => rec.Status == model.Status || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;

        }

        public void Insert(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
            var element = new Order
            {
                Id = maxId + 1,
                ManufactureId = model.ManufactureId,
                Count = model.Count,
                Sum = model.Sum,
                Status = model.Status,
                DateCreate = model.DateCreate,
                DateImplement = model.DateImplement,
            };
            source.Orders.Add(CreateModel(model, element));
        }

        public void Update(OrderBindingModel model)
        {
            var element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
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
            string manufactureName = null;
            foreach (var document in source.Manufactures)
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