using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using MusicHub.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MusicHub.Data.Framework
{
    public abstract class BaseObject<T> : IBaseRepository<T>  where T : class 
    {
        /// <summary>
        /// dbo bajo el cual se esta ejecutando
        /// </summary>
        public MusicHubDB db;
        private bool sharedb = false;
        //private static Objectdb oc;

        /// <summary>
        /// Constructor por defecto que inicializa las entidades
        /// </summary>
        public BaseObject()
        {
            db = new MusicHubDB();
            //oc = ((IObjectdbAdapter)db).Objectdb;
        }

        /// <summary>
        /// Recibe un parametro db para trabajar en base al mismo
        /// </summary>
        /// <param name="cont">db sobre el cual se va a trabajar</param>
        public BaseObject(MusicHubDB cont)
        {
            db = cont;
            sharedb = true;
        }

        public void Dispose()
        {
            if (sharedb && (db != null))
                db.Dispose();
        }

        protected DbSet<T> DbSet
        {
            get
            {
                return db.Set<T>();
            }
        }

        public abstract T GetById(int id);

        /// <summary>
        /// Borrar una Entidad
        /// </summary>
        /// <param name="entity">Entidad</param>
        //public virtual string Delete(T entity)
        //{
        //    oc.DeleteObject(entity);
        //    oc.SaveChanges();

        //    return "";
        //}

        ///// <summary>
        ///// Actualizar una entidad
        ///// </summary>
        ///// <param name="entity">Entidad</param>
        //public virtual string Update(T entity)
        //{
        //    ObjectStateEntry stateEntry = null;

        //    if (oc.ObjectStateManager.TryGeTStateEntry(entity, out stateEntry))
        //    {
        //        oc.ObjectStateManager.ChangeObjectState(entity, EntityState.Unchanged);
        //    }


        //    oc.AttachTo(GetEntitySetName(db, entity), entity);
        //    oc.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);

        //    return oc.SaveChanges();
        //}

        ///// <summary>
        ///// Agregar una entidad. 
        ///// Si tiene mas de un SET debe sobreescribirse este metodo en la clase
        ///// que lo implementa
        ///// </summary>
        ///// <param name="entity">Entidad</param>
        //public virtual T Add(T entity){

        //    oc.CreateObjectSet<T>().AddObject(entity);
        //    oc.SaveChanges();

        //    return entity;
        //}



        ///// <summary>
        ///// Utilizado para hacer una busqueda
        ///// </summary>
        ///// <param name="specification">Parametros</param>
        ///// <returns>La entidad que implementa la clase abstracta</returns>
        //protected T Find(Expression<Func<T, bool>> specification)
        //{
        //    return oc.CreateObjectSet<T>().SingleOrDefault(specification);
        //}

        ///// <summary>
        ///// Trae todos los objetos de la coleccion.
        ///// Si tiene mas de un SET debe sobreescribirse este metodo en la clase
        ///// que lo implementa
        ///// </summary>
        ///// <returns>Una coleccion de la clase abstracta que lo implementa</returns>
        //public virtual IQueryable<T> GetAll()
        //{
        //    //var xLinq = from x in db.CreateObjectSet<T>()
        //    //            select x;

        //    //return xLinq;

        //    return oc.CreateObjectSet<T>();
        //}

        //public virtual IQueryable<T> GetAll(int numberOfObjectsPerPage, int pageNumber, string orderbyf)
        //{
        //    var query = from d in oc.CreateObjectSet<T>() orderby orderbyf select d;
        //    return query.Skip(numberOfObjectsPerPage * pageNumber)
        //        .Take(numberOfObjectsPerPage);
        //}

        //public static string GetEntitySetName(MusicHubDB db, object entity)
        //{
        //    Type entityType = Objectdb.GeTType(entity.GetType());
        //    if (entityType == null)
        //        throw new InvalidOperationException("not an entity");

        //    EntityContainer container = oc.MetadataWorkspace.GetEntityContainer(oc.DefaultContainerName, DataSpace.CSpace);
        //    return (from entitySet in container.BaseEntitySets
        //            where entitySet.ElementType.Name.Equals(entityType.Name)
        //            select entitySet.Name).Single();
        //}

        public IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<T>();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter,
         out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() :
                DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual T Create(T T)
        {
            var newEntry = DbSet.Add(T);
            if (!sharedb)
                db.SaveChanges();
            return newEntry;
        }

        public virtual List<T> Create(IEnumerable<T> T)
        {
            List<T> newEntries = new List<T>();

            foreach (T o in T)
            {

                newEntries.Add(DbSet.Add(o));
            }

            if (!sharedb)
                db.SaveChanges();

            return newEntries;
        }

        public virtual int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public virtual int Delete(T T)
        {
            DbSet.Remove(T);
            if (!sharedb)
                return db.SaveChanges();
            return 0;
        }

        public virtual int Update(T T)
        {
            var entry = db.Entry(T);
            DbSet.Attach(T);
            entry.State = EntityState.Modified;
            if (!sharedb)
                return db.SaveChanges();
            return 0;
        }

        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            if (!sharedb)
                return db.SaveChanges();
            return 0;
        }


        public IQueryable<T> Filter<Key>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<T>.Delete(T t)
        {
            DbSet.Remove(t);

            if (!sharedb)
                db.SaveChanges();
        }

        public virtual async Task<IQueryable<T>> AllAsync()
        {
            return DbSet.AsQueryable();
        }

        public virtual async Task<IQueryable<T>> FilterAsync(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<T>();
        }

        public virtual async Task<bool> ContainsAsync(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual async Task<T> FindAsync(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual async Task<T> CreateAsync(T t)
        {
            var newEntry = DbSet.Add(t);
            if (!sharedb)
                db.SaveChanges();
            return newEntry;
        }

        public virtual async Task<List<T>> CreateAsync(IEnumerable<T> T)
        {
            List<T> newEntries = new List<T>();

            foreach (T o in T)
            {

                newEntries.Add(DbSet.Add(o));
            }

            if (!sharedb)
                db.SaveChanges();

            return newEntries;
        }

        public virtual async Task<int> UpdateAsync(T T)
        {
            var entry = db.Entry(T);
            DbSet.Attach(T);
            entry.State = EntityState.Modified;
            if (!sharedb)
                return db.SaveChanges();
            return 0;
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            if (!sharedb)
                return db.SaveChanges();
            return 0;
        }
    }
}
