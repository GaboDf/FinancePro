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
    public class Ingresos : ICRUD<data.Ingresos>
    {
        private RepositoryIngresos _repo = null;
        public Ingresos(SolutionDbContext solutionDbContext)
        {
            _repo = new RepositoryIngresos(solutionDbContext);
        }
        public void Delete(data.Ingresos t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Ingresos> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<data.Ingresos>> GetAllWithAsync()
        {
            return await _repo.GetAllWithAsAsync();
        }

        public data.Ingresos GetOneByID(int id)
        {
            return _repo.GetOneById(id);
        }

        public async Task<data.Ingresos> GetOneByIdWithAsync(int id)
        {
            return await _repo.GetOneByIdAsAsync(id);
        }

        public void Insert(data.Ingresos t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Ingresos t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
