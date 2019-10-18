using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductShopWebApi.OrderUtils
{
	public class OrderRequest
	{
		public class ProductRequest
		{
			public string Name { get; set; }

			public int RequestedNumber { get; set; }
		}

		public string CustomersName { get; set; }

		public IEnumerable<ProductRequest> RequestedProducts { get; set; }
	}
}