using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.ViewModels;
using System.Collections.Generic;
namespace AbstractForgeContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        List<ReportManufactureComponentViewModel> GetManufactureComponent();
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveComponentsToWordFile(ReportBindingModel model);
        /// <summary>
        /// Сохранение компонент с указаеним изделий в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveManufactureComponentToExcelFile(ReportBindingModel model);
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveOrdersToPdfFile(ReportBindingModel model);

    }
}
