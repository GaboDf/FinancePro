using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = DAL.DO.Objects;
namespace DAL.Repository
{
    public interface IRepositoryIngresos : IRepository<data.Ingresos>
    {
        Task<IEnumerable<data.Ingresos>> GetAllWithAsAsync();
        Task<data.Ingresos> GetOneByIdAsAsync(int id);
    }
}
