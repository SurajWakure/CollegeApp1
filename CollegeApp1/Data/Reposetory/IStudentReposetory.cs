namespace Collegeapp1.Data.Reposetory
{
    public interface IStudentReposetory :ICollegeReposetory<Student>
    {

        Task<List<Student>> GetStudentByFeeStatus(int feeStatus);

        //Task<List<Student>> GetAllAsync();

        /*Task <Student> GetByIdAsync(int id, bool useNoTracking = false);

        Task<Student> GetByNameAsync(string name);

        Task<int> CreateAsync(Student student);
        Task<int> UpdateAsync(Student student);
        Task<bool> DeleteAsync(Student student);*/




        /*List<Student> Getstudents();
        Student GetStudntById(int id);
        Student GetStudentByName(string name);
        int CreateStudent(Student student);
        int UpdateStudent(Student student);
        bool DeleteStudntById(int id);*/
    }
}
