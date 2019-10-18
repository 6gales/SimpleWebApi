using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductShopWebApi.OrderUtils
{
	public class OrderResponse
	{
		public class ProductResponse : Product
		{
			public int ProductCount { get; set; }
					
			public decimal SumPrice { get; set; }

			public ProductResponse()
			{
			}

			public ProductResponse(Product product, int count)
			{
				Name = product.Name;
				Price = product.Price;
				ProductCount = count;
				SumPrice = count * Price;
			}
		}

		public string CustomersName { get; set; }

		public int Id { get; set; }

		public string Date { get; set; }

		public IEnumerable<ProductResponse> Products { get; set; }

		public decimal TotalPrice { get; set; }

		public OrderResponse()
		{
			Date = DateTime.Now.ToShortDateString();
		}

		public OrderResponse(string customersName, int id, IEnumerable<ProductResponse> products, decimal total)
		{
			CustomersName = customersName;
			Id = id;
			Products = products;
			TotalPrice = total;
			Date = DateTime.Now.ToShortDateString();
		}
	}
}