using CountryFunction.DataAccess.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace CountryFunction.API.Controllers
{
    public class Country : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CountryController : Controller
        {
            private readonly ICRUDService _crudService;
            public CountryController(ICRUDService crudservice)
            {
                _crudService = crudservice;
            }

            [HttpGet]
            public ActionResult Get()
            {
                var countryfrom = _crudService.GetAllAsync();
                return Ok(countryfrom);
            }
        }
    }
}
