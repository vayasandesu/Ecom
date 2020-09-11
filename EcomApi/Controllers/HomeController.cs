using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Controllers {

	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase {

		[HttpGet]
		public string Get() {
			return "API Document : https://docs.google.com/document/d/1iQSFaiqG9yKsMoTzITsJzybf71ZIYOKuB4cZit0r0NM/edit?usp=sharing";
		}

	}

}
