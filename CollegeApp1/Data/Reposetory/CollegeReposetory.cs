using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Collegeapp1.Data.Reposetory
{
    public class CollegeReposetory<T>:ICollegeReposetory<T> where T : class
    {
        private readonly CollegeDbContext _dbContext;
        private DbSet<T> _dbSet;

        public CollegeReposetory(CollegeDbContext dbContext)
        {
            _dbContext=dbContext;
            _dbSet=dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T dbrecord)
        {
            _dbSet.Add(dbrecord);
            await _dbContext.SaveChangesAsync();
            return dbrecord;
        }

        public async Task<bool> DeleteAsync(T dbrecord)
        {

            _dbSet.Remove(dbrecord);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T,bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();

            else
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
        }

      /*  public async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }*/

        public async Task<T> UpdateAsync(T dbrecord)
        {
            _dbContext.Update(dbrecord);
            /* var studenntToUpdate = await _dbContext.students.AsNoTracking().Where(n => n.Id == student.Id).FirstOrDefaultAsync();
             if (studenntToUpdate == null)
                 throw new ArgumentNullException($"no sudent with id found {student.Id}");


                 studenntToUpdate.Name=student.Name;
                 studenntToUpdate.Email=student.Email;
                 studenntToUpdate.Address=student.Address;
                 studenntToUpdate.DOB= student.DOB;*/

            await _dbContext.SaveChangesAsync();

            return dbrecord;
        }
    }
}
