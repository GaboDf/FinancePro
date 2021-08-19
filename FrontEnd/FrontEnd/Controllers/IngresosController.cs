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
    public class IngresosController : Controller
    {
        string baseurl = "https://financeproapi20210813124632.azurewebsites.net/";
        CategoriasServices service = new CategoriasServices();
        // GET: Ingresos
        public async Task<IActionResult> Index()
        {
            List<data.Ingresos> aux = new List<data.Ingresos>();
            List<data.Ingresos> naux = new List<data.Ingresos>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("/api/Ingresos/GetIngresos");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Ingresos>>(auxres);
                    naux = aux.FindAll(m => m.Idusuario == getUserId());
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

        // GET: Ingresos/Create
        public IActionResult Create()
        {
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre");
            ViewData["Idcliente"] = getUserId();
            return View();
        }

        // POST: Ingresos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Monto,Nombre,Descripcion,Fecha,Idusuario,Idcategoria")] data.Ingresos Ingresos)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    data.addIngreso request = new data.addIngreso
                    {
                        Monto = Ingresos.Monto,
                        Nombre = Ingresos.Nombre,
                        Descripcion = Ingresos.Descripcion,
                        Fecha = Ingresos.Fecha,
                        Idusuario = Ingresos.Idusuario,
                        Idcategoria = Ingresos.Idcategoria
                    };

                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(request);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("/api/Ingresos/PostIngresos", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre", Ingresos.Idcategoria);
            ViewData["Idcliente"] = getUserId();
            return View(Ingresos);
        }

        // GET: Ingresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ingresos = GetById(id);
            if (Ingresos == null)
            {
                return NotFound();
            }
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre", Ingresos.Idcategoria);
            return View(Ingresos);
        }

        // POST: Ingresos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Nombre,Descripcion,Fecha,Idusuario,Idcategoria")] data.Ingresos Ingresos)
        {
            if (id != Ingresos.Id)
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
                        var content = JsonConvert.SerializeObject(Ingresos);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("/api/Ingresos/PutIngresos/" + id, byteContent).Result;

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
            ViewData["Idcategoria"] = new SelectList(service.GetAll(), "Id", "Nombre", Ingresos.Idcategoria);
            return View(Ingresos);
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
                HttpResponseMessage res = await cl.DeleteAsync("/api/Ingresos/DeleteIngresos/" + id);

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
        private data.Ingresos GetById(int? id)
        {
            data.Ingresos aux = new data.Ingresos();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("/api/Ingresos/GetIngresos/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.Ingresos>(auxres);
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
