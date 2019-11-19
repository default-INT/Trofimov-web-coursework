using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using RepairServiceCenterASP.Models;

namespace RepairServiceCenterASP.Data
{
    public class RepairServiceCenterContext : DbContext
    {
        public RepairServiceCenterContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RepairedModel> RepairedModels { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<TypeOfFault> TypeOfFaults { get; set; }
        public DbSet<ServicedStore> ServicedStores { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
