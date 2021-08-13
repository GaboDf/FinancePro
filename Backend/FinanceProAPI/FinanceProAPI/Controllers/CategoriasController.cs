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
    public class CategoriasController : ControllerBase
    {
        private readonly SolutionDbContext _context;
        private readonly IMapper _mapper;

        public CategoriasController(SolutionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categorias
        [HttpGet]
        //[Route("/Categorias")]
        public async Task<ActionResult<IEnumerable<DataModels.Categorias>>> GetCategorias()
        {
            var res = new BS.Categorias(_context).GetAll();
            var mapaux = _mapper.Map<IEnumerable<data.Categorias>, IEnumerable<DataModels.Categorias>>(res).ToList();

            return mapaux;
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        //[Route("/Categorias/{id?}")]
        public async Task<ActionResult<DataModels.Categorias>> GetCategorias(int id)
        {
            var Categorias = new BS.Categorias(_context).GetOneByID(id);
            var mapaux = _mapper.Map<data.Categorias, DataModels.Categorias>(Categorias);


            if (Categorias == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //[Route("/Categorias/{id?}")]
        public async Task<IActionResult> PutCategorias(int id, DataModels.Categorias Categorias)
        {
            if (id != Categorias.Id)
            {
                return BadRequest();
            }

            try
            {
                var mapaux = _mapper.Map<DataModels.Categorias, data.Categorias>(Categorias);
                new BS.Categorias(_context).Update(mapaux);
            }
            catch (Exception ee)
            {
                if (!CategoriasExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //[Route("/Categorias")]
        public async Task<ActionResult<DataModels.Categorias>> PostCategorias(DataModels.Categorias Categorias)
        {
            var mapaux = _mapper.Map<DataModels.Categorias, data.Categorias>(Categorias);
            new BS.Categorias(_context).Insert(mapaux);

            return CreatedAtAction("GetCategorias", new { id = Categorias.Id }, Categorias);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        //[Route("/Categorias/{id?}")]
        public async Task<ActionResult<DataModels.Categorias>> DeleteCategorias(int id)
        {
            var Categorias = new BS.Categorias(_context).GetOneByID(id);
            if (Categorias == null)
            {
                return NotFound();
            }

            new BS.Categorias(_context).Delete(Categorias);
            var mapaux = _mapper.Map<data.Categorias, DataModels.Categorias>(Categorias);

            return mapaux;
        }

        private bool CategoriasExists(int id)
        {
            return (new BS.Categorias(_context).GetOneByID(id) != null);
        }
    }
}
