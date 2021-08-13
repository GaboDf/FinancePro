using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data = DAL.DO.Objects;
using DAL.EF;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        private readonly IMapper _mapper;

        public GastosController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Gastos
        [HttpGet]
        //[Route("/Gastos")]
        public async Task<ActionResult<IEnumerable<DataModels.Gastos>>> GetGastos()
        {
            var res = await new BS.Gastos(_context).GetAllWithAsync();

            var mapaux = _mapper.Map<IEnumerable<data.Gastos>, IEnumerable<DataModels.Gastos>>(res).ToList();

            return mapaux;

        }

        // GET: api/Gastos/5
        [HttpGet("{id}")]
        //[Route("/Gastos/{id?}")]
        public async Task<ActionResult<DataModels.Gastos>> GetGastos(int id)
        {
            var Gastos = await new BS.Gastos(_context).GetOneByIdWithAsync(id);
            var mapaux = _mapper.Map<data.Gastos, DataModels.Gastos>(Gastos);
            if (Gastos == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Gastos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //[Route("/Gastos/{id?}")]
        public async Task<IActionResult> PutGastos(int id, DataModels.Gastos Gastos)
        {
            if (id != Gastos.Id)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<DataModels.Gastos, data.Gastos>(Gastos);
                new BS.Gastos(_context).Update(mapaux);
            }
            catch (Exception ee)
            {
                if (!GastosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gastos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //[Route("/Gastos")]
        public async Task<ActionResult<DataModels.Gastos>> PostGastos(DataModels.Gastos Gastos)
        {
            var mapaux = _mapper.Map<DataModels.Gastos, data.Gastos>(Gastos);
            new BS.Gastos(_context).Insert(mapaux);
            return CreatedAtAction("GetGastos", new { id = Gastos.Id }, Gastos);
        }

        // DELETE: api/Gastos/5
        [HttpDelete("{id}")]
        //[Route("/Gastos/{id?}")]
        public async Task<ActionResult<DataModels.Gastos>> DeleteGastos(int id)
        {
            var Gastos = new BS.Gastos(_context).GetOneByID(id);
            if (Gastos == null)
            {
                return NotFound();
            }

            new BS.Gastos(_context).Delete(Gastos);
            var mapaux = _mapper.Map<data.Gastos, DataModels.Gastos>(Gastos);
            return mapaux;
        }

        private bool GastosExists(int id)
        {
            return (new BS.Gastos(_context).GetOneByID(id) != null);
        }
    }
}

