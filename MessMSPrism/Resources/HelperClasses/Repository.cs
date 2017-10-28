using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MessMSPrism.Resources.HelperClasses
{
    public class Repository <T> where T : class
    {
        private DbContext _context;
        public Repository()
        {
            using (var ctx = new MessMsContext())
            {
                _context = ctx;
            }
        }


        #region Insert Methods

        public virtual void Insert(T entity)

        {
            _context.Set<T>().Attach(entity);
            _context.SaveChanges();
        }
        #endregion

        #region Update Methods

        public virtual void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.SaveChanges();

        }

        #endregion

        #region Delete Methods

        

        public virtual void DeleteById(int id)
        {
            var e = GetById(id);
            Delete(e);
            
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        #endregion

        #region Find Methods

        

        public virtual T GetById(int id) => _context.Set<T>().Find(id);

        public virtual IEnumerable<T> getAll() => _context.Set<T>();

        #endregion

    }
}