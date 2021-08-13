using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL.DO.Objects;

namespace DAL.Repository
{
    public class RepositoryIngresos : Repository<data.Ingresos>, IRepositoryIngresos
    {
        public RepositoryIngresos(SolutionDbContext context) : base(context) { }
        public async Task<IEnumerable<Ingresos>> GetAllWithAsAsync()
        {
            return await _db.Ingresos
                .Include(m => m.Categoria)
                .ToListAsync();
        }

        public async Task<Ingresos> GetOneByIdAsAsync(int id)
        {
            return await _db.Ingresos
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        private SolutionDbContext _db
        {
            get { return dbContext; }
        }
    }
}
