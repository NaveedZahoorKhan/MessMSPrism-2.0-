using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MessMSPrism.Models;

namespace MessMSPrism.Resources.HelperClasses
{
    public sealed class Repository <T> where T : class
    {
        private  DbContext _context;
        public Repository()
        {
            using (var ctx = new MessMsContext())
            {
                _context = ctx;
            }
        }


        #region Insert Methods

        public void Insert(T entity)

        {
            _context.Set<T>().Attach(entity);
            _context.SaveChanges();
        }
        #endregion

        #region Update Methods

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.SaveChanges();

        }

        #endregion

        #region Delete Methods

        

        public void DeleteById(int id)
        {
            var e = GetById(id);
            Delete(e);
            
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        #endregion
        



        #region Find Methods



        public T GetById(int id)
        {
            using (_context= new MessMsContext())
            {
                return _context.Set<T>().Find(id);
            }   
        }

        public IEnumerable<T> GetAll()
        {
            using (_context = new MessMsContext())
            {
                return _context.Set<T>();

            }    
        }

        #endregion

        #region Others

        public int GetCount()
        {
            using (_context = new MessMsContext())
            {
                return _context.Set<T>().AsEnumerable().Count();
            }
        }

        #endregion
    }
}