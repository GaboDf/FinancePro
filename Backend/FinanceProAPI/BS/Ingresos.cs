using DAL.DO.Interfaces;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;

namespace BS
{
    public class Ingresos : ICRUD<data.Ingresos>
    {
        private SolutionDbContext context;

        public Ingresos(SolutionDbContext _context)
        {
            context = _context;
        }
        public void Delete(data.Ingresos t)
        {
            new DAL.Ingresos(context).Delete(t);
        }

        public IEnumerable<data.Ingresos> GetAll()
        {
            return new DAL.Ingresos(context).GetAll();
        }

        public async Task<IEnumerable<data.Ingresos>> GetAllWithAsync()
        {
            return await new DAL.Ingresos(context).GetAllWithAsync();
        }

        public data.Ingresos GetOneByID(int id)
        {
            return new DAL.Ingresos(context).GetOneByID(id);
        }

        public async Task<data.Ingresos> GetOneByIdWithAsync(int id)
        {
            return await new DAL.Ingresos(context).GetOneByIdWithAsync(id);
        }

        public void Insert(data.Ingresos t)
        {
            new DAL.Ingresos(context).Insert(t);
        }

        public void Update(data.Ingresos t)
        {
            new DAL.Ingresos(context).Update(t);
        }
    }
}
