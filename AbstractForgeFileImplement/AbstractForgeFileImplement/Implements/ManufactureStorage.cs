using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using AbstractForgeFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractForgeFileImplement.Implements
{
    public class ManufactureStorage : IManufactureStorage
    {
        private readonly FileDataListSingleton source;
        public ManufactureStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<ManufactureViewModel> GetFullList()
        {
            return source.Manufactures.Select(CreateModel).ToList();
        }
        public List<ManufactureViewModel> GetFilteredList(ManufactureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Manufactures
            .Where(rec => rec.ManufactureName.Contains(model.ManufactureName)).Select(CreateModel).ToList();
        }
        public ManufactureViewModel GetElement(ManufactureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var manufacture = source.Manufactures.FirstOrDefault(rec => rec.ManufactureName == model.ManufactureName || rec.Id == model.Id);
            return manufacture != null ? CreateModel(manufacture) : null;
        }
        public void Insert(ManufactureBindingModel model)
        {
            int maxId = source.Manufactures.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
            var element = new Manufacture
            {
                Id = maxId + 1,
                ManufactureComponents = new
                Dictionary<int, int>()
            };
            source.Manufactures.Add(CreateModel(model, element));
        }
        public void Update(ManufactureBindingModel model)
        {
            var element = source.Manufactures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(ManufactureBindingModel model)
        {
            Manufacture element = source.Manufactures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Manufactures.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Manufacture CreateModel(ManufactureBindingModel model, Manufacture manufacture)
        {
            manufacture.ManufactureName = model.ManufactureName;
            manufacture.Price = model.Price;
            // удаляем убранные
            foreach (var key in manufacture.ManufactureComponents.Keys.ToList())
            {
                if (!model.ManufactureComponents.ContainsKey(key))
                {
                    manufacture.ManufactureComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.ManufactureComponents)
            {
                if (manufacture.ManufactureComponents.ContainsKey(component.Key))
                {
                    manufacture.ManufactureComponents[component.Key] =
                   model.ManufactureComponents[component.Key].Item2;
                }
                else
                {
                    manufacture.ManufactureComponents.Add(component.Key,
                   model.ManufactureComponents[component.Key].Item2);
                }
            }
            return manufacture;
        }
        private ManufactureViewModel CreateModel(Manufacture manufacture)
        {
            return new ManufactureViewModel
            {
                Id = manufacture.Id,
                ManufactureName = manufacture.ManufactureName,
                Price = manufacture.Price,
                ManufactureComponents = manufacture.ManufactureComponents
                .ToDictionary(recPC => recPC.Key, recPC =>
                (source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}
