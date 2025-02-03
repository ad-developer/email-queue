using System.Linq.Expressions;

namespace EmailQueueCore.Common;

public interface IBaseRepository<TEntity> where TEntity : class
{
    bool SaveChanges { get; set; }

    IContext Context { get; set;}

    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<IEnumerable<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);
    
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    
    Task<IEnumerable<TEntity>> GetPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
    
    Task<TEntity> GetByIdAsync(Guid id);
    
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
   IEnumerable<TEntity> GetAll();
    
    IEnumerable<TEntity> GetAllPaged(int pageNumber, int pageSize);
    
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    
    IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
    
    TEntity GetById(Guid id);
    
    TEntity Add(TEntity entity);
    
    TEntity Update(TEntity entity);
    
    void Delete(Guid id);
}