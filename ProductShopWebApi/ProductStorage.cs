using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProductShopWebApi
{
	public class ProductStorage
	{
		public IEnumerable<Product> Products { get => _productSet; }

		private HashSet<Product> _productSet;

		public ProductStorage()
		{
			using (StreamReader r = new StreamReader("InitialProductData.json"))
			{
				string json = r.ReadToEnd();
				_productSet = JsonConvert.DeserializeObject<HashSet<Product>>(json);
			}
		}

		public bool Add(Product product)
		{
			if (_productSet.Contains(product))
				return false;

			_productSet.Add(product);
			return true;
		}

		public bool Delete(Product product)
		{
			if (_productSet.Contains(product))
				return false;

			_productSet.Add(product);
			return true;
		}
	}
}