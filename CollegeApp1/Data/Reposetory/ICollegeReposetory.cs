using System.Linq.Expressions;

namespace Collegeapp1.Data.Reposetory
{
    public interface ICollegeReposetory<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);

       // Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);

        Task<T> CreateAsync(T dbrecord);
        Task<T> UpdateAsync(T dbrecord);
        Task<bool> DeleteAsync(T dbrecord);
    }
}
