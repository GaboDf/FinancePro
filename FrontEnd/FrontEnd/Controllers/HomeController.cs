using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using data = FrontEnd.Models;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private String baseurl = "https://financeproapi20210813124632.azurewebsites.net/";
        private Servicios.CategoriasServices service = new Servicios.CategoriasServices();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["currentGasto"] = getCurrentGasto();
            ViewData["currentGastos"] = getCurrentGastos();
            ViewData["Gastos"] = getGastos();
            ViewData["Ingresos"] = GetIngresos();
            ViewData["CantCategoria"] = getCurrentCategorias();
            ViewData["NomCategorias"] = null; //CAMBIAR
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<data.Gastos> getGastos()
        {
            List<data.Gastos> aux = new List<data.Gastos>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res =  cl.GetAsync("/api/Gastos/GetGastos").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Gastos>>(auxres).FindAll(m => m.Idcliente == getUserId());
                }
            }
            return aux;
        }

        private List<data.Ingresos> GetIngresos()
        {
            List<data.Ingresos> aux = new List<data.Ingresos>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res =  cl.GetAsync("/api/Ingresos/GetIngresos").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Ingresos>>(auxres).FindAll(m => m.Idusuario == getUserId());
                }
            }
            return aux;
        }
        private string getCurrentGasto()
        {
            double total = 0;
            foreach (data.Gastos gasto in getGastos())
            {
                int month = DateTime.Parse(gasto.Fecha.ToString()).Month;
                if(month == DateTime.Now.Month)
                {
                    total += gasto.Monto;
                }
            }
            return total.ToString();
        }

        private List<data.Gastos> getCurrentGastos()
        {
            return getGastos().FindAll(m => DateTime.Parse(m.Fecha.ToString()).Month == DateTime.Now.Month);
        }
        private int[] getCurrentCategorias()
        {
            int cCategoria = service.GetAll().Count();
            int[] cantCategoria = new int[cCategoria];
            foreach (data.Gastos gasto in getGastos())
            {
                cantCategoria[gasto.Idcategoria-1] += 1;
            }
            return cantCategoria;
        }
        private string getUserId()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
