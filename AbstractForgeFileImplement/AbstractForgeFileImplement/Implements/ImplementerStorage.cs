using System;
using System.Collections.Generic;
using System.Linq;
using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using AbstractForgeFileImplement.Models;

namespace AbstractForgeFileImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        private readonly FileDataListSingleton source;
        public ImplementerStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<ImplementerViewModel> GetFullList()
        {
            return source.Implementers.Select(CreateModel).ToList();
        }
        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Implementers.Where(rec => rec.ImplementerFIO.Contains(model.ImplementerFIO)).Select(CreateModel).ToList();
        }
        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var implementer = source.Implementers.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO || rec.Id == model.Id);
            return implementer != null ? CreateModel(implementer) : null;
        }
        public void Insert(ImplementerBindingModel model)
        {
            int maxId = source.Implementers.Count > 0 ? source.Implementers.Max(rec => rec.Id) : 0;
            var element = new Implementer
            {
                Id = maxId + 1,
            };
            source.Implementers.Add(CreateModel(model, element));
        }
        public void Update(ImplementerBindingModel model)
        {
            var element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(ImplementerBindingModel model)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Implementers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            implementer.ImplementerFIO = model.ImplementerFIO;
            implementer.PauseTime = model.PauseTime;
            implementer.WorkingTime = model.WorkingTime;
            return implementer;
        }
        private ImplementerViewModel CreateModel(Implementer implementer)
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
