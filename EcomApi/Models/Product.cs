using Google.Cloud.Firestore;
using System.Collections.Generic;

namespace EcomApi.Models {

	[FirestoreData]
	public class Product {

		[FirestoreProperty("Name")]
		public string Name { get; set; }

		[FirestoreProperty("Slug")]
		public string Slug { get; set; }

		[FirestoreProperty("Price")]
		public float Price { get; set; }

		//public static Product Parse(DocumentSnapshot doc) {
		//	var product = new Product();
		//	doc.TryGetValue<string>("Name", out var name);
		//	doc.TryGetValue<string>("Slug", out var slug);
		//	doc.TryGetValue<float>("Price", out var price);

		//	product.Id = doc.Id;
		//	product.Name = name;
		//	product.Slug = slug;
		//	product.Price = price;

		//	return product;
		//}

		//public static Dictionary<string, object> ToDictionary(Product value) {
		//	var dic = new Dictionary<string, object>();
		//	dic.Add("Name", value.Name);
		//	dic.Add("Slug", value.Slug);
		//	dic.Add("Price", value.Price);
		//	return dic;
		//}

	}

}
