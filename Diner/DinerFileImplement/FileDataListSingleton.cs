using DinerBusinessLogic.Enums;
using DinerFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace DinerFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string FoodFileName = "Food.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string SnackFileName = "Snack.xml";
        private readonly string SnackFoodFileName = "SnackFood.xml";
        private readonly string StorageFileName = "Storage.xml";
        private readonly string StorageFoodFileName = "StorageFood.xml";
        public List<Food> Foods { get; set; }
        public List<Order> Orders { get; set; }
        public List<Snack> Snacks { get; set; }
        public List<SnackFood> SnackFoods { get; set; }
        public List<Storage> Storages { set; get; }
        public List<StorageFood> StorageFoods { set; get; }
        private FileDataListSingleton()
        {
            Foods = LoadFoods();
            Orders = LoadOrders();
            Snacks = LoadSnacks();
            SnackFoods = LoadSnackFoods();
            Storages = LoadStorages();
            StorageFoods = LoadStorageFoods();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveFoods();
            SaveOrders();
            SaveSnacks();
            SaveSnackFoods();
            SaveStorages();
            SaveStorageFoods();
        }
        private List<Food> LoadFoods()
        {
            var list = new List<Food>();
            if (File.Exists(FoodFileName))
            {
                XDocument xDocument = XDocument.Load(FoodFileName);
                var xElements = xDocument.Root.Elements("Food").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Food
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FoodName = elem.Element("FoodName").Value
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
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SnackId = Convert.ToInt32(elem.Element("SnackId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Snack> LoadSnacks()
        {
            var list = new List<Snack>();
            if (File.Exists(SnackFileName))
            {
                XDocument xDocument = XDocument.Load(SnackFileName);
                var xElements = xDocument.Root.Elements("Snack").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Snack
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SnackName = elem.Element("SnackName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<SnackFood> LoadSnackFoods()
        {
            var list = new List<SnackFood>();
            if (File.Exists(SnackFoodFileName))
            {
                XDocument xDocument = XDocument.Load(SnackFoodFileName);
                var xElements = xDocument.Root.Elements("SnackFood").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new SnackFood
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SnackId = Convert.ToInt32(elem.Element("SnackId").Value),
                        FoodId = Convert.ToInt32(elem.Element("FoodId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private List<Storage> LoadStorages()
        {
            var list = new List<Storage>();
            if (File.Exists(StorageFileName))
            {
                XDocument xDocument = XDocument.Load(StorageFileName);
                var xElements = xDocument.Root.Elements("Storage").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Storage()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        StorageName = elem.Element("StorageName").Value.ToString()
                    });
                }
            }
            return list;
        }

        private List<StorageFood> LoadStorageFoods()
        {
            var list = new List<StorageFood>();
            if (File.Exists(StorageFoodFileName))
            {
                XDocument xDocument = XDocument.Load(StorageFoodFileName);
                var xElements = xDocument.Root.Elements("StorageFood").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new StorageFood()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FoodId = Convert.ToInt32(elem.Element("FoodId").Value),
                        StorageId = Convert.ToInt32(elem.Element("StorageId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private void SaveFoods()
        {
            if (Foods != null)
            {
                var xElement = new XElement("Foods");
                foreach (var Food in Foods)
                {
                    xElement.Add(new XElement("Food",
                    new XAttribute("Id", Food.Id),
                    new XElement("FoodName", Food.FoodName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(FoodFileName);
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
                    new XElement("SnackId", order.SnackId),
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
        private void SaveSnacks()
        {
            if (Snacks != null)
            {
                var xElement = new XElement("Snacks");
                foreach (var Snack in Snacks)
                {
                    xElement.Add(new XElement("Snack",
                    new XAttribute("Id", Snack.Id),
                    new XElement("SnackName", Snack.SnackName),
                    new XElement("Price", Snack.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SnackFileName);
            }
        }
        private void SaveSnackFoods()
        {
            if (SnackFoods != null)
            {
                var xElement = new XElement("SnackFoods");
                foreach (var SnackFood in SnackFoods)
                {
                    xElement.Add(new XElement("SnackFood",
                    new XAttribute("Id", SnackFood.Id),
                    new XElement("SnackId", SnackFood.SnackId),
                    new XElement("FoodId", SnackFood.FoodId),
                    new XElement("Count", SnackFood.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SnackFoodFileName);
            }
        }
        private void SaveStorages()
        {
            if (Storages != null)
            {
                var xElement = new XElement("Storages");
                foreach (var elem in Storages)
                {
                    xElement.Add(new XElement("Storage",
                        new XAttribute("Id", elem.Id),
                        new XElement("StorageName", elem.StorageName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StorageFileName);
            }
        }
        private void SaveStorageFoods()
        {
            if (StorageFoods != null)
            {
                var xElement = new XElement("StorageFoods");
                foreach (var elem in StorageFoods)
                {
                    xElement.Add(new XElement("StorageFood",
                        new XAttribute("Id", elem.Id),
                        new XElement("FoodId", elem.FoodId),
                        new XElement("StorageId", elem.StorageId),
                        new XElement("Count", elem.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StorageFoodFileName);
            }
        }
    }
}