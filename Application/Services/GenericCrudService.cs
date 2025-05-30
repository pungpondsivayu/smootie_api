using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Shared.Responses;
using Application.Shared;
using Domain.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class GenericCrudService<TEntity> : IGenericCrudService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> repo;

        public GenericCrudService(IGenericRepository<TEntity> _repo)
        {
            repo = _repo;
        }

        public Task<TEntity?> GetByIdAsync(object id) => repo.GetByIdAsync(id);

        public async Task<PaginationResponse<TEntity>> GetAllPageAsync(int pageSize, int currentPage)
        {
            var all = await repo.GetAllAsync();
            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse<TEntity>(paged, totalCount, currentPage, pageSize);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<List<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            // repo คือ IGenericRepository<TEntity> ที่คุณ inject มา
            return await repo.GetAllWithIncludeAsync(includes);
        }

        public IQueryable<TEntity> GetAll()
        {
            return repo.GetAll();
        }

        public async Task<ResponseMessage> AddAsync(TEntity entity)
        {
            await repo.AddAsync(entity);
            return new ResponseMessage(200, true, "Created successfully");

        }

        public async Task<ResponseMessage> AddRangeAsync(List<TEntity> entity)
        {

            await repo.AddRangeAsync(entity);
            return new ResponseMessage(200, true, "Created successfully");
        }

        public async Task<ResponseMessage> UpdateAsync(TEntity entity)
        {
            await repo.UpdateAsync(entity);
            return new ResponseMessage(200, true, "Updated successfully");
        }

        public async Task<ResponseMessage> UpdateRangeAsync(List<TEntity> entity)
        {
            await repo.UpdateRangeAsync(entity);
            return new ResponseMessage(200, true, "Updated successfully");

        }

        public async Task<ResponseMessage> DeleteAsync(object id)
        {
            await repo.DeleteAsync(id);
            return new ResponseMessage(200, true, "Deleted successfully");

        }
    }
}
