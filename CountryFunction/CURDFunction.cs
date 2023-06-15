using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CountryFunction.DataAccess.Implementation;
using System.Data.SqlClient;
using CountryFunction.Domain.Model;

namespace CountryFunction
{
    public class CURDFunction
    {
        private ICRUDService _service;
        public CURDFunction(ICRUDService service)
        {
            _service = service;
        }

        [FunctionName("create")]
        public async Task<IActionResult> create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "create")] HttpRequest req,
            ILogger log)
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
            {
                string bodystring = await new StreamReader(req.Body).ReadToEndAsync();
                var CountryToCreate = JsonConvert.DeserializeObject<Country>(bodystring);
                var country = await _service.CreateAsync(CountryToCreate);

                if (country == null)
                    return new BadRequestObjectResult($"country name {CountryToCreate.CountryName} exists!");

                var responsemsg = $"country is created, the id is {country.Id}";
                return new OkObjectResult(responsemsg);
            }
        }

        [FunctionName("delete")]
        public async Task<IActionResult> delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delete/{id}")] HttpRequest req,
            int id, ILogger log)
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
            {
                var DeleteSuccess = await _service.DeleteAsync(id);
                return new OkObjectResult($"the country is deleted : {DeleteSuccess}");
            }
        }

        [FunctionName("list")]
        public async Task<IActionResult> list(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "list")] HttpRequest req,
            ILogger log)
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
            {
                var country = await _service.GetAllAsync();
                return new OkObjectResult(country);
            }
        }

        [FunctionName("read")]
        public async Task<IActionResult> read(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "read/{id}")] HttpRequest req,
            int id, ILogger log)
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
            {
                var country = await _service.ReadAsync(id);
                return new OkObjectResult(country);
            }
        }

        [FunctionName("update")]
        public async Task<IActionResult> update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "update/{id}")] HttpRequest req,
            int id, ILogger log)
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
            {
                string bodystring = await new StreamReader(req.Body).ReadToEndAsync();
                var UpdateCountry = JsonConvert.DeserializeObject<Country>(bodystring);

                var ctr = await _service.UpdateAsync(id, UpdateCountry);
                return new OkObjectResult(ctr);
            }
        }
    }
}
