using Microsoft.EntityFrameworkCore;
using SampleDDD.Domain.Entities;
using SampleDDD.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDD.Infra.Data.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
        }
    }
}
