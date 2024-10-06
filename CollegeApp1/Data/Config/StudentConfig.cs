using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collegeapp1.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.Name).IsRequired();
            builder.Property(n => n.Name).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);


            builder.HasData(new List<Student>()
             {
                 new Student{
                     Id=1,
                     Name="suraj",
                     Email="surajwakure007@gmail.com",
                     Address="sawtamalii nagar murud",
                     DOB=new DateTime(2024,12,12),
                     marks=90

                 },
                 new Student{
                     Id=2,
                     Name="ajit",
                     Email="ajitwakure007@gmail.com",
                     Address="sawtamalii nagar murud",
                     DOB=new DateTime(2024,12,12),
                     marks=85
                 }

             });

            builder.HasOne(n => n.Department)
                .WithMany(n => n.Students)
                .HasForeignKey(n => n.DepartmentId)
                .HasConstraintName("FK_Students_Department");
        }
       
    }
}
