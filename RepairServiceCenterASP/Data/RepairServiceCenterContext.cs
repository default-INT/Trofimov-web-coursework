using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Models;

namespace RepairServiceCenterASP.Data
{
    public class RepairServiceCenterContext : DbContext
    {
        public RepairServiceCenterContext(DbContextOptions options) : base(options)
        {
        }
        
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<RepairedModel> RepairedModels { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<TypeOfFault> TypeOfFaults { get; set; }
        public virtual DbSet<ServicedStore> ServicedStores { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
