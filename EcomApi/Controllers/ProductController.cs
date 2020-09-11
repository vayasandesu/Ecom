using EcomApi.Database;
using EcomApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase{

		/// <summary>
		/// get list of products in system
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Product> Get() {

			var database = Firebase.Database;
			var collection = database.Collection("Products");
			var snaps = collection.GetSnapshotAsync().Result;

			return snaps.Select(doc => doc.ConvertTo<Product>());

		}

		/// <summary>
		/// Detail of Product
		/// </summary>
		/// <param name="id">id of product</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public Product Get(string id) {
			return QueryProduct(id);
		}

		public static Product QueryProduct(string id) {
			var database = Firebase.Database;
			var snap = database.Collection($"Products").Document(id);
			var doc = snap.GetSnapshotAsync().Result;
			return doc.ConvertTo<Product>();
		}

		/// <summary>
		/// setup
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[HttpPost]
		public string Add([FromBody] Product value) {
			var database = Firebase.Database;

			var collection = database.Collection($"Products");
			var result = collection.AddAsync(value).Result;

			return result.ToString();
		}

	}

}
