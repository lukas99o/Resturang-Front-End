using Microsoft.AspNetCore.Mvc;
using ResturangFrontEnd.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResturangFrontEnd.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClinet;
        private string baseUrl = "https://localhost:7157/";

        public CustomersController(HttpClient httpClient)
        {
            _httpClinet = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Customers";

            var response = await _httpClinet.GetAsync($"{baseUrl}GetAllCustomers");
            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var customerList = JsonSerializer.Deserialize<List<Customer>>(json);

            if (customerList != null)
            {
                foreach (var customer in customerList)
                {
                    Console.WriteLine(customer.Name);
                }
            }

            return View(customerList);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Customer";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var json = JsonSerializer.Serialize(customer);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClinet.PostAsync($"{baseUrl}CreateCustomer", content);

            return RedirectToAction("Index");
        }
    }
}
