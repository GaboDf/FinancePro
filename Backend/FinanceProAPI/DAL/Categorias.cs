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
    public class Categorias : ICRUD<data.Categorias>
    {
        private Repository<data.Categorias> _repo = null;
        public Categorias(SolutionDbContext solutionDbContext)
        {
            _repo = new Repository<data.Categorias>(solutionDbContext);
        }
        public void Delete(data.Categorias t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<data.Categorias> GetAll()
        {
            return _repo.GetAll();
        }

        public Task<IEnumerable<data.Categorias>> GetAllWithAsync()
        {
            throw new NotImplementedException();
        }

        public data.Categorias GetOneByID(int id)
        {
            return _repo.GetOneById(id);
        }

        public Task<data.Categorias> GetOneByIdWithAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(data.Categorias t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(data.Categorias t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
