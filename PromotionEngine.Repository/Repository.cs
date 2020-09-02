using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PromotionEngine.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        public Repository()
        {
            
        }


        public T Get(int id)
        {
            T var = (T)(object)42;
            return default(T);
        }
    }
}
