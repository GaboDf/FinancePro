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
    public class RepositoryGastos : Repository<data.Gastos>, IRepositoryGastos
    {
        public RepositoryGastos(SolutionDbContext context) : base(context){ }
        public async Task<IEnumerable<Gastos>> GetAllWithAsAsync()
        {
            return await _db.Gastos
                .Include(m => m.Categoria)
                .ToListAsync();
        }

        public async Task<Gastos> GetOneByIdAsAsync(int id)
        {
            return await _db.Gastos
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        private SolutionDbContext _db
        {
            get { return dbContext; }
        }
    }
}
