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

        public Cheese[] LoadCheeses(string fileName, DateTime dateRecieved){
            var items = GetItems(fileName);

            return getCheeseFromItems(items, dateRecieved);
        }

		public Cheese[] LoadCheeses(IFormFile file, DateTime dateRecieved)
		{
			var items = GetItems(file);

            return getCheeseFromItems(items, dateRecieved);
		}

        private Cheese[] getCheeseFromItems(List<Item> items, DateTime dateRecieved){
			var cheeses = new List<Cheese>();
			if (items != null)
			{
				var converter = new CheeseConverter();
				foreach (var item in items)
				{
					cheeses.Add(converter.CheeseFromItem(item, dateRecieved));
				}
			}
			_cheeseRepo.InsertCheeses(cheeses);
			return new List<Cheese>().ToArray();
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
