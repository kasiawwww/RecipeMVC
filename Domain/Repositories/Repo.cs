using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public class Repo : IRepo<T>
    {
        public T Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList()
        {
            throw new NotImplementedException();
        }

        public bool Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
