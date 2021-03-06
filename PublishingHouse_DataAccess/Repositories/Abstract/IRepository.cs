using PublishingHouse_Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_DataAccess.Repositories
{
    public interface IRepository<T> where T : IEntity, new()
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<bool> IsExists(int id);

        //Asenkron ile çalışscaksınız geri dönüşümsüz yani void yerine Task yazılabilir
    }
}
