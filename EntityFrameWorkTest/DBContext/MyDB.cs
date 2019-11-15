using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using EntityFrameWorkTest.Entities;

namespace EntityFrameWorkTest.DBContext
{
    class MyDB : DbContext
    {


        public MyDB():base("SQLiteConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<Install> Installs { get; set; }

    }
}
