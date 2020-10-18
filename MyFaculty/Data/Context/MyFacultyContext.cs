using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Data.Context.Entity;

namespace MyFaculty.Data.Context
{
    public class MyFacultyContext : DbContext
    {
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<MarkEntity> Marks { get; set; }
        public DbSet<SemesterEntity> Semesters { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }


        public MyFacultyContext(DbContextOptions<MyFacultyContext> options)
            : base(options)
        {
        }
    }
}