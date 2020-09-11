using Google.Cloud.Firestore;

namespace EcomApi.Models {

	[FirestoreData]
	public class User {

		[FirestoreDocumentId]
		public int Id { get; set; }

		[FirestoreProperty("Username")]
		public string Username { get; set; }

		[FirestoreProperty("Password")]
		public string Password { get; set; }

	}

}
