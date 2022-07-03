using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CRUDOperationUsingWEBAPI.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CRUDOperationUsingWEBAPI.Context
{
    public partial class StudentDBContext : IdentityDbContext<Users>
    {
      
        public StudentDBContext(DbContextOptions<StudentDBContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
