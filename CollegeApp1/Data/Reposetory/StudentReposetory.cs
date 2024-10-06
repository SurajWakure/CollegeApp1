using Collegeapp1.Data.Reposetory;
using Microsoft.EntityFrameworkCore;

namespace Collegeapp1.Data.IReposetory
{
    public class StudentReposetory :CollegeReposetory<Student>, IStudentReposetory
    {
        private readonly CollegeDbContext _dbContext;

        public StudentReposetory(CollegeDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<List<Student>> GetStudentByFeeStatus(int feeStatus)
        {
            //write code to return student who do not give fees 
            return null;
        }
        /*
       public async Task<int> CreateAsync(Student student)
       {
           _dbContext.students.Add(student);
           await _dbContext.SaveChangesAsync();
           return student.Id;
       }

       public async Task<bool> DeleteAsync(Student student)
       {

        *//*   Student? student = await _dbContext.students.Where(n => n.Id == student.Id).FirstOrDefaultAsync();

           if (student == null)
               throw new ArgumentNullException($"no sudent with id found {id}");*//*

           _dbContext.students.Remove(student);
           await _dbContext.SaveChangesAsync();
           return true;


       }

       public async Task<List<Student>> GetAllAsync()
       {
           return await _dbContext.students.ToListAsync();
       }

       public async Task<Student> GetByIdAsync(int id,bool useNoTracking =false)
       {
           if(useNoTracking)
           return await _dbContext.students.AsNoTracking().Where(n => n.Id == id).FirstOrDefaultAsync();

           else
               return await _dbContext.students.AsNoTracking().Where(n => n.Id == id).FirstOrDefaultAsync();
       }

       public async Task<Student> GetByNameAsync(string name)
       {
           return await _dbContext.students.Where(n => n.Name.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
       }

       public async Task<int> UpdateAsync(Student student)
       {
           _dbContext.Update(student);
          *//* var studenntToUpdate = await _dbContext.students.AsNoTracking().Where(n => n.Id == student.Id).FirstOrDefaultAsync();
           if (studenntToUpdate == null)
               throw new ArgumentNullException($"no sudent with id found {student.Id}");


               studenntToUpdate.Name=student.Name;
               studenntToUpdate.Email=student.Email;
               studenntToUpdate.Address=student.Address;
               studenntToUpdate.DOB= student.DOB;*//*

               await _dbContext.SaveChangesAsync();

           return student.Id;
       }*/
    }

}