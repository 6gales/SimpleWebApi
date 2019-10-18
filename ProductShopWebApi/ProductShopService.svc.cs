using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using ProductShopWebApi.OrderUtils;

namespace ProductShopWebApi
{
	[ServiceContract(Namespace = "")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ProductShopService
	{
		private static ProductStorage _storage = new ProductStorage(); 

		[WebInvoke(Method = "GET",
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/shop")]
		
		public IEnumerable<Product> GetAllProducts() => _storage.Products;

		
		[WebInvoke(Method = "GET",
			RequestFormat = WebMessageFormat.Json,
			ResponseFormat = WebMessageFormat.Json,
			UriTemplate = "/shop/{productName}")]

		public Product GetProduct(string productName)
		{
			if (!_storage.Get(productName, out Product product))
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			
			return product;
		}


		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/shop", ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped)]
		
		public IEnumerable<Product> PostProducts(IEnumerable<Product> products)
						=> _storage.InsertProducts(products);


		[WebInvoke(Method = "PATCH", RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/shop", BodyStyle = WebMessageBodyStyle.Wrapped)]

		public void PatchProduct(Product product) => _storage.Update(product);


		[WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/shop/{productName}", ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped)]

		public void DeleteProduct(string productName)
		{
			if (!_storage.Delete(productName))
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
		}

		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/shop/order", ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped)]

		public OrderResponse MakeOrder(OrderRequest orderRequest)
		{
			int id = _storage.GetId();
			var products = new List<OrderResponse.ProductResponse>();
			decimal sum = 0.0M;

			foreach (var requestedProduct in orderRequest.RequestedProducts)
			{
				if (_storage.Get(requestedProduct.Name, out Product given))
				{
					var ordered = new OrderResponse.ProductResponse(given, requestedProduct.RequestedNumber);
					products.Add(ordered);
					sum += ordered.SumPrice;
				}
			}

			var order = new OrderResponse(orderRequest.CustomersName, id, products, sum);
			return order;
		}
	}
}
