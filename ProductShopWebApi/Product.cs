using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductShopWebApi
{
	public class Product : IEqualityComparer<Product>
	{
		public string Name { get; set;}

		public decimal Price { get; set; }

		public bool Equals(Product x, Product y)
		{
			if (x == null && y == null)
			{
				return true;
			}
			
			if (x == null || y == null)
			{
				return false;
			}
			
			return x.Name == y.Name;
		}

		public int GetHashCode(Product obj)
		{
			return obj.Name.GetHashCode();
		}
	}
}