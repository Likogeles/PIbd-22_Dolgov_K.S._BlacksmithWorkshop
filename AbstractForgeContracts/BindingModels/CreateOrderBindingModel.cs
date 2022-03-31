namespace AbstractForgeContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    public class CreateOrderBindingModel
    {
        public int ManufactureId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
