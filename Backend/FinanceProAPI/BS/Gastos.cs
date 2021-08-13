using DAL.DO.Interfaces;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;


namespace BS
{
    public class Gastos : ICRUD<data.Gastos>
    {
        private SolutionDbContext context;

        public Gastos(SolutionDbContext _context)
        {
            context = _context;
        }
        public void Delete(data.Gastos t)
        {
            new DAL.Gastos(context).Delete(t);
        }

        public IEnumerable<data.Gastos> GetAll()
        {
            return new DAL.Gastos(context).GetAll();
        }

        public async Task<IEnumerable<data.Gastos>> GetAllWithAsync()
        {
            return await new DAL.Gastos(context).GetAllWithAsync();
        }

        public data.Gastos GetOneByID(int id)
        {
            return new DAL.Gastos(context).GetOneByID(id);
        }

        public async Task<data.Gastos> GetOneByIdWithAsync(int id)
        {
            return await new DAL.Gastos(context).GetOneByIdWithAsync(id);
        }

        public void Insert(data.Gastos t)
        {
            new DAL.Gastos(context).Insert(t);
        }

        public void Update(data.Gastos t)
        {
            new DAL.Gastos(context).Update(t);
        }
    }
}
