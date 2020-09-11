using Google.Cloud.Firestore;
using System;

namespace EcomApi.Database {

	public class Firebase {

		public const string PROJECT_ID = "lacia-project-deal";
		public const string GOOGLE_APPLICATION_CREDENTIALS = "./Resources/firebase-key.json";

		/// <summary>
		/// Create new instance of database
		/// </summary>
		public static FirestoreDb Database {
			get {
				FirestoreDb db = FirestoreDb.Create(EcomApi.Database.Firebase.PROJECT_ID);
				return db;
			}
		}

		public static void SetupEnvironment() {
			Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", EcomApi.Database.Firebase.GOOGLE_APPLICATION_CREDENTIALS);
		}

	}

}
