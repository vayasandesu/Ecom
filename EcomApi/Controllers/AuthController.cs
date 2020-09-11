using EcomApi.Database;
using EcomApi.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace EcomApi.Controllers {

	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase {

		[HttpGet]
		public string Index() {
			return "login feature";
		}


		[HttpPost("login")]
		public string Login([FromBody] User value) {
			// Check username and password are correct
			var firebase = Firebase.Database;
			var username = value.Username;
			var password = value.Password;

			var collection = firebase.Collection("Users");
			var snap = collection.WhereEqualTo("Username", username)
				.WhereEqualTo("Password", password)
				.GetSnapshotAsync().Result;
			if(snap.Count > 0) {
				return $"Hello {username}";
			} else {
				return "maybe username or password are wrong";
			}
		}

		[HttpPost("register")]
		public string Register([FromBody] User user) {
			var firebase = Firebase.Database;
			var username = user.Username;
			var password = user.Password;

			if(IsUserExist(firebase, username)) {
				return $"Username {username} already exist.";
			} else {
				var collection = firebase.Collection("Users");
				var snap = collection.AddAsync(user).Result;

				return $"Welcome to system [{username}].";
			}
			
		}

		private bool IsUserExist(FirestoreDb db, string username) {
			var collection = db.Collection("Users");
			var snap = collection.WhereEqualTo("Username", username)
				.GetSnapshotAsync().Result;
			
			return snap.Count > 0;
		}

	}

}
