using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResturangFrontEnd.Models;
using System.Text;

namespace ResturangFrontEnd.Controllers
{
    public class TablesController : Controller
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7157/";

        public TablesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Tables";

            var response = await _httpClient.GetAsync($"{baseUrl}api/Tables");
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            var TableList = JsonConvert.DeserializeObject<List<Table>>(json);

            return View(TableList);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Table";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Table table)
        {
            if (!ModelState.IsValid)
            {
                return View(table);
            }

            var json = JsonConvert.SerializeObject(table);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUrl}api/Tables/CreateTable", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int tableID)
        {
            var response = await _httpClient.GetAsync($"{baseUrl}api/Tables/GetSpecificTable/{tableID}");

            var json = await response.Content.ReadAsStringAsync();

            var table = JsonConvert.DeserializeObject<Table>(json);

            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Table table)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(table);
            }

            var json = JsonConvert.SerializeObject(table);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"{baseUrl}api/Tables/UpdateTable/{table.TableID}", content);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int tableID)
        {
            var response = await _httpClient.DeleteAsync($"{baseUrl}api/Tables/DeleteTable/{tableID}");

            return RedirectToAction("Index");
        }
    }
}

