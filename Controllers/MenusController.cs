using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResturangFrontEnd.Models;
using System.Text;

namespace ResturangFrontEnd.Controllers
{
    public class MenusController : Controller
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7157/";

        public MenusController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Menu";

            var response = await _httpClient.GetAsync($"{baseUrl}api/Menus");
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            var menuList = JsonConvert.DeserializeObject<List<Menu>>(json);

            return View(menuList);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Menu";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return View(menu);
            }

            var json = JsonConvert.SerializeObject(menu);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUrl}api/menus/CreateMenu", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int menuID)
        {
            var response = await _httpClient.GetAsync($"{baseUrl}api/Menus/UpdateMenu/{menuID}");

            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            var menu = JsonConvert.DeserializeObject<Menu>(json);

            return View(menu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return View(menu);
            }

            var json = JsonConvert.SerializeObject(menu);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"{baseUrl}api/Menus/UpdateMenu/{menu.MenuID}", content);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int menuID)
        {
            var response = await _httpClient.DeleteAsync($"{baseUrl}api/Menus/DeleteMenu/{menuID}");
            return RedirectToAction("Index");
        }
    }
}
