using System;
using MyFaculty.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyFaculty.Data.Migrations
{
    [DbContext(typeof(MyFacultyContext))]
    partial class MyFacultyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.GroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.MarkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Mark");

                    b.Property<int>("StudentId");

                    b.Property<int>("SubjectId");

                    b.HasKey("Id");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.SemesterEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("EndYear");

                    b.Property<int>("StartYear");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.StudentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<int>("GroupId");

                    b.Property<string>("Email");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.SubjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<int>("SemesterId");

                    b.Property<int>("TeacherId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.TeacherEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("Grade");

                    b.Property<string>("Email");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.MarkEntity", b =>
            {
                b.HasOne("MyFaculty.Data.Context.Entity.StudentEntity")
                    .WithMany()
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.MarkEntity", b =>
            {
                b.HasOne("MyFaculty.Data.Context.Entity.SubjectEntity")
                    .WithMany()
                    .HasForeignKey("SubjectId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.StudentEntity", b =>
            {
                b.HasOne("MyFaculty.Data.Context.Entity.GroupEntity")
                    .WithMany()
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.SubjectEntity", b =>
            {
                b.HasOne("MyFaculty.Data.Context.Entity.SubjectEntity")
                    .WithMany()
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.SubjectEntity", b =>
            {
                b.HasOne("MyFaculty.Data.Context.Entity.StudentEntity")
                    .WithMany()
                    .HasForeignKey("SemesterId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("MyFaculty.Data.Context.Entity.SubjectEntity", b =>
            {
                b.HasOne("MyFaculty.Data.Context.Entity.StudentEntity")
                    .WithMany()
                    .HasForeignKey("TeacherId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}
