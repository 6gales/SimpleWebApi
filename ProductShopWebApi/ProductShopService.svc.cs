using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

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
		public string GetAllProducts() => JsonConvert.SerializeObject(_storage.Products);


		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/shop", ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped)]
		public void AddProduct(string str)
		{

		}


		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/shop/order", ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped)]
		public void MakeOrder(string str)
		{
	
		}

		[WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
			UriTemplate = "/shop", ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped)]
		public void DeleteProduct(string shopId)
		{
		}
	}
}
