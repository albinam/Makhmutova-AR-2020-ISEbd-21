﻿using DinerDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DinerDatabaseImplement
{
    public class DinerDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-2RG8HAM\SQLEXPRESS;Initial Catalog=DinerDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Food> Foods { set; get; }
        public virtual DbSet<Snack> Snacks { set; get; }
        public virtual DbSet<SnackFood> SnackFoods { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}
