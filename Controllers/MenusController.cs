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
            ViewData["Title"] = "Menus";

            var response = await _httpClient.GetAsync($"{baseUrl}api/Menus");
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            var menuList = JsonConvert.DeserializeObject<List<Menu>>(json);

            return View(menuList);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create Menu";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Menu menu)
        {
            ViewData["Title"] = "Create Menu Post";

            if (!ModelState.IsValid)
            {
                return View(menu);
            }

            var json = JsonConvert.SerializeObject(menu);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUrl}api/menus/CreateMenu", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Menu";

            var response = await _httpClient.GetAsync($"{baseUrl}api/Menus/GetSpecificMenu/{id}");

            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            var menu = JsonConvert.DeserializeObject<Menu>(json);

            return View(menu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Menu menu)
        {
            ViewData["Title"] = "Edit Menu Post";

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
            ViewData["Title"] = "Delete Menu Post";

            var response = await _httpClient.DeleteAsync($"{baseUrl}api/Menus/DeleteMenu/{menuID}");
            return RedirectToAction("Index");
        }
    }
}
