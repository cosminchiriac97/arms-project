using System.Collections.Generic;
using Bussiness.Crawler;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    public class SeleniumController : Controller
    {

        private readonly ISeleniumServices _seleniumServices;

        public SeleniumController(ISeleniumServices seleniumServices)
        {
            _seleniumServices = seleniumServices;
        }
        // GET api/values
        [HttpGet]
        public List<string> Get()
        {
            _seleniumServices.ExtractDataFromWeb();
            return null;
        }
    }
}
