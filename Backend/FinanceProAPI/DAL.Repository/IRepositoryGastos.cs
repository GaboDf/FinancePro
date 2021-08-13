using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;
namespace DAL.Repository
{
    public interface IRepositoryGastos : IRepository<data.Gastos>
    {
        Task<IEnumerable<data.Gastos>> GetAllWithAsAsync();
        Task<data.Gastos> GetOneByIdAsAsync(int id);
    }
}
