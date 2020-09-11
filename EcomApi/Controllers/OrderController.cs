using EcomApi.Database;
using EcomApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EcomApi.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase {
		
		/// <summary>
		/// Get order history
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		[HttpGet("{username}")]
		public IEnumerable<string> Get(string username) {
			var db = Firebase.Database;
			var snaps = db.Collection("Orders").WhereEqualTo("Username", username).GetSnapshotAsync().Result;
			return snaps.Documents.Select((doc, index) => {
				return $"Transaction [{index}] : "+doc.Id;
			});
		}

		/// <summary>
		/// Get order history
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		[HttpGet("{username}/{id}")]
		public string Get(string username, string id) {
			var db = Firebase.Database;

			var doc = db.Collection("Orders").Document(id);
			var snap = doc.GetSnapshotAsync().Result;

			var json = JsonConvert.SerializeObject(snap.ToDictionary());
			var order = JsonConvert.DeserializeObject<Order>(json);
			if(order.Username.Equals(username)) {
				return JsonResponser.Response(true, json);
			} else {
				return JsonResponser.Response(false, "Username mismatch of transaction owner");
			}
		}

		/// <summary>
		/// create transaction
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		[HttpPost("create")]
		public string Create([FromBody] Order order) {
			var db = Firebase.Database;
			var snaps = db.Collection("Orders");
			SetPrice(order);
			var result = snaps.AddAsync(order).Result;
			return JsonResponser.Response(true, $"your transaction ID is [{result.Id}]");
		}

		private void SetPrice(Order order) {

			var cache = new Dictionary<string, Product>();
			foreach(var transaction in order.Transactions) {
				var id = transaction.ProductID;
				if(!cache.ContainsKey(id)) {
					cache.Add(id, ProductController.QueryProduct(transaction.ProductID));
				}

				var product = cache[id];
				transaction.Price = product.Price;
			}
		}

		/// <summary>
		/// remove transaction
		/// </summary>
		/// <returns></returns>
		[HttpDelete("cancel/{username}/{orderID}")]
		public string RevokeOrder(string username, string orderID) {
			var db = Firebase.Database;
			var doc = db.Collection("Orders").Document(orderID);
			var snap = doc.GetSnapshotAsync().Result;
			if(snap.Exists) {
				snap.TryGetValue<string>("Username", out var owner);
				if(!owner.Equals(username)) {
					return JsonResponser.Response(false,
						"cancel fail orderID not match with username's owner");
				} else {
					var result = doc.DeleteAsync().Result;
					return JsonResponser.Response(true, 
						$"cancel order [{orderID}] completed");
				}
			} else {
				return JsonResponser.Response(false, $"order [{orderID}] doesn't exist");
			}
		}

	}
}
