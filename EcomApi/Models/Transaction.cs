using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomApi.Models {

	[FirestoreData]
	public class Order {


		[FirestoreProperty("Username")]
		public string Username { get; set; }

		[FirestoreProperty("Transaction")]
		public Transaction[] Transactions { get; set; }
	}

	[FirestoreData]
	public class Transaction {
		[FirestoreProperty("ProductID")]
		public string ProductID { get; set; }

		[FirestoreProperty("Amount")]
		public int Amount { get; set; }

		[FirestoreProperty("Price")]
		public float Price { get; set; }
	}

}
