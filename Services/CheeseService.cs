using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Serialization;
using cheeseIt.Models;
using cheeseIt.Converters;

namespace cheeseIt.Services
{
    public class CheeseService
    {
        private readonly IHostingEnvironment _env;

        public CheeseService(IHostingEnvironment environment)
        {
            _env = environment;
        }

        public Cheese[] LoadCheeses(DateTime dateRecieved){
            var items = GetItems();

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

        private List<Item> GetItems(){
            XmlSerializer ser = new XmlSerializer(typeof(ItemCollection));
            List<Item> items = null;
			using (FileStream myFileStream = new FileStream(_env.ContentRootPath + "/rustydragon_13062017.xml", FileMode.Open))
			{
                items = ((ItemCollection)ser.Deserialize(myFileStream)).Items;
			}
            return items;
        }
    }
}
