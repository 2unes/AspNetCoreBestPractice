using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper.Contrib.Extensions;
using Shared;

namespace Repositories.Dapper
{
    /// <summary>
    /// When ID type is not specified assume int
    /// </summary>
    public abstract class Repository<T> : Repository<T, int>
        where T : class, IIdentifiable<int>
    {
    }

    public abstract class Repository<T,ID> : IRepository<T,ID> 
        where T : class, IIdentifiable<ID>
    {
        protected abstract IDbConnection Conn { get; }

        private IList<Action<T>> _onSaved = new List<Action<T>>();
        private IList<Action<T>> _onCreated = new List<Action<T>>();
        private IList<Action<T>> _onUpdated = new List<Action<T>>();
        private IList<Action<T>> _onDestroyed = new List<Action<T>>();

        /// <summary>
        /// Adds an action that is executed on change
        /// </summary>
        /// <param name="action">Action.</param>
        public void AddOnSaved(Action<T> action){
            _onSaved.Add(action);
        }

        /// <summary>
        /// Adds an action that is executed on Created (insert operations)
        /// </summary>
        /// <param name="action">Action.</param>
        public void AddOnCreated(Action<T> action){
            _onCreated.Add(action);
        }

        /// <summary>
        /// Adds the on destroyed.
        /// </summary>
        /// <param name="action">Action.</param>
        public void AddOnDestroyed(Action<T> action)
        {
            _onDestroyed.Add(action);
        }

        /// <summary>
        /// Delete the record specified by the id
        /// </summary>
        /// <param name="id">Identifier.</param>
        public virtual bool Delete(T item)
        {
            var result =  Conn.Delete(item);
            foreach (var action in _onDestroyed)
                action(item);
            
            return result;
        }

        public abstract bool Delete(ID id);

        /// <summary>
        /// Retrieves the record specified by the id
        /// </summary>
        /// <returns>The item retrieved</returns>
        /// <param name="id">The id of the resource to retrieve</param>
        public virtual T Get(ID id)
        {
            return Conn.Get<T>(id);
        }

        /// <summary>
        /// Insert the specified item.
        /// </summary>
        /// <returns>The inserted record with the auto generated id</returns>
        /// <param name="item">Item.</param>
        public virtual void Insert(T item)
        {
            Conn.Insert(item);
            foreach (var action in _onCreated)
                action(item);
            foreach (var action in _onSaved)
                action(item);
        }

        /// <summary>
        /// Update the specified item.
        /// </summary>
        /// <returns>The updated item</returns>
        /// <param name="item">item to update</param>
        public virtual void Update(T item)
        {
            Conn.Update(item);
            foreach (var action in _onUpdated)
                action(item);
            foreach (var action in _onSaved)
                action(item);
        }

        /// <summary>
        /// Executes Insert or Update based on whether or not the ID is present
        /// </summary>
        /// <returns>The item to save</returns>
        /// <param name="item">item to save</param>
        public virtual void Save(T item){
            if (item.Id.Equals(default(T))){
				 Insert(item);
            }else{
                Update(item);
            }
        }
        /// <summary>
        /// Finds all of the current type
        /// </summary>
        /// <returns>All of type</returns>
        public virtual IEnumerable<T> FindAll(){
            return Conn.GetAll<T>();
        }
    }
}
