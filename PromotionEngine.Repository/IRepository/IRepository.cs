using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
    }
}
