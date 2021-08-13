using DAL.DO.Interfaces;
using DAL.EF;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;

namespace DAL
{
    public class Gastos : ICRUD<data.Gastos>
    {
        private RepositoryGastos _repo = null;
        public Gastos(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryGastos(solutionDbContext);
        }
        public void Delete(data.Gastos t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Gastos> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<data.Gastos>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsAsync();
        }

        public data.Gastos GetOneByID(int id)
        {
            return _repo.GetOneById(id);
        }

        public async Task<data.Gastos> GetOneByIdWithAsync(int id)
        {
            return await _repo.GetOneByIdAsAsync(id);
        }

        public void Insert(data.Gastos t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Gastos t)
        {
            _repo.Update(t);
            _repo.Commit();
        }

    }
}
