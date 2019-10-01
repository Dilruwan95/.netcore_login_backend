using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.models.Repository
{
   public interface IDataRepository <TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        Employee Authenticate(Employee emp);
        // void Update(EEntity dbEntity, EEntity entity);
        //  void Delete(EEntity entity);

    }
}
