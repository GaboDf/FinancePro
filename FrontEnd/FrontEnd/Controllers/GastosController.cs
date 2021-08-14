using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using data = FrontEnd.Models;
using System.Net.Http;

namespace FrontEnd.Controllers
{
    public class GastosController : Controller
    {
        string baseurl = "https://financeproapi20210813124632.azurewebsites.net/";

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            List<data.Gastos> aux = new List<data.Gastos>();
            List<data.Gastos> naux = new List<data.Gastos>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("/api/Gastos/GetGastos");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Gastos>>(auxres);
                    naux = aux.FindAll(m => m.Idcliente == getUserId());
                }
            }
            return View(naux);
        }

        private string getUserId()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
