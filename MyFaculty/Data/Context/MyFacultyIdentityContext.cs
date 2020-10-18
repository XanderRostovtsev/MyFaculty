using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyFaculty.Data.Context
{
    public class MyFacultyIdentityContext : IdentityDbContext
    {
        public MyFacultyIdentityContext(DbContextOptions<MyFacultyIdentityContext> options)
            : base(options)
        {
        }
    }
}
