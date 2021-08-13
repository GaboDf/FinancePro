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
    public class IngresosController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        private readonly IMapper _mapper;

        public IngresosController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ingresos
        [HttpGet]
        //[Route("/Ingresos")]
        public async Task<ActionResult<IEnumerable<DataModels.Ingresos>>> GetIngresos()
        {
            var res = await new BS.Ingresos(_context).GetAllWithAsync();

            var mapaux = _mapper.Map<IEnumerable<data.Ingresos>, IEnumerable<DataModels.Ingresos>>(res).ToList();

            return mapaux;

        }

        // GET: api/Ingresos/5
        [HttpGet("{id}")]
        //[Route("/Ingresos/{id?}")]
        public async Task<ActionResult<DataModels.Ingresos>> GetIngresos(int id)
        {
            var Ingresos = await new BS.Ingresos(_context).GetOneByIdWithAsync(id);
            var mapaux = _mapper.Map<data.Ingresos, DataModels.Ingresos>(Ingresos);
            if (Ingresos == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Ingresos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //[Route("/Ingresos/{id?}")]
        public async Task<IActionResult> PutIngresos(int id, DataModels.Ingresos Ingresos)
        {
            if (id != Ingresos.Id)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<DataModels.Ingresos, data.Ingresos>(Ingresos);
                new BS.Ingresos(_context).Update(mapaux);
            }
            catch (Exception ee)
            {
                if (!IngresosExists(id))
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

        // POST: api/Ingresos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //[Route("/Ingresos")]
        public async Task<ActionResult<DataModels.Ingresos>> PostIngresos(DataModels.Ingresos Ingresos)
        {
            var mapaux = _mapper.Map<DataModels.Ingresos, data.Ingresos>(Ingresos);
            new BS.Ingresos(_context).Insert(mapaux);
            return CreatedAtAction("GetIngresos", new { id = Ingresos.Id }, Ingresos);
        }

        // DELETE: api/Ingresos/5
        [HttpDelete("{id}")]
        //[Route("/Ingresos/{id?}")]
        public async Task<ActionResult<DataModels.Ingresos>> DeleteIngresos(int id)
        {
            var Ingresos = new BS.Ingresos(_context).GetOneByID(id);
            if (Ingresos == null)
            {
                return NotFound();
            }

            new BS.Ingresos(_context).Delete(Ingresos);
            var mapaux = _mapper.Map<data.Ingresos, DataModels.Ingresos>(Ingresos);
            return mapaux;
        }

        private bool IngresosExists(int id)
        {
            return (new BS.Ingresos(_context).GetOneByID(id) != null);
        }
    }
}
