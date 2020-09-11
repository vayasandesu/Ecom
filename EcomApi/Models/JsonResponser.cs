using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EcomApi.Models {
	public class JsonResponser {
		public static string Response(bool isSuccess, string Message) {
			JObject j = new JObject();
			j.Add("result", isSuccess);
			j.Add("message", Message);
			return JsonConvert.SerializeObject(j);
		}
	}
}
