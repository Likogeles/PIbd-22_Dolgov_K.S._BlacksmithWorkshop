using AbstractForgeBusinessLogic.OfficePackage;
using AbstractForgeBusinessLogic.OfficePackage.HelperModels;
using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.BusinessLogicsContracts;
using AbstractForgeContracts.StoragesContracts;
using AbstractForgeContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractForgeBusinessLogic.BusinessLogics
{

    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IManufactureStorage _manufactureStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(IManufactureStorage manufactureStorage, IComponentStorage
        componentStorage, IOrderStorage orderStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _manufactureStorage = manufactureStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportManufactureComponentViewModel> GetManufactureComponent()
        {
            var components = _componentStorage.GetFullList();
            var manufactures = _manufactureStorage.GetFullList();
            var list = new List<ReportManufactureComponentViewModel>();
            foreach (var component in components)
            {
                var record = new ReportManufactureComponentViewModel
                {
                    ComponentName = component.ComponentName,
                    Manufactures = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var manufacture in manufactures)
                {
                    if (manufacture.ManufactureComponents.ContainsKey(component.Id))
                    {
                        record.Manufactures.Add(new Tuple<string, int>(manufacture.ManufactureName,
                       manufacture.ManufactureComponents[component.Id].Item2));
                        record.TotalCount +=
                       manufacture.ManufactureComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                ManufactureName = x.ManufactureName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Manufactures = _manufactureStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveManufactureComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                ManufactureComponents = GetManufactureComponent()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
