using EcomApi.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EcomApi.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class ProfileController : ControllerBase {
		
		[HttpGet("{user}")]
		public string GetProfile(string user) {
			var db = Firebase.Database;
			var snap = db.Collection("Profiles").Document(user).GetSnapshotAsync().Result;
			if(snap.Exists) {
				snap.TryGetValue<string>("Slug", out var slug);
				return "profile : " + slug;
			} 
			return "profile : ";
		}

		// 
		[HttpPatch("edit")]
		public string Edit([FromBody] Dictionary<string, string> value) {
			value.TryGetValue("Username", out var user);
			value.TryGetValue("Slug", out var slug);

			var db = Firebase.Database;
			var collection = db.Collection("Profiles");
			var snap = collection.Document(user);

			var data = new Dictionary<string, object>();
			data.Add("Slug", slug);

			if(snap.GetSnapshotAsync().Result.Exists) {
				//Edit
				snap.UpdateAsync(data);
				return "edit completed";

			} else {
				// Create
				var doc = collection.Document(user).SetAsync(data);
				return "edit completed";
			}

		}

	}

}
