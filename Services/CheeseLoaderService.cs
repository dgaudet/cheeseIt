using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using cheeseIt.Models;
using cheeseIt.Converters;

namespace cheeseIt.Services
{
    public class CheeseLoaderService : ICheeseLoaderService
    {
        public Cheese[] LoadCheeses(string fileName, DateTime dateRecieved){
            var items = GetItems(fileName);

            var cheeses = new List<Cheese>();
            if (items != null)
            {
                var converter = new CheeseConverter();
                foreach (var item in items)
                {
                    cheeses.Add(converter.CheeseFromItem(item, dateRecieved));
                }
            }
            return cheeses.ToArray();
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
