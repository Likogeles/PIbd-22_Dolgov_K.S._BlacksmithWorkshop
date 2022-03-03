using AbstractForgeContracts.Enums;
using AbstractForgeFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AbstractForgeFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ManufactureFileName = "Manufacture.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Manufacture> Manufactures { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Manufactures = LoadManufactures();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        public static void FileDataListSingletonMySave()
        {
            instance.SaveComponents();
            instance.SaveOrders();
            instance.SaveManufactures();
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveManufactures();
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ManufactureId = Convert.ToInt32(elem.Element("ManufactureId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToInt32(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ?
                        (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value)
                    });
                }
            }
            return list;
        }
        private List<Manufacture> LoadManufactures()
        {
            var list = new List<Manufacture>();
            if (File.Exists(ManufactureFileName))
            {
                var xDocument = XDocument.Load(ManufactureFileName);
                var xElements = xDocument.Root.Elements("Manufacture").ToList();
                foreach (var elem in xElements)
                {
                    var manufComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("ManufactureComponents").Elements("ManufactureComponent").ToList())
                    {
                        manufComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Manufacture
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ManufactureName = elem.Element("ManufactureName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        ManufactureComponents = manufComp
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("ManufactureId", order.ManufactureId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveManufactures()
        {
            if (Manufactures != null)
            {
                var xElement = new XElement("Products");
                foreach (var manufacture in Manufactures)
                {
                    var compElement = new XElement("ManufactureComponents");
                    foreach (var component in manufacture.ManufactureComponents)
                    {
                        compElement.Add(new XElement("ManufactureComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Manufacture",
                     new XAttribute("Id", manufacture.Id),
                     new XElement("ManufactureName", manufacture.ManufactureName),
                     new XElement("Price", manufacture.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ManufactureFileName);
            }
        }
    }
}