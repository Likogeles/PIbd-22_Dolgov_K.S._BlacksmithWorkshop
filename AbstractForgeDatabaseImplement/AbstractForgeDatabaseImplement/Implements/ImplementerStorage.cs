using System;
using System.Collections.Generic;
using System.Linq;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using AbstractForgeContracts.BindingModels;
using AbstractForgeDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractForgeDatabaseImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        public List<ImplementerViewModel> GetFullList()
        {
            using var context = new AbstractForgeDatabase();
            return context.Implementers
                .Select(CreateModel)
                .ToList();
        }
        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractForgeDatabase();
            return context.Implementers
            .Include(rec => rec.Orders)
            .Where(rec => rec.ImplementerFIO == model.ImplementerFIO)
            .Select(CreateModel)
            .ToList();
        }
        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AbstractForgeDatabase();
            var implementer = context.Implementers
            .Include(rec => rec.Orders)
            .FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO || rec.Id == model.Id);
            return implementer != null ? CreateModel(implementer) : null;
        }
        public void Insert(ImplementerBindingModel model)
        {
            using var context = new AbstractForgeDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Implementers.Add(CreateModel(model, new Implementer()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(ImplementerBindingModel model)
        {
            using var context = new AbstractForgeDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(ImplementerBindingModel model)
        {
            using var context = new AbstractForgeDatabase();
            Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Implementers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            implementer.ImplementerFIO = model.ImplementerFIO;
            implementer.WorkingTime = model.WorkingTime;
            implementer.PauseTime = model.PauseTime;
            return implementer;
        }
        private static ImplementerViewModel CreateModel(Implementer implementer)
        {
            return new ImplementerViewModel
            {
                Id = implementer.Id,
                ImplementerFIO = implementer.ImplementerFIO,
                WorkingTime = implementer.WorkingTime,
                PauseTime = implementer.PauseTime
            };
        }
    }
}
