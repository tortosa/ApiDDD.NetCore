using Domain.Entities.UsersAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TortosaApi.Infrastructure.Repository.Entities.UsersAgg
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        { }

        // Mandatory constructor w/o parameters to generate migrations: https://docs.microsoft.com/es-es/ef/core/miscellaneous/cli/dbcontext-creation#using-a-constructor-with-no-parameters
        private UsersDbContext()
        { }
    }
}