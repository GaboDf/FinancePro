using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using data = FrontEnd.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontEnd.Servicios;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class GastosController : Controller
    {
        string baseurl = "https://financeproapi20210813124632.azurewebsites.net/";
        CategoriasServices service = new CategoriasServices();
        // GET: Gastos
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

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = GetById(id);


            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre");
            ViewData["Idcliente"] = getUserId();
            return View();
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Monto,Nombre,Descripcion,Fecha,Idcliente,Idcategoria")] data.Gastos gastos)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    data.addGasto request = new data.addGasto{
                        Monto = gastos.Monto,
                        Nombre = gastos.Nombre,
                        Descripcion = gastos.Descripcion,
                        Fecha = gastos.Fecha,
                        Idcliente = gastos.Idcliente,
                        Idcategoria = gastos.Idcategoria
                    };
                    
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(request);
                    
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("/api/Gastos/PostGastos", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre", gastos.Idcategoria);
            ViewData["Idcliente"] = getUserId();
            return View(gastos);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastos = GetById(id);
            if (gastos == null)
            {
                return NotFound();
            }
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre", gastos.Idcategoria);
            return View(gastos);
        }

        // POST: Gastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Nombre,Descripcion,Fecha,Idcliente,Idcategoria")] data.Gastos gastos)
        {
            if (id != gastos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        var content = JsonConvert.SerializeObject(gastos);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("/api/Gastos/PutGastos/" + id, byteContent).Result;

                        if (postTask.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    var aux2 = GetById(id);
                    if (aux2 == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre", gastos.Idcategoria);
            return View(gastos);
        }

        //// GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = GetById(id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        //// POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("/api/Gastos/DeleteGastos/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }


        private bool GastoExists(int id)
        {
            return (GetById(id) != null);
        }
        private data.Gastos GetById(int? id)
        {
            data.Gastos aux = new data.Gastos();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("/api/Gastos/GetGastos/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.Gastos>(auxres);
                }
            }
            return aux;
        }
        private List<data.Categorias> getAllCategorias()
        {

            List<data.Categorias> aux = new List<data.Categorias>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("/api/Categorias/GetCategorias").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Categorias>>(auxres);
                }
            }
            return aux;
        }

        private string getUserId()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
