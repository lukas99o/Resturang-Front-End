using Microsoft.AspNetCore.Mvc;
using ResturangFrontEnd.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ResturangFrontEnd.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7157/";

        public CustomersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Customer";

            var response = await _httpClient.GetAsync($"{baseUrl}api/Customers");
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            var customerList = JsonConvert.DeserializeObject<List<Customer>>(json);

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

            var json = JsonConvert.SerializeObject(customer);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUrl}api/customers/CreateCustomer", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int customerID)
        {
            var response = await _httpClient.GetAsync($"{baseUrl}api/Customers/GetSpecificCustomer/{customerID}");

            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            var customer = JsonConvert.DeserializeObject<Customer>(json);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var json = JsonConvert.SerializeObject(customer);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"{baseUrl}api/Customers/UpdateCustomer/{customer.CustomerID}", content);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int customerID)
        {
            var response = await _httpClient.DeleteAsync($"{baseUrl}api/Customers/DeleteCustomer/{customerID}");
            return RedirectToAction("Index");
        }
    }
}
