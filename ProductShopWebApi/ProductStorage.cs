using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProductShopWebApi
{
	public class ProductStorage
	{
		private static int _id;
		private readonly string _initialDataFileName = "InitialProductData.json";
		private ConcurrentBag<Product> _productSet = null;
		private ConcurrentDictionary<string, decimal> _priceByName;

		public ProductStorage()
		{
			if (_productSet == null)
			{
				lock (this)
				{
					if (_productSet == null)
					{
						if (File.Exists(_initialDataFileName))
						{
							using (StreamReader r = new StreamReader(_initialDataFileName))
							{
								string json = r.ReadToEnd();
								List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
								_productSet = new ConcurrentBag<Product>(products);
							}
						}

						if (_productSet == null)
						{
							_productSet = new ConcurrentBag<Product>();
						}
					}
					_priceByName = new ConcurrentDictionary<string, decimal>();
				}
			}
	
			foreach (var product in _productSet)
			{
				_priceByName[product.Name] = product.Price;
			}

			AppDomain.CurrentDomain.ProcessExit += WriteOnExit;
		}

		public int GetId()
		{
			int id;
			lock (typeof(ProductStorage))
			{
				id = _id++;
			}
			return id;
		}

		public IEnumerable<Product> Products { get => _productSet; }

		public IEnumerable<Product> InsertProducts(IEnumerable<Product> products)
		{
			List<Product> inserted = new List<Product>();
			foreach (var product in products)
			{
				if (!_productSet.Contains(product))
				{
					_productSet.Add(product);
					_priceByName[product.Name] = product.Price;
					inserted.Add(product);
				}
			}

			return inserted;
		}

		public bool Get(string name, out Product product)
		{
			decimal price;
			if (!_priceByName.TryGetValue(name, out price))
			{
				product = null;
				return false;
			}

			product = new Product { Name = name, Price = price };
			return true;
		}

		public bool Delete(string name)
		{
			Product p = new Product { Name = name, Price = 0.0M };
			if (!_productSet.Contains(p))
				return false;

			_priceByName.TryRemove(name, out decimal val);
			_productSet.TryTake(out p);
			
			return true;
		}

		public void Update(Product product)
		{
			_priceByName.AddOrUpdate(product.Name, product.Price, (k, v) => product.Price);
			if (!_productSet.Contains(product))
			{
				_productSet.Add(product);
			}
			else
			{
				_productSet.TryTake(out Product p);
				_productSet.Add(product);
			}
		}

		private void WriteOnExit(object sender, EventArgs args)
		{
			using (FileStream products = File.Open(_initialDataFileName,
						FileMode.Create, FileAccess.Write))
			using (StreamWriter a = new StreamWriter(products))
			{
				string json = JsonConvert.SerializeObject(_productSet);
				a.WriteLine(json);
			}
		}
	}
}