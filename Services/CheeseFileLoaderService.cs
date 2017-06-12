using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using cheeseIt.Models;
using cheeseIt.Converters;
using cheeseIt.Repositories;
using Microsoft.AspNetCore.Http;

namespace cheeseIt.Services
{
    public class CheeseLoaderService : ICheeseLoaderService
    {
        private ICheeseRepository _cheeseRepo;
        
        public CheeseLoaderService(ICheeseRepository cheeseRepository){
            _cheeseRepo = cheeseRepository;
        }

        public int LoadCheeses(string fileName, DateTime dateRecieved){
            var items = GetItems(fileName);
            var cheeses = convertAndStoreCheeseForItems(items, dateRecieved);
            return cheeses;
        }

        public int LoadCheeses(IFormFile file, DateTime dateRecieved)
        {
            var items = GetItems(file);
            var cheeses = convertAndStoreCheeseForItems(items, dateRecieved);

            return cheeses;
        }

        private int convertAndStoreCheeseForItems(List<Item> items, DateTime dateRecieved){
            var cheeses = new List<Cheese>();
            if (items != null)
            {
                var converter = new CheeseConverter();
                foreach (var item in items)
                {
                    var convertedCheese = converter.CheeseFromItem(item, dateRecieved);
                    if (convertedCheese != null)
                    {
                        cheeses.Add(convertedCheese);
                    }

                }
            }
            _cheeseRepo.InsertCheeses(cheeses);
            return cheeses.Count;
        }
        
        private List<Item> GetItems(IFormFile file)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ItemCollection));
            List<Item> items = null;
            using (var fileStream = file.OpenReadStream())
            {
                items = ((ItemCollection)ser.Deserialize(fileStream)).Items;
            }
            return items;
        }

        private List<Item> GetItems(string fileName){
            XmlSerializer ser = new XmlSerializer(typeof(ItemCollection));
            List<Item> items = null;
            using (FileStream myFileStream = new FileStream(fileName, FileMode.Open))
            {
                items = ((ItemCollection)ser.Deserialize(myFileStream)).Items;
            }
            return items;
        }
    }
}
