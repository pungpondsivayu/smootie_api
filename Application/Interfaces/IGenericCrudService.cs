using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared.Responses;
using Application.Shared;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericCrudService<TEntity> where TEntity : class
    {
        Task<PaginationResponse<TEntity>> GetAllPageAsync(int pageSize, int currentPage);
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<ResponseMessage> AddAsync(TEntity entity);
        Task<ResponseMessage> AddRangeAsync(List<TEntity> entity);
        Task<ResponseMessage> UpdateAsync(TEntity entity);
        Task<ResponseMessage> UpdateRangeAsync(List<TEntity> entity);
        Task<ResponseMessage> DeleteAsync(object id);
        Task<TEntity?> GetByIdAsync(object id);
    }
}
