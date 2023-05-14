using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO_Demo.DAL;
using System.Xml.Linq;
using ADO_Demo.Model;
using System.Reflection;

namespace ADO_Demo
{
    public class ProdDemo
    {
        private static IConfiguration _configuration;
        static void Main()
        {
            GetAppSettingsFile();
            InsertProduct();
            searchID(2);
            searchName("iPhone");
            DeleteEntry(2011);
        }
        static ProductDAL getObj()
        {
            ProductDAL obj = new ProductDAL(_configuration);
            return obj;
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();

        }

        static void InsertProduct()
        {            
            ProductDAL obj = getObj();
            ProductModel model = new ProductModel { Name= "iPhone", Price= 89990, Qty= 10};
            obj.AddData(model);           
        }
        static void searchID(int id)
        {
            ProductDAL obj = getObj();
            ProductModel model = null;
            model = obj.Search(id);
            Console.WriteLine("The entry at provided ID is:");
            Console.WriteLine($"ID: {model.ID}, Name: {model.Name}, Price: {model.Price}, Quantity: {model.Qty}");
        }
        static void searchName(string name)
        {
            ProductDAL obj = getObj();
            Console.WriteLine("Searching by Name");
            List<ProductModel> models = new List<ProductModel>();
            models = obj.Search("iPhone");
            foreach (ProductModel p in models)
            {
                Console.WriteLine($"ID: {p.ID}, Name: {p.Name}, Price: {p.Price}, Quantity: {p.Qty}");
            }
        }
        static void DeleteEntry(int id)
        {
            ProductDAL obj = getObj();
            Console.WriteLine("Deleting Entry:");
            obj.Delete(id);
        }
    }
}