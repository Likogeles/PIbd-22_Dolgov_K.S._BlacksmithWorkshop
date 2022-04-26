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
    public class ManufactureStorage : IManufactureStorage
    {
        public List<ManufactureViewModel> GetFullList()
        {
            using var context = new AbstractForgeDatabase();
            return context.Manufactures
            .Include(rec => rec.ManufactureComponents)
            .ThenInclude(rec => rec.Component)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<ManufactureViewModel> GetFilteredList(ManufactureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractForgeDatabase();
            return context.Manufactures
            .Include(rec => rec.ManufactureComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.ManufactureName.Contains(model.ManufactureName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public ManufactureViewModel GetElement(ManufactureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractForgeDatabase();
            var manufacture = context.Manufactures
            .Include(rec => rec.ManufactureComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.ManufactureName == model.ManufactureName ||
            rec.Id == model.Id);
            return manufacture != null ? CreateModel(manufacture) : null;
        }
        public void Insert(ManufactureBindingModel model)
        {
            using var context = new AbstractForgeDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                Manufacture manufacture = new Manufacture() {
                    ManufactureName = model.ManufactureName,
                    Price = model.Price
                };
                context.Manufactures.Add(manufacture);
                context.SaveChanges();
                CreateModel(model, manufacture, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(ManufactureBindingModel model)
        {
            using var context = new AbstractForgeDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Manufactures.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(ManufactureBindingModel model)
        {
            using var context = new AbstractForgeDatabase();
            Manufacture element = context.Manufactures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Manufactures.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Manufacture CreateModel(ManufactureBindingModel model, Manufacture manufacture,
       AbstractForgeDatabase context)
        {
            manufacture.ManufactureName = model.ManufactureName;
            manufacture.Price = model.Price;
            if (model.Id.HasValue)
            {
                var manufactureComponents = context.ManufactureComponents.Where(rec => rec.ManufactureId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ManufactureComponents.RemoveRange(manufactureComponents.Where(rec => !model.ManufactureComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in manufactureComponents)
                {
                    updateComponent.Count = model.ManufactureComponents[updateComponent.ComponentId].Item2;
                    model.ManufactureComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.ManufactureComponents)
            {
                context.ManufactureComponents.Add(new ManufactureComponent
                {
                    ManufactureId = manufacture.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return manufacture;
        }
        private static ManufactureViewModel CreateModel(Manufacture manufacture)
        {
            return new ManufactureViewModel
            {
                Id = manufacture.Id,
                ManufactureName = manufacture.ManufactureName,
                Price = manufacture.Price,
                ManufactureComponents = manufacture.ManufactureComponents
                .ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }
}
